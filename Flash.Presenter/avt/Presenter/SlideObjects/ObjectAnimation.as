package avt.Presenter.SlideObjects {
	import fl.transitions.Tween;
	import avt.Util.TransitionUtils;
	import flash.utils.setTimeout;
	import flash.utils.clearTimeout;
	import avt.Util.ConfigUtils;
	
	public class ObjectAnimation {
		
		var _slideObject:SlideObjectBase;
		
		var _prop:String;
		var _easing:String;
		var _start:Number;
		var _duration:Number;
		var _from:Number;
		var _to:Number;
		
		var _tween:Tween;
		var _timer:Number;
		var _initalValue:* = null;
		
		public function ObjectAnimation(slideObject:SlideObjectBase, config:*) {
			_slideObject = slideObject;
			
			trace("    > Parsing animation...");
			
			_prop = config.prop;
			if (!slideObject.hasOwnProperty(_prop)) {
				trace("    Invalid property " + _prop);
				return;
			}
			
			_start = ConfigUtils.parseNumberFromProp(config, "start", 0);
			_duration = ConfigUtils.parseNumberFromProp(config, "duration", 2000);
			_from = ConfigUtils.parseNumberFromProp(config, "from", NaN);
			_to = ConfigUtils.parseNumberFromProp(config, "to", NaN);
			_easing = ConfigUtils.parseStringFromProp(config, "easing", "None.easeNone");
			//try { _start = parseInt(config.start)/1000; } catch (err:Error) { _start = 0; }
			//try { _duration = parseInt(config.duration)/1000; } catch (err:Error) { _duration = 2000; }
			//try { _from = parseFloat(config.from); } catch (err:Error) { _from = NaN; }
			//try { _to = parseFloat(config.to); } catch (err:Error) { }
			// try { _easing = config.easing.toString(); } catch (err:Error) { _easing = "None.easeNone"; }
			
			trace("      prop: " + _prop);
			trace("      easing: " + _easing);
			trace("      start: " + _start);
			trace("      duration: " + _duration);
			trace("      from: " + _from);
			trace("      to: " + _to);
			
		}
		
		public function scheduleStart():void {		
			_timer = setTimeout(startTween, _start);
		}
		
		public function stop():void {
			clearTimeout(_timer);
			
			if (_tween)
				_tween.stop();
				
			if (_initalValue != null)
				_slideObject[_prop] = _initalValue;
		}
		
		private function startTween():void {
			var from = _from;
			if (!from)
				from = _slideObject[_prop];
			
			_initalValue = _slideObject[_prop];
			_tween = new Tween(_slideObject, _prop, TransitionUtils.easingFromString(_easing), from, _to, _duration / 1000, true);
		}
		
	}
	
}