package avt.Presenter.SlideObjects {
	import fl.transitions.Tween;
	import avt.Util.EasingHelper;
	import flash.utils.setTimeout;
	import flash.utils.clearTimeout;
	
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
		
		public function ObjectAnimation(slideObject:SlideObjectBase, config:*) {
			_slideObject = slideObject;
			
			trace("    > Parsing animation...");
			
			_prop = config.prop;
			if (!slideObject.hasOwnProperty(_prop)) {
				trace("    Invalid property " + _prop);
				return;
			}
			
			try { _start = parseInt(config.start)/1000; } catch (err:Error) { _start = 0; }
			try { _duration = parseInt(config.duration)/1000; } catch (err:Error) { _duration = 2000; }
			try { _from = parseFloat(config.from); } catch (err:Error) { _from = NaN; }
			try { _to = parseFloat(config.to); } catch (err:Error) { }
			_easing = config.easing;
			
			trace("      prop: " + _prop);
			trace("      easing: " + _easing);
			trace("      start: " + _start);
			trace("      duration: " + _duration);
			trace("      from: " + _from);
			trace("      to: " + _to);
			
		}
		
		public function start():void {
			_timer = setTimeout(startTween, _start * 1000);
		}
		
		public function stop():void {
			clearTimeout(_timer);
		}
		
		private function startTween():void {
			var from = _from;
			if (!from)
				from = _slideObject[_prop];
			
			_tween = new Tween(_slideObject, _prop, EasingHelper.fromString(_easing), from, _to, _duration, true);
		}
		
	}
	
}