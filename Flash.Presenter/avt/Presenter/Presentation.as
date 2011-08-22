package avt.Presenter {
	
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.events.Event;
	import com.adobe.serialization.json.JSON;
	import avt.Util.Random;
	import avt.Util.FileUtils;
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import avt.Presenter.AdvanceSlide.*;
	import avt.Presenter.Loaders.BaseLoader;
	import avt.Util.ConfigUtils;
	import avt.Util.FontManager;
	import avt.Presenter.Backgrounds.IBackground;
	import avt.Presenter.Backgrounds.BackgroundFactory;
	
	public class Presentation {

		public function Presentation(container:Sprite) {
			_container = container;
			
			_slideContainer = new Sprite();
			_container.addChild(_slideContainer);
			
			_overlay = new Sprite();
			_container.addChild(_overlay);
		}
		
		
		public const LOADSTATE_INIT:int = 0;
		public const LOADSTATE_LOADER:int = 1;
		public const LOADSTATE_RESOURCES:int = 2;
		public const LOADSTATE_SLIDES:int = 3;
		
		private var _loadingState:int = LOADSTATE_INIT;
		
		private var _loader:BaseLoader;
		public function get loader():BaseLoader { return _loader; }
		
		private var _background:IBackground;
		public function get background():IBackground { return _background; }
		
		private var _container:Sprite;
		public function get container():Sprite { return _container; }
		
		private var _overlay:Sprite;
		public function get overlay():Sprite { return _overlay; }
		
		private var _slideContainer:Sprite;
		public function get slideContainer():Sprite { return _slideContainer; }
		
		private var _debug:Boolean;
		public function get debug():Boolean { return _debug; }
		public function set debug(val:Boolean):void { _debug = val; }
		
		private var _debugShowTitles:Boolean;
		public function get debugShowTitles():Boolean { return _debugShowTitles; }
		public function set debugShowTitles(val:Boolean):void { _debugShowTitles = val; }
		
		private var _slideWidth:int;
		public function get slideWidth():int { return _slideWidth; }
		public function set slideWidth(w:int):void { _slideWidth = w; }
		
		private var _slideHeight:int;
		public function get slideHeight():int { return _slideHeight; }
		public function set slideHeight(w:int):void { _slideHeight = w; }
		
		//private var _defaultSlideDuration:int;
//		public function get defaultSlideDuration():int { return _defaultSlideDuration; }
		
		// private var _hasTimer:Boolean;
		// public function get hasTimer():Boolean { return _hasTimer; }
		
		private var _randomOrder:Boolean;
		public function get randomOrder():Boolean { return _randomOrder; }
		public function set randomOrder(r:Boolean):void { _randomOrder = r; }
		
		private var _loop:Boolean;
		public function get loop():Boolean { return _loop; }
		public function set loop(l:Boolean):void { _loop = l; }
		
		private var _advanceSlide:Vector.<IAdvanceSlide>;
		public function get advanceSlide():Vector.<IAdvanceSlide> { return _advanceSlide; }
		
		private var _isTimerEnabled:Boolean = false;
		public function get isTimerEnabled():Boolean { return _isTimerEnabled; }
		
			
		private var _slides:Vector.<Slide>;
		public function get slides():Vector.<Slide> { return _slides; }
		
		private var _prevSlide:Slide = null;
		public function get prevSlide():Slide { return _prevSlide; }
		
		private var _currentSlideIndex:int = -1;
		
		private function get nextSlideIndex():int {
			var nextSlideIndex = _currentSlideIndex + 1;
			if (nextSlideIndex >= _slides.length)
				nextSlideIndex = 0;
			
			return nextSlideIndex;
		}
		
		
		public function goToNextSlide(opts:Object):Slide {
			if (!_slides || _slides.length == 0) {
				throw new Error("There are no slides loaded!");
			}
			
			if (_currentSlideIndex != -1 && _slides[_currentSlideIndex] != null) {
				_prevSlide = _slides[_currentSlideIndex];
				stopAllSlideAdvanceListeners();
				
				// if there's only one slide don't bother to hide it and show it back
				if (_slides.length == 1)
					return _slides[_currentSlideIndex];
			}
			
			_currentSlideIndex = nextSlideIndex;
			_slides[_currentSlideIndex].render(opts);
			
			return _slides[_currentSlideIndex];
		}
		
		public function get currentSlide():Slide {
			return _slides[_currentSlideIndex];
		}
		
		public function Load(strPath:String) : void {
			trace("> Loading configuration file from "+ strPath +"...");
			
			var myTextLoader:URLLoader = new URLLoader();

			myTextLoader.addEventListener(Event.COMPLETE, function(event:Event) {
				trace("  Done.");
				trace("> Determine configuration file format...");
				
				var settings:* = FileUtils.parseConfigurationObject(event.target.data, strPath);
				if (settings == null) {
					return;
				}
				
				parseConfiguration(settings);
				
			});
			
			// TODO: handle error
			myTextLoader.load(new URLRequest(strPath));

		}
		
	
		private function parseConfiguration(config:*): void {
			trace("> Setting up presentation...");
			
			trace("> Parsing resources... ");
			var fm:FontManager = new FontManager(fontsLoaded);
			fm.parseConfig(config.hasOwnProperty("resources") ? config.resources.fonts : null);
			
			//try { this.debug = config.setup.debug.toString().toLowerCase() == "true"; } catch (e:Error) { this.debug = false; }
//			trace("  debug: " + this.debug);
			
			try { this.slideWidth = parseInt(config.setup.slideWidth); } catch (e:Error) { this.slideWidth = 600; }
			trace("  width: " + this.slideWidth);
			
			try { this.slideHeight = parseInt(config.setup.slideHeight); } catch (e:Error) { this.slideHeight = 200; }
			trace("  height: " + this.slideHeight);
			
			try { this.randomOrder = config.setup.randomOrder.toString().toLowerCase() == "true"; } catch (e:Error) { this.randomOrder = false; }
			trace("  randomOrder: " + this.randomOrder);
			
			try { this.loop = config.setup.loop.toString().toLowerCase() == "true"; } catch (e:Error) { this.loop = false; }
			trace("  loop: " + this.loop);
			
			try { debugShowTitles = ConfigUtils.parseBoolean(config.setup.debug.showTitles, false); } catch (err:Error) { }
			trace("  debug.showTitles: " + debugShowTitles);
			
			_container.stage.scaleMode  = StageScaleMode.NO_SCALE;
			_container.stage.align = StageAlign.TOP_LEFT;
			
			trace("> Parse Loader");
			_loader = new BaseLoader(this);
			_loader.parseConfiguration(config.setup.loader);
			overlay.addChild(_loader);
			_loader.show();
			
			trace("> Parse main background"); 
			_background = BackgroundFactory.factory(config.setup.background);
			_background.addTo(slideContainer);
			_background.redraw(slideWidth, slideHeight);
			
			trace("> Parse advance slide options...");
			
			_advanceSlide = new Vector.<IAdvanceSlide>();
			
			try { advanceSlide.push(new AdvanceSlideClick(this, config.setup.advancePresentation.manual.click)); } catch (err:Error) { }
			try { 
				advanceSlide.push(new AdvanceSlideTimer(this, config.setup.advancePresentation.timing)); 
				_isTimerEnabled = (advanceSlide[advanceSlide.length-1] as AdvanceSlideTimer).enabled;
			} catch (err:Error) { }
			
			if (advanceSlide.length == 0) {
				trace("  > No advance timing event defined, defaulting to timing 10 seconds");
				advanceSlide.push(new AdvanceSlideTimer(this, null));
				_isTimerEnabled = true;
			}
			
			//
//			if (_advanceSlide.length == 0) {
//				trace("  > No advance event defined, defaulting to timings (10 seconds)");
//				advanceSlide.push(new AdvanceSlideTimer(this, null));
//			}
//			
//			try {
//				var advTimer:AdvanceSlideTimer = new AdvanceSlideTimer(this, config.setup.advancePresentation.timing);
//				advanceSlide.push(advTimer);
//				_defaultSlideDuration = advTimer.defaultDuration;
//			} catch (err:Error) { }
			
			
			//_hasTimer = true;
//			try { _hasTimer = config.setup.advancePresentation.timing.enabled.toString().toLowerCase() == "true"; } catch (err:Error) { 
//				try { _hasTimer = config.setup.advancePresentation.timing.toString().toLowerCase() == "true"; } catch (errInner:Error) { _hasTimer = true; }
//			}
//			
//			if (_hasTimer) {
//				var advTimer:AdvanceSlideTimer = new AdvanceSlideTimer(this, config.setup.advancePresentation.timing);
//				advanceSlide.push(advTimer);
//				_defaultSlideDuration = advTimer.defaultDuration;
//			}
			
			//var apByClick:String = "";
//			try { apByClick = config.setup.advancePresentation.manual.click.enabled.toString().toLowerCase(); }
//				catch (err:Error) { try { apByClick = config.setup.advancePresentation.manual.click.toString().toLowerCase(); } catch (errInner:Error) { } }
//				
//			if (apByClick == "true") {
//				advanceSlide.push(new AdvanceSlideClick(this, config.setup.advancePresentation.manual.click));
//			}
//			
//			if (_advanceSlide.length == 0) {
//				trace("  > No advance event defined, defaulting to timings (10 seconds)");
//				_hasTimer = true;
//				advanceSlide.push(new AdvanceSlideTimer(this, null));
//			}
			
			
			this._slides = new Vector.<Slide>();
			var slidesArr = config.slides.length != undefined ? config.slides : config.slides.children();
			var slidesArrLen = config.slides.length != undefined ? config.slides.length : config.slides.children().length();
			
			trace("> Parsing "+ slidesArrLen +" slides...");
			
			for (var i:int=0; i < slidesArrLen; i++) {
				
				var s:Slide = new Slide(this, slidesArr[i]);
				
				//s.parseConfiguration(config.slides[i]);
				s.originalIndex = s.viewIndex = i + 1;
				slides.push(s);
				
				slideContainer.addChild(s);
			}
			
			if (this.randomOrder) {
				trace("> Random order detected, reorder array...");
				this._slides = suffleSlides();
				trace("  Done, new order: ");
				for (var ir:int=0; ir < slides.length; ir++) {
					trace("  - "+ slides[ir].originalIndex + ": " + slides[ir].title);
				}
			}
			
			trace("> Done loading configuration, render first slide");
			
			
			//_container.stage.scaleMode = StageScaleMode.NO_SCALE;
            //_container.stage.align = StageAlign.TOP_LEFT;
			
			goToNextSlide({});
			
			if (_loadSlidesWhenPossible) {
				slides[0].load(slideFinishedLoading);
			}
		}
		
		private var _loadSlidesWhenPossible = false;
		public function fontsLoaded() {
			if (slides) {
				slides[0].load(slideFinishedLoading);
			} else {
				// slides aren't loaded yet, delay until they are
				_loadSlidesWhenPossible = true;
			}
		}
		
		public function slideFinishedLoading(slide:Slide) {
			// load next slide
			if (slide.viewIndex < slides.length) {
				slides[slide.viewIndex].load(slideFinishedLoading);
			}
		}
		
		public function notifySlideVisible(slide:Slide) {
			
			// if there's only one slide don't bother to hide it and show it back
			if (_slides.length == 1)
				return ;
					
			for (var ias:int = 0; ias < advanceSlide.length; ias++) {
				advanceSlide[ias].startListen(currentSlide);
			}
		}
		
		public function stopAllSlideAdvanceListeners() {
			for (var ias:int = 0; ias < advanceSlide.length; ias++) {
				advanceSlide[ias].endListen(currentSlide);
			}
		}
		
		public function suffleSlides():Vector.<Slide> {
			var newSlides:Vector.<Slide> = new Vector.<Slide>(slides.length);
			for (var ir:int=0; ir < slides.length; ir++) {
				var newIndex:int = Random.randomNumber(0, slides.length - 1);
				while (newSlides[newIndex]!= null) {
					newIndex = Random.randomNumber(0, slides.length - 1);
				}
				newSlides[newIndex] = slides[ir];
				newSlides[newIndex].viewIndex = ir;
			}
			
			return newSlides;
		}
		
		

	}
	
}
