package avt.Presenter.SlideObjects {
	
	import avt.Presenter.Slide;
	import br.com.stimuli.loading.BulkLoader;
	import flash.display.MovieClip;
	import fl.transitions.Tween;
	import avt.Util.PositionHelper;
	import flash.events.Event;
	import fl.transitions.Transition;
	import flash.utils.setTimeout;
	import flash.utils.clearTimeout;
	import avt.Util.ConfigUtils;
	import fl.transitions.TransitionManager;
	import flash.geom.Transform;
	import fl.transitions.Iris;
	import flash.events.MouseEvent;
	import flash.net.navigateToURL;
	import flash.net.URLRequest;
	import avt.Presenter.Backgrounds.IBackground;
	import avt.Presenter.Backgrounds.BackgroundFactory;
	
	
	public class SlideObjectBase extends MovieClip {

		public function SlideObjectBase(slide:Slide) {
			_slide = slide;
		}
		
		private var _slide:Slide;
		
		private var _objectId:String;
		public function get objectId():String { return _objectId; }
		
		private var _initProps:Object;
		private var _animations:Vector.<ObjectAnimation>;
		private var _enterSceneAtTime:int;
		private var _leaveSceneAtTime:int;
		
		private var _transitionManager:TransitionManager;
		
		private var _transitionsIn:Vector.<ObjectTransition>;
		private var _transitionsInDuration:Number = 0;
		
		private var _transitionsOut:Vector.<ObjectTransition>;
		private var _transitionsOutDuration:Number = 0;
		
		private var _bInit: Boolean;
		private var _timerShowHide: uint;
		
		private static var _OBJECTID = 0;
		
		private var _background:IBackground;

		public static function factory(slide:Slide, config:*, loader:BulkLoader):SlideObjectBase {
			var obj:SlideObjectBase = null;
			
			if (config.objType && config.objType != undefined){
				switch (config.objType.toString()) {
					case "graphic":
						obj = new GraphicObject(slide);
						break;
					case "text":
					case "html":
						obj = new HtmlText(slide);
						break;
				}
			}
			
			if (obj != null) {
				obj._objectId = "obj" + (_OBJECTID++);
				obj.parseConfiguration(config, loader);
			}
			
			return obj;
		}

		public function parseConfiguration(config:*,loader:BulkLoader): void {
			trace("  > Parsing object...");
			
			_bInit = false;
			
			_initProps = new Object();
			_initProps.x = "center";
			_initProps.y = "center";
				
			if (config.props && config.props != undefined) {
				trace("    > Parsing inital parameters: ");
				var initProps:* = config.props.children ? config.props.children() : config.props;
				for (var iProp in initProps) {
					var nodeName:String = isNaN(parseInt(iProp)) ? iProp : initProps[iProp].name();
					_initProps[nodeName] = initProps[iProp];
					trace("      " + nodeName + ": " + initProps[iProp])
				}
			} else {
				trace("      no properties found, default position to (center,center)");
			}
			
			_background = BackgroundFactory.factory(config.background);
			_background.addTo(this, 200, 200);
			
			// when to enter scene
			_enterSceneAtTime = 0;
			_transitionsIn = new Vector.<ObjectTransition>();
			if (config.enterScene && config.enterScene != undefined) {
				_enterSceneAtTime = ConfigUtils.parseNumber(config.enterScene.atTime, 0);
				
				if (config.enterScene.transitions && config.enterScene.transitions != undefined) {
					
					var tArrIn = config.enterScene.transitions.length != undefined ? config.enterScene.transitions: config.enterScene.transitions.children();
					var tArrInLen = config.enterScene.transitions.length != undefined ? config.enterScene.transitions.length : config.enterScene.transitions.children().length();
					
					for (var itin=0; itin<tArrInLen; itin++) {
						var tIn:ObjectTransition = new ObjectTransition(this, tArrIn[itin], Transition.IN);
						_transitionsIn.push(tIn);
						
						if (_transitionsInDuration < tIn.duration)
							_transitionsInDuration = tIn.duration;
					}
				}
			}
			trace("    > enterSceneAtTime: " + _enterSceneAtTime);
			
			// when to leave scene
			_leaveSceneAtTime = _slide.duration;
			_transitionsOut = new Vector.<ObjectTransition>();
			if (config.leaveScene && config.leaveScene != undefined) {
				_leaveSceneAtTime = ConfigUtils.parseNumber(config.leaveScene.atTime, _slide.duration);
				
				if (config.leaveScene.transitions && config.leaveScene.transitions != undefined) {
					
					var tArrOut = config.leaveScene.transitions.length != undefined ? config.leaveScene.transitions: config.leaveScene.transitions.children();
					var tArrOutLen = config.leaveScene.transitions.length != undefined ? config.leaveScene.transitions.length : config.leaveScene.transitions.children().length();
					
					for (var itout = 0; itout < tArrOutLen; itout++) {
						var tOut:ObjectTransition = new ObjectTransition(this, tArrOut[itout], Transition.OUT);
						_transitionsOut.push(tOut);
					
						if (_transitionsOutDuration < tOut.duration)
							_transitionsOutDuration = tOut.duration;
					}
				}
			}
			trace("    > _leaveSceneAtTime: " + _leaveSceneAtTime);

			// parse animations
			_animations = new Vector.<ObjectAnimation>();						
			if (config.animations && config.animations != undefined) {
				
				var objArr = config.animations.length != undefined ? config.animations : config.animations.children();
				var objArrLen = config.animations.length != undefined ? config.animations.length : config.animations.children().length();
				
				for (var i=0; i<objArrLen; i++) {
					var anim:ObjectAnimation = new ObjectAnimation(this, objArr[i]);
					_animations.push(anim);
				}
			}
			
			// parse link
			if (config.hasOwnProperty("link")) {
				
				var url = config.link.hasOwnProperty("url") ? config.link.url : config.link;
				var target = config.link.hasOwnProperty("target") ? config.link.target : "_self";
				
				trace("    > link set to " + url + " (target: "+ target +")");
				
				this.buttonMode = true;
   				this.useHandCursor = true;
   
				this.addEventListener(MouseEvent.CLICK, function(ev:MouseEvent) {
					navigateToURL(new URLRequest(url), target);
					ev.preventDefault();
					ev.stopPropagation();
					ev.stopImmediatePropagation();
				});
			}
		}
		
		public function reset():void {
			
			for (var i in _initProps) {
				
				// ignore special properties
				if (i.indexOf("margin") == 0)
					continue;
					
				if (!this.hasOwnProperty(i)) {
					trace("    Invalid property " + i);
				} else {
					this[i] = _initProps[i];
				}
			}
			
			if (!_bInit) {
				_initProps["x"] = this["x"] = PositionHelper.getRealX(this, _slide.presentation.slideWidth, _initProps["x"], "center", _initProps["marginLeft"], _initProps["marginRight"]);
				_initProps["y"] = this["y"] = PositionHelper.getRealY(this, _slide.presentation.slideHeight, _initProps["y"], "center", _initProps["marginTop"], _initProps["marginBottom"]);
			}
			
			_bInit = true;
			
			clearTimeout(_timerShowHide);
			//visible = false;
			
			//if (_leaveSceneAtTime != _slide.duration || _transitionsOut.length > 0) {
//				clearTimeout(_timerShowHide);
//				visible = false;
//			}
		}
		
		public function scheduleShow() {
			reset();
			visible = false;
			_timerShowHide = setTimeout(function() {
					_show();
				}, _enterSceneAtTime);
		}
		
		private function _show():void {
			
			if (_transitionsIn.length == 0) {
				trace("  > Showing object instantly.");
				visible = true;
			} else {
				trace("  > Showing object with transitions.");
				
				if (_transitionManager) {
					for (var tstop in _transitionManager.transitionsList) {
						(_transitionManager.transitionsList[tstop] as Transition).stop();
					}
				}
				
				_transitionManager = new TransitionManager(this);
				for (var it=0; it < _transitionsIn.length; it++) {
					_transitionsIn[it].start(_transitionManager);
				}
			}
			
			for (var i=0; i<_animations.length; i++) {
				_animations[i].scheduleStart();
			}
			
			// if there's only one slide don't bother to hide it and show it back
			if (_slide.presentation.slides.length == 1)
				return;
			
			if (_leaveSceneAtTime == _slide.duration && _transitionsOut.length == 0) {
				return;
			}
			
			// set timer to hide object
			if (_slide.presentation.isTimerEnabled) {
				_timerShowHide = setTimeout(function() {
					hide();
				}, _leaveSceneAtTime - _enterSceneAtTime - _transitionsOutDuration);
			}
		}
		
		public function hide() {
			
			clearTimeout(_timerShowHide);
			
			if (_transitionsOut.length == 0) {
				trace("  > Hiding object instantly.");
				_slide.notifyObjectHidden(this);
				//visible = false;
				
				for (var ia in _animations) {
					_animations[ia].stop();
				}
				
			} else {
				trace("  > Hiding object with transitions.");
				
				if (_transitionManager) {
					for (var tstop in _transitionManager.transitionsList) {
						(_transitionManager.transitionsList[tstop] as Transition).stop();
					}
				}

				_transitionManager = new TransitionManager(this);
				var _this = this;
				_transitionManager.addEventListener("allTransitionsOutDone", function() { _slide.notifyObjectHidden(_this); visible=false; });
				
				for (var it=0; it < _transitionsOut.length; it++) {
					_transitionsOut[it].start(_transitionManager);
				}
				
				for (var ias in _animations) {
					_animations[ias].stop();
				}
			}
		}
		
		
		//public function cancelAnimations():void {
//			for (var i=0; i<_animations.length; i++) {
//				_animations[i].stop();
//			}
//		}

	}
	
}
