package avt.Presenter.SlideObjects {
	
	import fl.transitions.*;
	import avt.Util.TransitionUtils;
	import flash.utils.setTimeout;
	import flash.utils.clearTimeout;
	import fl.transitions.easing.Strong;
	
	public class ObjectTransition {
		
		private var _slideObject:SlideObjectBase;
		private var _props:Object;	
		
		public function get duration():int { return _props["duration"]*1000; }
		
		public function ObjectTransition(slideObject:SlideObjectBase, config:*, direction:uint) {
			
			_slideObject = slideObject;
			_props = new Object();
			
			_props["direction"] = direction;
			
			trace("    > Parsing transition...");
			
			var props:* = config.children ? config.children() : config;
			for (var iProp in props) {
				var nodeName:String = isNaN(parseInt(iProp)) ? iProp : props[iProp].name();
				trace("      "+ nodeName +": " + props[iProp]);
				switch (nodeName) {
					case "transition":
						_props["type"] = TransitionUtils.transtionFromString(props[iProp]);
						break;
					case "duration":
						_props["duration"] = parseInt(props[iProp]) / 1000;
						break;
					case "easing":
						_props["easing"] = TransitionUtils.easingFromString(props[iProp]);
						break;
					default:
						_props[nodeName] = props[iProp];
						break;
				}
			}
			
			 // _props = {type:Fly, direction:direction, duration:_props["duration"], easing:_props["easing"], startPoint:_props["startPoint"]};
			//for (var ip in _props) {
//				trace("      "+ ip +": " + _props[ip]);
//			}
		
		}
		
		public function start():void {
			TransitionManager.start(_slideObject, _props);
		}
//		
//		public function stop():void {
//			clearTimeout(_timer);
//		}
		
		//private function startTween():void {
//			var from = _from;
//			if (!from)
//				from = _slideObject[_prop];
//			
//			_tween = new Tween(_slideObject, _prop, EasingHelper.fromString(_easing), from, _to, _duration, true);
//		}
//		
	}
	
}