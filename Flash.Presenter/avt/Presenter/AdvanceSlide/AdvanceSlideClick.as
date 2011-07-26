﻿package avt.Presenter.AdvanceSlide {
	
	import flash.display.Sprite;
	import flash.events.MouseEvent;
	import avt.Presenter.Slide;
	import avt.Presenter.Presentation;
	
	public class AdvanceSlideClick implements IAdvanceSlide {
		
		public function AdvanceSlideClick(presentation:Presentation) {
			_presentation = presentation;
		}
		
		private var _presentation:Presentation;
		
		public function startListen(currentSlide:Slide):void {
			currentSlide.stage.addEventListener(MouseEvent.CLICK, onSlideClick);
		}
		
		public function endListen(currentSlide:Slide):void {
			currentSlide.stage.removeEventListener(MouseEvent.CLICK, onSlideClick);
		}
		
		private function onSlideClick(event:MouseEvent):void {
			_presentation.goToNextSlide();
		}
	}
	
}