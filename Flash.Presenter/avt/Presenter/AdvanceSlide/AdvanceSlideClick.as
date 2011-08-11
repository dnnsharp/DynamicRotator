package avt.Presenter.AdvanceSlide {
	
	import flash.display.Sprite;
	import flash.events.MouseEvent;
	import avt.Presenter.Slide;
	import avt.Presenter.Presentation;
	import avt.Presenter.Config.IConfigurable;
	import avt.Util.ConfigUtils;
	
	public class AdvanceSlideClick implements IAdvanceSlide, IConfigurable {
		
		private var _presentation:Presentation;
		private var _completeObjectTransitions:Boolean;
		
		private var _enabled:Boolean;
		public function get enabled():Boolean { return _enabled; }
		public function set enabled(val:Boolean):void { _enabled = val;}	
		
		public function AdvanceSlideClick(presentation:Presentation, config:*) {
			_presentation = presentation;
			parseConfig(config);
		}
		
		public function parseConfig(config:*): void {

			if (config == null || config == undefined) {
				enabled = false;
				return; // nothing to parse
			}

			enabled = ConfigUtils.parseBoolean(config.hasOwnProperty("enabled") ? config.enabled : config, false);						
			_completeObjectTransitions = ConfigUtils.parseBooleanFromProp(config, "completeObjectTransitions", true);
			
			trace("  > Advance by slide click (enabled: "+ enabled +"; completeObjectTransitions: " + _completeObjectTransitions + ")");
		}
		
		public function startListen(currentSlide:Slide):void {
			currentSlide.stage.addEventListener(MouseEvent.CLICK, onSlideClick);
		}
		
		public function endListen(currentSlide:Slide):void {
			currentSlide.stage.removeEventListener(MouseEvent.CLICK, onSlideClick);
		}
		
		private function onSlideClick(event:MouseEvent):void {
			if (!enabled)
				return;
				
			_presentation.goToNextSlide({ completeObjectTransitions: _completeObjectTransitions });
		}
	}
	
}