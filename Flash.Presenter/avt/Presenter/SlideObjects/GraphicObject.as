package avt.Presenter.SlideObjects {
	import flash.display.MovieClip;
	import avt.Presenter.Slide;
	import flash.net.URLRequest;
	import flash.display.Loader;
	import br.com.stimuli.loading.BulkLoader;
	import flash.display.Bitmap;
	import flash.events.Event;
	
	
	public dynamic class GraphicObject extends SlideObjectBase {

		public function GraphicObject(slide:Slide) {
			super(slide);
		}
		

		public override function parseConfiguration(config:*,loader:BulkLoader): void {
			super.parseConfiguration(config,loader);
			
			if (config.src && config.src != undefined) {
				loader.add(config.src.toString(), { id: objectId });
				loader.addEventListener(BulkLoader.COMPLETE, function(evt:Event) {
					var obj : * = loader.getContent(objectId).parent;
					addChild(obj);
				});
			}
		}

	}
	
}
