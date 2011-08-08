package avt.Presenter.Backgrounds {
	
	import flash.display.DisplayObject;
	import flash.display.Sprite;
		
	public class SolidBackground implements IBackground {
		
		public function SolidBackground() {

		}
		
		private var _color: uint;
		private var _alpha: Number;
		
		public function init(config:*):void {
			
			_color = 0xFFFFFF;
			if (config.color && config.color != undefined)
				try { _color = parseInt(config.color); } catch (err:Error) { _color = 0xFFFFFF; }
				
			_alpha = 1;
			if (config.alpha && config.alpha != undefined)
				try { _alpha = parseFloat(config.alpha); } catch (err:Error) { _alpha = 1; }
			
			
			trace("  > Slide background is solid color " + config.color)
		}
		
		public function addTo(obj:Sprite, width: Number, height:Number):void {
			var bg:Sprite = new Sprite();
			bg.graphics.beginFill(_color, _alpha);
			bg.graphics.drawRect(0, 0, width, height);
			bg.graphics.endFill();
			obj.addChild(bg);
		}
		
	}
	
}