package avt.Presenter {
	
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.events.Event;
	import com.adobe.serialization.json.JSON;
	import flash.xml.XMLNode;
	import flash.xml.XMLDocument;
	import org.flexunit.runner.manipulation.filters.AbstractFilter;
	
	public class SlideObject {

		public function SlideObject() {
			// constructor code
		}

		public function parseConfiguration(config:*): void {
			trace("> Setting up presentation...");
				
			this.debug = config.setup.debug.toString().toLowerCase() == "true";
			trace("  debug: " + this.debug);
			
			this.width = config.setup.width ? parseInt(config.setup.width) : 600;
			trace("  width: " + this.width);
			
			this.height = config.setup.height ? parseInt(config.setup.height) : 200;
			trace("  height: " + this.height);
			
			// parse slides
			trace("> Parsing Slides...");
			this.slides = new Vector.<Slide>();
			for (var s:* in config.slides) {
				Slide s = new Slide();
				s.parseConfiguration(s);
				slides.Add(s);
			}
		}

	}
	
}
