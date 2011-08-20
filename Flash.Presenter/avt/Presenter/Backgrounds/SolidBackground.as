package avt.Presenter.Backgrounds {
	
	import flash.display.DisplayObject;
	import flash.display.Sprite;
		
	public class SolidBackground implements IBackground {
		
		private var _bg:Sprite;
		
		public function SolidBackground() {

		}
		
		private var _color: uint;
		private var _alpha: Number;
		
		public function init(config:*):void {
			
			_bg = new Sprite();
			
			_color = 0xFFFFFF;
			if (config.color && config.color != undefined)
				try { _color = parseInt(config.color); } catch (err:Error) { _color = 0xFFFFFF; }
				
			_alpha = 1;
			if (config.alpha && config.alpha != undefined)
				try { _alpha = parseFloat(config.alpha); } catch (err:Error) { _alpha = 1; }
			
			
			trace("  > Slide background is solid color " + config.color)
		}
		
		public function addTo(obj:Sprite):void {
			obj.addChild(_bg);
		}
		
		public function redraw(width: Number, height:Number):void {
			_bg.graphics.beginFill(_color, _alpha);
			_bg.graphics.drawRect(0, 0, width, height);
			_bg.graphics.endFill();
		}
		
	}
	
}