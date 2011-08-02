package avt.Presenter.SlideTransitions {
	import avt.Presenter.Presentation;
	
	
	public class SlideTransitionFactory {
		
		public function SlideTransitionFactory() {
			
		}
		
		static public function factory(presentation:Presentation, config:*):ISlideTransition {
			
			var t:ISlideTransition = new SlideTransitionNone();
			if (config.transition && config.transition != undefined) {
				switch (config.transition.toString()) {
					case "fade":
						t = new SlideTransitionFade();
						break;
					case "push":
						t = new SlideTransitionPush();
						break;
				}
			}
			t.init(presentation, config);
			return t;
		}
		
	}
	
}