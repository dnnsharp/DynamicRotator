package avt.Presenter.SlideTransitions {
	import avt.Presenter.Slide;
	import avt.Presenter.Presentation;
		
	public class SlideTransitionNone implements ISlideTransition {
		
		public function SlideTransitionNone() {

		}
		
		public function init(presentation:Presentation, config:*):void {
			
		}
		
		public function transition(prevSlide:Slide, currentSlide:Slide, fnComplete:Function):void {
			
			trace("  > Slide transition NONE");
			
			if (prevSlide != null) {
				prevSlide.visible = false;
			}
			
			currentSlide.reset();
			fnComplete();
		}
		
		//public function cancel():void {
//			trace("  > Cancel transition NONE");
//		}
		
	}
	
}