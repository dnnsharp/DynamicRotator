package avt.Presenter.AdvanceSlide {
	import flash.display.Sprite;
	import avt.Presenter.Slide;
	
	public interface IAdvanceSlide {
		function startListen(currentSlide:Slide):void;
		function endListen(currentSlide:Slide):void;
	}
	
}