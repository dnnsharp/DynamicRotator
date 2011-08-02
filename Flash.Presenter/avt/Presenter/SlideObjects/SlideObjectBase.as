package avt.Presenter.SlideObjects {
	
	import avt.Presenter.Slide;
	import br.com.stimuli.loading.BulkLoader;
	import flash.display.Sprite;
	import fl.transitions.Tween;
	import avt.Util.PositionHelper;
	import flash.events.Event;
	
	
	public class SlideObjectBase extends Sprite {

		public function SlideObjectBase(slide:Slide) {
			_slide = slide;
		}
		
		private var _slide:Slide;
		private var _initProps:Object;
		private var _animations:Vector.<ObjectAnimation>;
		private var _bInit: Boolean;

		public static function factory(slide:Slide, config:*, loader:BulkLoader):SlideObjectBase {
			var obj:SlideObjectBase = null;
			
			if (config.objType && config.objType != undefined){
				switch (config.objType.toString()) {
					case "graphic":
						obj = new GraphicObject(slide);
						break;
				}
			}
			
			if (obj != null) {
				obj.parseConfiguration(config, loader);
			}
			
			return obj;
		}

		public function parseConfiguration(config:*,loader:BulkLoader): void {
			trace("  > Parsing object...");
			
			_bInit = false;
			
			_initProps = new Object();
			if (config.props && config.props != undefined) {
				var initProps:* = config.props.children ? config.props.children() : config.props;
				for (var iProp in initProps) {
					var nodeName:String = isNaN(parseInt(iProp)) ? iProp : initProps[iProp].name();
					_initProps[nodeName] = initProps[iProp];
				}
			} else {
				_initProps = new Object();
				_initProps.x = 0;
				_initProps.y = 0;
			}

			_animations = new Vector.<ObjectAnimation>();						
			if (config.animations && config.animations != undefined) {
				
				var objArr = config.animations.length != undefined ? config.animations : config.animations.children();
				var objArrLen = config.animations.length != undefined ? config.animations.length : config.animations.children().length();
				
				for (var i=0; i<objArrLen; i++) {
					var anim:ObjectAnimation = new ObjectAnimation(this, objArr[i]);
					_animations.push(anim);
				}
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
				_initProps["y"] = this["y"] = PositionHelper.getRealY(this, _slide.presentation.slideHeight, _initProps["y"], "top", _initProps["marginTop"], _initProps["marginBottom"]);
			}
			
			_bInit = true;
		}
		
		public function startAnimations():void {
			for (var i=0; i<_animations.length; i++) {
				_animations[i].start();
			}
		}
		
		public function cancelAnimations():void {
			for (var i=0; i<_animations.length; i++) {
				_animations[i].stop();
			}
		}

	}
	
}
