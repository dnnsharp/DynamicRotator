package avt.Presenter.Loaders {
	import flash.display.Sprite;
	import avt.Presenter.Presentation;
	import flash.display.Loader;
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.net.URLRequest;
	import avt.Util.PositionHelper;
	
	
	public class BaseLoader extends Sprite {

		public function BaseLoader(presentation:Presentation) {
			_presentation = presentation;

		}
		
		private var _presentation:Presentation;
		public function get presentation():Presentation { return _presentation; }
		
		private var _loader:Loader;
		private var _initProps:Object;
		private var _mc:MovieClip = null;
		private var _mcBg:MovieClip;
		private var _syncProgressWithTimeline:Boolean;
		
		public function show():void {
			this.visible = true;
			if (_mc != null) {
				_mc.gotoAndPlay(0);
			}
		}
		
		public function hide():void {
			this.visible = false;
		}
		
		public function update(progress:Number):void {
			
			if (_mc == null || !_syncProgressWithTimeline)
				return;
				
			_mc.gotoAndStop(Math.floor(progress * _mc.totalFrames));
		}
				
		public function parseConfiguration(config:*): void {
		
			if (!config || config == undefined) {
				trace("  No configuration found, no loader animation will be displayed");
				return;
			}
			
			_initProps = new Object();
			if (config.props && config.props != undefined) {
				var initProps:* = config.props.children ? config.props.children() : config.props;
				for (var iProp in initProps) {
					var nodeName:String = isNaN(parseInt(iProp)) ? iProp : initProps[iProp].name();
					_initProps[nodeName] = initProps[iProp];
				}
			} else {
				trace("  Properties not found, defaulting to center coordinates");
				_initProps = new Object();
				_initProps.x = NaN;
				_initProps.y = NaN;
			}
			
			_syncProgressWithTimeline = config.syncProgressWithTimeline.toString() == "true";
			
			if (config.src && config.src != undefined) {
				trace("  Loading resource from " + config.src + "...");
				_loader = new Loader();
				var _this = this;
				_loader.contentLoaderInfo.addEventListener(Event.COMPLETE, function onLoaderReady(e:Event) {     
					
					trace("> Done loading loader. Now setting its properties...");
					
					// the image is now loaded, so let's add it to the display tree!     
					_this.addChild(_loader);
					 
					for (var i in _initProps) {
						
						if (i.indexOf("margin") == 0)
							continue;
					
						if (!_this.hasOwnProperty(i)) {
							trace("    Invalid loader property " + i);
						} else {
							trace("  "+i+": " + _initProps[i]);
							_this[i] = _initProps[i];
						}
					}
					
					_initProps["x"] = _this["x"] = PositionHelper.getRealX(_this, presentation.slideWidth, _initProps["x"], "center", _initProps["marginLeft"], _initProps["marginRight"]);
					_initProps["y"] = _this["y"] = PositionHelper.getRealY(_this, presentation.slideHeight, _initProps["y"], "center", _initProps["marginTop"], _initProps["marginBottom"]);
					
					_mc = (_loader.getChildAt(0) as MovieClip).getChildAt(0) as MovieClip;
				});
				
				var fileRequest:URLRequest = new URLRequest(config.src);
				_loader.load(fileRequest);
			}

		}
		
	}
	
}
