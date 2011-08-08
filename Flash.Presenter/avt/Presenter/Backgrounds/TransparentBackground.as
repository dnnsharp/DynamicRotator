package avt.Presenter.Backgrounds {
	import flash.display.Sprite;
		
	public class TransparentBackground implements IBackground {
		
		public function TransparentBackground() {

		}
		
		public function init(config:*):void {
			trace("  > Slide background is transparent")
		}
		
		public function addTo(obj:Sprite, width: Number, height:Number):void {
			
		}
	}
	
}