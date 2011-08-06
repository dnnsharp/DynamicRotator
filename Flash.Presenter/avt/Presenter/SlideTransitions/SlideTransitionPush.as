package avt.Presenter.SlideTransitions {
	
	import avt.Presenter.Slide;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.getDefinitionByName;
	import fl.transitions.TweenEvent;
	import avt.Presenter.Presentation;
	import avt.Util.TransitionUtils;
	
	public class SlideTransitionPush implements ISlideTransition {
		
		public function SlideTransitionPush() {

		}
		
		const DefaultDuration:int = 2000;
		
		var _prevTween:Tween = null;
		var _currentTween:Tween = null;
		
		var _duration:Number = DefaultDuration/1000;
		var _easing:Function = null;
		
		var _direction:String = "left";
		
		public function init(presentation:Presentation, config:*):void {
			
			try {
				_duration = parseInt(config.duration) / 1000;
				if (_duration<=0) 
					_duration = DefaultDuration / 1000;
			} catch (e:Error) { _duration = DefaultDuration/10000; }
			
			_easing = TransitionUtils.easingFromString(config.easing.toString());

			try { _direction = config.direction; } catch (e:Error) { _direction = "left"; }
		}
		
		public function transition(prevSlide:Slide, currentSlide:Slide, fnComplete:Function):void {

			var prop:String;
			var deltaMove:Number;
			
			if (_direction == "up" || _direction== "down") {
				prop="y";
				deltaMove = (_direction =="up" ? -currentSlide.presentation.slideHeight : currentSlide.presentation.slideHeight);
			} else {
				prop="x";
				deltaMove = (_direction =="left" ? -currentSlide.presentation.slideWidth: currentSlide.presentation.slideWidth);
			}
			
			trace("  > Slide transition PUSH (duration: "+ _duration +"; prop: " + prop + "; delta:"+ deltaMove +")");
			
			if (prevSlide != null) {
				prevSlide.reset();
			}

			currentSlide.reset();

			_currentTween= new Tween(currentSlide, prop,_easing,-deltaMove,0,_duration,true); 
			_currentTween.addEventListener(TweenEvent.MOTION_FINISH, function(e:TweenEvent) { 
				if (prevSlide != null)
					prevSlide.visible = false;
			   fnComplete(); 
			});
			_currentTween.addEventListener(TweenEvent.MOTION_CHANGE , function(e:TweenEvent) { 
				if (prevSlide != null) {
					prevSlide[prop] = deltaMove + e.position;
				}
			});
		}
		
	}
	
}