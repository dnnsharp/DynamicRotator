package avt.Presenter.SlideObjects {
	import flash.display.MovieClip;
	import avt.Presenter.Slide;
	import flash.net.URLRequest;
	import flash.display.Loader;
	import br.com.stimuli.loading.BulkLoader;
	import flash.display.Bitmap;
	import flash.events.Event;
	
	
	public class GraphicObject extends SlideObjectBase {

		public function GraphicObject(slide:Slide) {
			super(slide);
		}
		

		public override function parseConfiguration(config:*,loader:BulkLoader): void {
			super.parseConfiguration(config,loader);
			
			if (config.src) {
				loader.add(config.src);
				loader.addEventListener(BulkLoader.COMPLETE, function(evt:Event) {
					var obj : * = loader.getContent(config.src).parent;
					addChild(obj);
				});
			
			}
		}

	}
	
}
