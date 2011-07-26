package avt.Presenter.AdvanceSlide {
	
	import flash.display.Sprite;
	import flash.utils.setTimeout;
	import flash.utils.clearTimeout;
	import avt.Presenter.Slide;
	import avt.Presenter.Presentation;
	
	public class AdvanceSlideTimer implements IAdvanceSlide {
		
		private var _autoStart:Boolean;
		public function get autoStart():Boolean { return _autoStart; }
		public function set autoStart(i:Boolean):void { _autoStart = i; }	
		
		private var _defaultDuration:int;
		public function get defaultDuration():int { return _defaultDuration; }
		public function set defaultDuration(i:int):void { _defaultDuration = i; }	
		
		private var _presentation:Presentation;
		private var _timer:uint;
		
		public function AdvanceSlideTimer(presentation:Presentation, autoStart:Boolean, defaultDuration:int) {
			_presentation = presentation;
			this.defaultDuration = defaultDuration;
			this.autoStart = autoStart;
		}
		
		public function startListen(currentSlide:Slide):void {
			_timer = setTimeout(function() {
					_presentation.goToNextSlide();
				}, currentSlide.duration > 0 ? currentSlide.duration : defaultDuration);
		}
		
		public function endListen(currentSlide:Slide):void {
			clearTimeout(_timer);
		}
	}
	
}