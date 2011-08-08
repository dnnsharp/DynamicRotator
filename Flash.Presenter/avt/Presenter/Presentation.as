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
	
	public class Presentation {

		public function Presentation(container:Sprite) {
			_container = container;
			
			_slideContainer = new Sprite();
			_container.addChild(_slideContainer);
			
			_overlay = new Sprite();
			_container.addChild(_overlay);
		}
		
		const AdvancePresentation_TimingDefaultMs = 10000; // 10 seconds
		
		private var _loader:BaseLoader;
		public function get loader():BaseLoader { return _loader; }
		
		private var _container:Sprite;
		public function get container():Sprite { return _container; }
		
		private var _overlay:Sprite;
		public function get overlay():Sprite { return _overlay; }
		
		private var _slideContainer:Sprite;
		public function get slideContainer():Sprite { return _slideContainer; }
		
		private var _debug:Boolean;
		public function get debug():Boolean { return _debug; }
		public function set debug(d:Boolean):void { _debug = d; }
		
		private var _slideWidth:int;
		public function get slideWidth():int { return _slideWidth; }
		public function set slideWidth(w:int):void { _slideWidth = w; }
		
		private var _slideHeight:int;
		public function get slideHeight():int { return _slideHeight; }
		public function set slideHeight(w:int):void { _slideHeight = w; }
		
		private var _defaultSlideDuration:int;
		public function get defaultSlideDuration():int { return _defaultSlideDuration; }
		
		private var _randomOrder:Boolean;
		public function get randomOrder():Boolean { return _randomOrder; }
		public function set randomOrder(r:Boolean):void { _randomOrder = r; }
		
		private var _loop:Boolean;
		public function get loop():Boolean { return _loop; }
		public function set loop(l:Boolean):void { _loop = l; }
		
		private var _advanceSlide:Vector.<IAdvanceSlide>;
		public function get advanceSlide():Vector.<IAdvanceSlide> { return _advanceSlide; }
			
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
		
		
		public function goToNextSlide():Slide {
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
			_slides[_currentSlideIndex].render();
			
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
			
			try { this.debug = config.setup.debug.toString().toLowerCase() == "true"; } catch (e:Error) { this.debug = false; }
			trace("  debug: " + this.debug);
			
			try { this.slideWidth = parseInt(config.setup.slideWidth); } catch (e:Error) { this.slideWidth = 600; }
			trace("  width: " + this.slideWidth);
			
			try { this.slideHeight = parseInt(config.setup.slideHeight); } catch (e:Error) { this.slideHeight = 200; }
			trace("  height: " + this.slideHeight);
			
			try { this.randomOrder = config.setup.randomOrder.toString().toLowerCase() == "true"; } catch (e:Error) { this.randomOrder = false; }
			trace("  randomOrder: " + this.randomOrder);
			
			try { this.loop = config.setup.loop.toString().toLowerCase() == "true"; } catch (e:Error) { this.loop = false; }
			trace("  loop: " + this.loop);
			
			_container.stage.scaleMode  = StageScaleMode.NO_SCALE;
			_container.stage.align = StageAlign.TOP_LEFT;
			
			trace("> Parse Loader");
			_loader = new BaseLoader(this);
			_loader.parseConfiguration(config.setup.loader);
			overlay.addChild(_loader);
			_loader.show();
			
			trace("> Parse advance slide options...");
			
			_advanceSlide = new Vector.<IAdvanceSlide>();
			
			var apAllowTiming:Boolean = true;
			try { apAllowTiming = config.setup.advancePresentation.timing.enabled.toString().toLowerCase() == "true"; } catch (e:Error) { apAllowTiming = true; }
			
			_defaultSlideDuration = AdvancePresentation_TimingDefaultMs;
			
			if (apAllowTiming) {
				
				var apTimingAutoStart:Boolean = true;
				try { apTimingAutoStart = config.setup.advancePresentation.timing.autostart.toString().toLowerCase() == "true"; } catch (e:Error) { apTimingAutoStart = true; }
				try { _defaultSlideDuration = parseInt(config.setup.advancePresentation.timing.defaultTiming); } catch (e:Error) { _defaultSlideDuration = AdvancePresentation_TimingDefaultMs; }
				
				advanceSlide.push(new AdvanceSlideTimer(this, apTimingAutoStart, _defaultSlideDuration));
				trace("  > Advance by timings (autostart: " + apTimingAutoStart +"; defaultInterval: " + _defaultSlideDuration + ")");
			}
			
			var apByClick:String = "";
			try { apByClick = config.setup.advancePresentation.manual.click.toString().toLowerCase(); } catch (e:Error) { }
			if (apByClick == "true") {
				advanceSlide.push(new AdvanceSlideClick(this));
				trace("  > Advance by slide click");
			}
			
			if (_advanceSlide.length == 0) {
				trace("  > No advance event defined, defaulting to timings (10 seconds)");
				advanceSlide.push(new AdvanceSlideTimer(this, true, 10000));
			}
			
			
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
			

			slides[0].load(slideFinishedLoading);
			
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
			
			goToNextSlide();
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
