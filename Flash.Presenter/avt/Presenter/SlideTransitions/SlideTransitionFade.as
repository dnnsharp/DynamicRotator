package avt.Presenter.SlideTransitions {
	
	import avt.Presenter.Slide;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.getDefinitionByName;
	import fl.transitions.TweenEvent;
	import avt.Presenter.Presentation;
	
	public class SlideTransitionFade implements ISlideTransition {
		
		public function SlideTransitionFade() {
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
		
		public function init(presentation:Presentation, config:*):void {
			
			try {
				_duration = parseInt(config.duration) / 1000;
				if (_duration<=0) 
					_duration = DefaultDuration / 1000;
			} catch (e:Error) { _duration = DefaultDuration/10000; }
			
			var strEasing:String = config.easing.toString();
			_easing = getDefinitionByName("fl.transitions.easing."+strEasing.substr(0, strEasing.indexOf('.')))[strEasing.substr(strEasing.indexOf('.')+1)] as Function;
			trace(_easing);
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
		
		//public function cancel(): void {
//			
//			trace("  > Cancel slide transition FADE");
//			
//			if (_prevTween) {
//				_prevTween.fforward();
//			}
//			
//			if (_currentTween) {
//				// _currentTween.fforward();
//			}
//		}
	}
	
}