package avt.Presenter.SlideTransitions {
	
	import avt.Presenter.Slide;
	import avt.Presenter.Presentation;
	
	public interface ISlideTransition {
		function init(presentation:Presentation, config:*):void;
		function transition(prevSlide:Slide, currentSlide:Slide, fnComplete:Function):void;
	}
	
}