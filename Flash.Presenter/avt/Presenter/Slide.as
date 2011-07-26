package avt.Presenter {
	
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.events.Event;
	import avt.Util.FileUtils;
	import com.adobe.serialization.json.JSON;
	import flash.text.TextField;
	import flash.display.MovieClip;
	import flash.text.TextFormat;
	import flash.geom.ColorTransform;
	import flash.display.Sprite;
	import avt.Presenter.SlideTransitions.*;
	
	public class Slide extends Sprite {

		public function Slide(presentation:Presentation) {
			_presentation = presentation;
			this.visible = false;
		}
		
		private var _presentation:Presentation;
		private var _mcBg:MovieClip;
		
		private var _loaded:Boolean;
		public function get loaded():Boolean { return _loaded; }
		public function set loaded(l:Boolean):void { _loaded = l; }
		
		private var _renderOnLoad:Boolean = false;
		
		private var _originalIndex:int;
		public function get originalIndex():int { return _originalIndex; }
		public function set originalIndex(i:int):void { _originalIndex = i; }	
		
		private var _viewIndex:int;
		public function get viewIndex():int { return _viewIndex; }
		public function set viewIndex(i:int):void { _viewIndex = i; }	
		
		private var _title:String;
		public function get title():String { return _title; }
		public function set title(t:String):void { _title = t; }
		
		private var _duration:int;
		public function get duration():int { return _duration; }
		public function set duration(i:int):void { _duration = i; }	
		
		private var _transition:ISlideTransition;
		public function get transition():ISlideTransition { return _transition; }
		
		public function parseConfiguration(config:*): void {
			trace("  > Parsing slide...");
			
			if (config.src) {
				trace("  ...configuration is in external file, loading " + config.src);
				this.title = "Loading from external source...";
				
				var slideConfigLoader:URLLoader = new URLLoader();

				slideConfigLoader.addEventListener(Event.COMPLETE, function(e:Event) {
					trace("> Loaded external slide config file.");

					var settings:* = FileUtils.parseConfigurationObject(e.target.data);
					if (settings == null) {
						return;
					}
					
					trace("  > Parsing externally loaded slide...");
					_parseConfiguration(settings);
					
				});
				
				// TODO: handle error
				slideConfigLoader.load(new URLRequest(config.src));
				
			} else {
				_parseConfiguration(config);
			}
		}
		
		private function _parseConfiguration(config:*): void {
			
			this.title = config.title ? config.title : "Unnamed Slide";
			trace("    title: " + this.title);
			
			try { this.duration = parseInt(config.duration); } catch (e:Error) { config.duration = -1; }
			trace("    duration: " + this.duration);
			
			if (config.transition) {
				_transition = SlideTransitionFactory.factory(_presentation, config.transition);
			} else {
				_transition = new SlideTransitionNone();
			}
			
			
			// setup graphics
			// TODO: backgrounder class?
			_mcBg = new MovieClip();
			_mcBg.graphics.beginFill(0xff0000);
			_mcBg.graphics.drawRect(0, 0, _presentation.slideWidth, _presentation.slideHeight);
        	_mcBg.graphics.endFill();
			_mcBg.alpha=0.2;
			addChild(_mcBg);
			
			
			var textFormat:TextFormat = new TextFormat("verdana", 24)
			var textField:TextField = new TextField()
			textField.defaultTextFormat = textFormat
    		textField.text = title;
			textField.width=300;
    		addChild(textField)
			
			var colorTransform:ColorTransform = this.transform.colorTransform;
			colorTransform.color = 0xCC6666;
			this.transform.colorTransform = colorTransform;
			
			
			
			
			//this.graphics.beginFill(0xff0000);
//        this.graphics.drawCircle(50,50,50); // parameters(x,y,radius)
//        this.graphics.endFill();
		
			//opaqueBackground = 0xf2f2ff;
			
			
			loaded = true;
			if (_renderOnLoad) {
				_render();
			}
		}
		
		public function render():void {
			if (!loaded) {
				trace("> Render called but slide is not loaded; show loader until everything loads...");
				_renderOnLoad = true;
				return;
			}
			
			_render();
		}
		
		public function reset():void {
			x=0;
			y=0;
			alpha = 1;
			visible = true;
		}
		
		private function _render():void {
			trace("> Render slide " + originalIndex + ": " + title);
			
			this.parent.setChildIndex(this, this.parent.numChildren - 1);
		
		
			_transition.transition(_presentation.prevSlide, this, onTransitionComplete);
		}
		
		private function onTransitionComplete():void {
			_presentation.notifySlideVisible(this);
		}

	}
	
}
