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
	import flash.display.Loader;
	import avt.Presenter.SlideObjects.*;
	import br.com.stimuli.loading.BulkLoader;
	import br.com.stimuli.loading.BulkProgressEvent;
	import avt.Presenter.Backgrounds.*;
	import avt.Presenter.AdvanceSlide.AdvanceSlideTimer;
	import avt.Util.ConfigUtils;
	import flash.events.MouseEvent;
	import flash.net.navigateToURL;
	
	public class Slide extends Sprite {

		public function Slide(presentation:Presentation, config:*) {
			_presentation = presentation;
			this.visible = false;
			_config = config;
		}
		
		private var _presentation:Presentation;
		public function get presentation():Presentation { return _presentation; }
		
		// private var _mcBg:MovieClip;
		
		private var _loaded:Boolean;
		public function get loaded():Boolean { return _loaded; }
		public function set loaded(l:Boolean):void { _loaded = l; }
		
		private var _config: *;		
		public function get config():* { return _config; }
		
		private var _loader: BulkLoader;
		
		private var _renderOnLoad:Boolean = false;
		private var _renderOpts:Object = {};
		
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
		
		private var _background:IBackground;
		
		private var _objects:Vector.<SlideObjectBase>;
		public function get objects():Vector.<SlideObjectBase> { return _objects; }
		
			
		
		public function load(fnComplete:Function):void {
			
			trace("> Loading slide "+ originalIndex +"...");
			loaded = false;
						
			if (config.src && config.src != undefined) {
				trace("  ...configuration is in external file, loading " + config.src);
				this.title = "Loading from external source...";
				
				var slideConfigLoader:URLLoader = new URLLoader();

				slideConfigLoader.addEventListener(Event.COMPLETE, function(e:Event) {
					trace("> Loaded external slide config file.");

					var settings:* = FileUtils.parseConfigurationObject(e.target.data, config.src);
					if (settings == null) {
						return;
					}
					
					_config = settings;
					_parseConfiguration();
				});
				
				// TODO: handle error
				slideConfigLoader.load(new URLRequest(config.src));
				
			} else {
				_parseConfiguration();
			}
			
		}
		
		
		private function _parseConfiguration(): void {
			
			trace("  > Parsing slide configuration...");
			
			this.title = config.title && config.title != undefined ? config.title : "Unnamed Slide";
			trace("    title: " + this.title);
			
			this.duration = ConfigUtils.parseNumber(config.duration, AdvanceSlideTimer.DefaultSlideDuration);				
			trace("    duration: " + this.duration);
			
			
			if (config.transition && config.transition != undefined) {
				trace("    Has transition of type " + config.transition.transition);
				_transition = SlideTransitionFactory.factory(_presentation, config.transition);
			} else {
				_transition = new SlideTransitionNone();
			}
			
			_background = BackgroundFactory.factory(config.background);
			_background.addTo(this, _presentation.slideWidth, _presentation.slideHeight);
			
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
			
			// parse slides objects
			_loader = new BulkLoader();
			this._objects = new Vector.<SlideObjectBase>();
			if (config.objects && config.objects != undefined) {
				
				var objArr = config.objects.length != undefined ? config.objects : config.objects.children();
				var objArrLen = config.objects.length != undefined ? config.objects.length : config.objects.children().length();
			
				trace("  > Parsing "+ objArrLen +" objects...");
				for (var i:int=0; i < objArrLen; i++) {
					var so:SlideObjectBase = SlideObjectBase.factory(this, objArr[i], _loader); 
					if (so) {
						_objects.push(so);
						addChild(so);
					}
				}
			}
			
			
			if (_loader.itemsTotal == 0) {
				trace("There are no slide objects resources to load for slide " + originalIndex + ".");
				presentation.loader.hide();
				presentation.slideFinishedLoading(this);

				loaded = true;
				if (_renderOnLoad) {
					_render(_renderOpts);
				}
			} else {
				
				var _thisSlide = this;
				_loader.addEventListener(BulkLoader.COMPLETE, function(evt:Event) {
					
					var _failedItems:Array = _loader.getFailedItems();
					if (_failedItems.length > 0) {
						trace("!FAILED ITEMS: ");
						
						for (var i=0; i< _failedItems.length; i++) {
							trace(" - " + _failedItems[i]);
						}
					} else {
						trace("  All slide objects loaded fine...");
					}
										 
					presentation.loader.hide();
					presentation.slideFinishedLoading(_thisSlide);
					loaded = true;
					if (_renderOnLoad) {
						_render(_renderOpts);
					}
				});
				
				_loader.addEventListener(BulkLoader.PROGRESS, function(e:BulkProgressEvent) {
					presentation.loader.update(e.percentLoaded);
				});
					
				_loader.start();
			}
			
			
			//// setup graphics
//			// TODO: backgrounder class?
//			_mcBg = new MovieClip();
//			_mcBg.graphics.beginFill(0xff0000);
//			_mcBg.graphics.drawRect(0, 0, _presentation.slideWidth, _presentation.slideHeight);
//        	_mcBg.graphics.endFill();
//			_mcBg.alpha=0.2;
//			addChild(_mcBg);
//			
//			
			if (_presentation.debugShowTitles) {
				var textFormat:TextFormat = new TextFormat("verdana", 14)
				var textField:TextField = new TextField()
				textField.defaultTextFormat = textFormat;
				textField.text = title.replace("\\n", "\n");
				textField.width = _presentation.slideWidth;
				textField.selectable = false;
				addChild(textField);
			}
			
			
			//var colorTransform:ColorTransform = this.transform.colorTransform;
//			colorTransform.color = 0xCC6666;
//			this.transform.colorTransform = colorTransform;

			
			//this.graphics.beginFill(0xff0000);
//        this.graphics.drawCircle(50,50,50); // parameters(x,y,radius)
//        this.graphics.endFill();
		
			//opaqueBackground = 0xf2f2ff;
			
		}
		

		public function render(opts:Object):void {
			if (!loaded) {
				trace("> Render called but slide is not loaded; show loader until everything loads...");
				_renderOnLoad = true;
				presentation.loader.show();
				_renderOpts = opts;
				return;
			}
			
			_render(opts);
		}
		
		public function reset():void {
			x=0;
			y=0;
			alpha = 1;
			visible = true;
			
			for (var i=0; i < objects.length; i++) {
				objects[i].reset();
			}
		}
		
		private function _render(opts:Object):void {
			trace("> Render slide " + originalIndex + ": " + title);
			
			if (_presentation.prevSlide && opts.completeObjectTransitions == true) {
				_presentation.prevSlide.hideObjects( function() { __render(opts); } );
			} else {
				__render(opts);
			}
		}
		
		private function __render(opts:Object):void {
			this.parent.setChildIndex(this, this.parent.numChildren - 1);
			_transition.transition(_presentation.prevSlide, this, onTransitionComplete);
			
			for (var i=0; i < objects.length; i++) {
				objects[i].scheduleShow();
			}
		}
		
		private var _objToHide:int;
		private var _fnHideObjectComplete:Function;
		
		private function hideObjects(fnComplete:Function):void {
			
			_objToHide = objects.length;
			if (_objToHide == 0) {
				fnComplete();
				return;
			}
			
			_fnHideObjectComplete = fnComplete;
			for (var obj in objects) {
				objects[obj].hide();
			}
		}
		
		public function notifyObjectHidden(obj:SlideObjectBase):void {
			_objToHide--;
			if ( _objToHide == 0) {
				if (_fnHideObjectComplete != null) 
					_fnHideObjectComplete();
			}
		}
		
		private function onTransitionComplete():void {
			_presentation.notifySlideVisible(this);
		}

	}
	
}
