package avt.Presenter.AdvanceSlide {
	
	import flash.display.Sprite;
	import flash.utils.setTimeout;
	import flash.utils.clearTimeout;
	import avt.Presenter.Slide;
	import avt.Presenter.Presentation;
	import avt.Presenter.Config.IConfigurable;
	import avt.Util.ConfigUtils;
	
	public class AdvanceSlideTimer implements IAdvanceSlide, IConfigurable {
		
		private var _autoStart:Boolean;
		public function get autoStart():Boolean { return _autoStart; }
		public function set autoStart(val:Boolean):void { _autoStart = val; }	
		
		private var _enabled:Boolean;
		public function get enabled():Boolean { return _enabled; }
		public function set enabled(val:Boolean):void { _enabled = val; }	
		
		private var _defaultDuration:int;
		public function get defaultDuration():int { return _defaultDuration; }
		public function set defaultDuration(val:int):void { _defaultDuration = val; }	
		
		private var _presentation:Presentation;
		private var _timer:uint;
		
		public static const DEFAULT_TIMING_MS:int = 10000; // 10 seconds
		public static var DefaultSlideDuration:int = DEFAULT_TIMING_MS;
		
		public function AdvanceSlideTimer(presentation:Presentation, config:*) {
			_presentation = presentation;
			parseConfig(config);
		}
		
		public function parseConfig(config:*): void {
			
			try { enabled = config.enabled.toString().toLowerCase() == "true"; } catch (err:Error) { 
				try { enabled = config.toString().toLowerCase() == "true"; } catch (errInner:Error) { enabled = true; }
			}
			
			try { autoStart = ConfigUtils.parseBoolean(config.autoStart, true); } catch (err:Error) { }
			try { DefaultSlideDuration = defaultDuration = ConfigUtils.parseNumber(config.defaultTiming, DEFAULT_TIMING_MS); } catch (err:Error) { }

			trace("  > Advance by timings (enabled: "+ enabled +"; autostart: " + autoStart +"; defaultDuration: " + defaultDuration + ")");
		}
		
		public function startListen(currentSlide:Slide):void {

			_timer = setTimeout(function() {
					if (!enabled)
						return;
					_presentation.goToNextSlide({});
				}, currentSlide.duration > 0 ? currentSlide.duration : defaultDuration);
		}
		
		public function endListen(currentSlide:Slide):void {			
			clearTimeout(_timer);
		}
	}
	
}