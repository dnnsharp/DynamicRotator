package avt.Presenter.SlideTransitions {
	import avt.Presenter.Presentation;
	
	
	public class SlideTransitionFactory {
		
		public function SlideTransitionFactory() {
			
		}
		
		static public function factory(presentation:Presentation, config:*):ISlideTransition {
			
			var t:ISlideTransition = null;
			switch (config.type) {
				case "fade":
					t = new SlideTransitionFade();
					break;
				case "push":
					t = new SlideTransitionPush();
					break;
				default:
					t = new SlideTransitionNone();
					break;
			}
			t.init(presentation, config);
			return t;
		}
		
	}
	
}