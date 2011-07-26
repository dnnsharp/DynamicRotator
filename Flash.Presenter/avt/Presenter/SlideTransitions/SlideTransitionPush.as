package avt.Presenter.SlideTransitions {
	
	import avt.Presenter.Slide;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.getDefinitionByName;
	import fl.transitions.TweenEvent;
	import avt.Presenter.Presentation;
	
	public class SlideTransitionPush implements ISlideTransition {
		
		public function SlideTransitionPush() {
			// hack the compiler to include these simbols
			var a = None.easeNone;
			a = Regular.easeIn;
			a = Bounce.easeIn;
			a = Back.easeIn;
			a = Elastic.easeIn;
			a = Strong.easeIn;
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
			
			var strEasing:String = config.easing.toString();
			_easing = getDefinitionByName("fl.transitions.easing."+strEasing.substr(0, strEasing.indexOf('.')))[strEasing.substr(strEasing.indexOf('.')+1)] as Function;

			try { _direction = config.direction; } catch (e:Error) { _direction = "left"; }
		}
		
		public function transition(prevSlide:Slide, currentSlide:Slide, fnComplete:Function):void {

			var prop:String;
			var deltaMove:Number;
			
			if (_direction == "up" || _direction== "down") {
				prop="y";
				deltaMove = (_direction =="up" ? -currentSlide.stage.height : currentSlide.stage.height);
			} else {
				prop="x";
				deltaMove = (_direction =="left" ? -currentSlide.stage.width: currentSlide.stage.width);
			}
			
			trace("  > Slide transition PUSH (duration: "+ _duration +"; prop: " + prop + "; delta:"+ deltaMove +")");
			
			if (prevSlide != null) {
				prevSlide.reset();
				//_prevTween = new Tween(prevSlide, prop,_easing,0,deltaMove,_duration,true); 
				//_prevTween.addEventListener(TweenEvent.MOTION_FINISH, function(e:TweenEvent) { prevSlide.visible = false; });
			}

			currentSlide.reset();

			_currentTween= new Tween(currentSlide, prop,_easing,-deltaMove,0,_duration,true); 
			_currentTween.addEventListener(TweenEvent.MOTION_FINISH, function(e:TweenEvent) { fnComplete(); });
			_currentTween.addEventListener(TweenEvent.MOTION_CHANGE , function(e:TweenEvent) { 
				if (prevSlide != null) {
					prevSlide[prop] = deltaMove + e.position;
				}
			});
		}
		
	}
	
}