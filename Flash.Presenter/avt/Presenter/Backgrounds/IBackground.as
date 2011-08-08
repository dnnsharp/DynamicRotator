package avt.Presenter.Backgrounds {
	
	import flash.display.Sprite;
	
	public interface IBackground {
		function init(config:*):void;
		function addTo(obj:Sprite, width: Number, height:Number):void;
	}
	
}