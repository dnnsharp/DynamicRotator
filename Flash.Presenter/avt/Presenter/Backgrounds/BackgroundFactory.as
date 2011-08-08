package avt.Presenter.Backgrounds {
	
	import flash.display.DisplayObject;
	
	public class BackgroundFactory {
		
		public function BackgroundFactory() {
			
		}
		
		static public function factory(config:*):IBackground {
			
			var t:IBackground = new TransparentBackground();
			if (config && config != undefined && config.bgType && config.bgType != undefined) {
				switch (config.bgType.toString()) {
					case "solid":
						t = new SolidBackground();
						break;
					case "gradient":
						t = new GradientBackground();
						break;
				}
				t.init(config);
			}
			
			return t;
		}
		
	}
	
}