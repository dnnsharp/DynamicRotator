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
	
	public class Presentation {

		private var _container:Sprite;

		public function Presentation(container:Sprite) {
			_container = container;
		}
		
		const AdvancePresentation_TimingDefaultMs = 10000; // 10 seconds
		
		private var _debug:Boolean;
		public function get debug():Boolean { return _debug; }
		public function set debug(d:Boolean):void { _debug = d; }
		
		private var _slideWidth:int;
		public function get slideWidth():int { return _slideWidth; }
		public function set slideWidth(w:int):void { _slideWidth = w; }
		
		private var _slideHeight:int;
		public function get slideHeight():int { return _slideHeight; }
		public function set slideHeight(w:int):void { _slideHeight = w; }
		
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
		
		private var _currentSlide:int = -1;
		public function goToNextSlide():Slide {
			if (!_slides || _slides.length == 0) {
				throw new Error("There are no slides loaded!");
			}
			
			if (_currentSlide != -1 && _slides[_currentSlide] != null) {
				_slides[_currentSlide].visible = false;
				stopAllSlideAdvanceListeners();
			}
			
			_currentSlide++;
			if (_currentSlide >= _slides.length)
				_currentSlide = 0;

			_slides[_currentSlide].visible = true;
			_slides[_currentSlide].render();
			
			return _slides[_currentSlide];
		}
		
		public function get currentSlide():Slide {
			return _slides[_currentSlide];
		}
		
		public function Load(strPath:String) : void {
			trace("> Loading configuration file...");
			
			var myTextLoader:URLLoader = new URLLoader();

			myTextLoader.addEventListener(Event.COMPLETE, function(event:Event) {
				trace("  Done.");
				trace("> Determine configuration file format...");
				
				var settings:* = FileUtils.parseConfigurationObject(event.target.data);
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
			
			trace("> Parse advance slide options...");
			
			_advanceSlide = new Vector.<IAdvanceSlide>();
			
			var apAllowTiming:Boolean = true;
			try { apAllowTiming = config.setup.advancePresentation.timing.allow.toString().toLowerCase() == "true"; } catch (e:Error) { apAllowTiming = true; }
			
			if (apAllowTiming) {
				
				var apTimingAutoStart:Boolean = true;
				try { apTimingAutoStart = config.setup.advancePresentation.timing.autostart.toString().toLowerCase() == "true"; } catch (e:Error) { apTimingAutoStart = true; }
				
				var apTimingDefaultMs:int = AdvancePresentation_TimingDefaultMs;
				try { apTimingDefaultMs = parseInt(config.setup.advancePresentation.timing.defaultTiming); } catch (e:Error) { apTimingDefaultMs = AdvancePresentation_TimingDefaultMs; }
				
				advanceSlide.push(new AdvanceSlideTimer(this, apTimingAutoStart, apTimingDefaultMs));
				trace("  > Advance by timings (autostart: " + apTimingAutoStart +"; defaultInterval: " + apTimingDefaultMs + ")");
			}
			
			var apAllowManualEvent:String = "none";
			try { apAllowManualEvent = config.setup.advancePresentation.manual.event.toString().toLowerCase(); } catch (e:Error) { apAllowManualEvent = "none"; }
			if (apAllowManualEvent == "click") {
				advanceSlide.push(new AdvanceSlideClick(this));
				trace("  > Advance by slide click");
			}
			
			// parse slides
			trace("> Parsing "+ config.slides.length +" slides...");
			this._slides = new Vector.<Slide>();
			for (var i:int=0; i < config.slides.length; i++) {
				var s:Slide = new Slide(this);
				
				s.parseConfiguration(config.slides[i]);
				s.originalIndex = s.viewIndex = i + 1;
				slides.push(s);
				
				_container.addChild(s);
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
			
			goToNextSlide();
		}
		
		public function notifySlideVisible(slide:Slide) {
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
