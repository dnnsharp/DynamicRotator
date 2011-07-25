package avt.Presenter {
	import flash.display.Sprite;
	
	public interface IAdvanceSlide {
		function startListen(currentSlide:Slide):void;
		function endListen(currentSlide:Slide):void;
	}
	
}