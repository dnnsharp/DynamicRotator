package avt.Presenter.SlideTransitions {
	
	import avt.Presenter.Slide;
	import fl.transitions.Tween;
	import flash.utils.getDefinitionByName;
	import fl.transitions.TweenEvent;
	import avt.Presenter.Presentation;
	import avt.Util.TransitionUtils;
	
	public class SlideTransitionFade implements ISlideTransition {
		
		public function SlideTransitionFade() {
			
		}
		
		const DefaultDuration:int = 2000;
		
		var _prevTween:Tween = null;
		var _currentTween:Tween = null;
		
		var _duration:Number = DefaultDuration/1000;
		var _easing:Function = null;
		
		public function init(presentation:Presentation, config:*):void {
			
			try {
				_duration = parseInt(config.duration) / 1000;
				if (_duration<=0) 
					_duration = DefaultDuration / 1000;
			} catch (e:Error) { _duration = DefaultDuration/10000; }
			
			var strEasing:String = config.easing.toString();
			_easing = TransitionUtils.easingFromString(strEasing);
		}
		
		public function transition(prevSlide:Slide, currentSlide:Slide, fnComplete:Function):void {
			
			trace("  > Slide transition FADE (duration: "+ _duration +")");
			
			if (prevSlide != null) {
				prevSlide.reset();
				_prevTween = new Tween(prevSlide, "alpha",_easing,1,0,_duration,true); 
				
			}

			currentSlide.reset();
			_currentTween= new Tween(currentSlide, "alpha",_easing,0,1,_duration,true); 
			_currentTween.addEventListener(TweenEvent.MOTION_FINISH, function(e:TweenEvent) { fnComplete(); });
		}
		
	}
	
}