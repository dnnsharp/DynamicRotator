package avt.Util {
	
	import flash.display.DisplayObject;

	public class PositionHelper {
		
		public static function getRealX(obj:DisplayObject, parentWidth:Number, x:*, xDefault: *, marginLeft:Number, marginRight:Number):Number {
			
			if (isNaN(marginLeft)) marginLeft = 0;
			if (isNaN(marginRight)) marginRight = 0;
			
			var xReal:Number  = x ? parseFloat(x) : parseFloat(xDefault);
			if (isNaN(xReal))
				xReal = mapAlignX(obj, parentWidth, x ? x.toString() : xDefault.toString());
			
			return xReal + marginLeft - marginRight;
		}
		
		public static function mapAlignX(obj:DisplayObject, parentWidth:Number, align:String):Number {
			var xReal:Number = NaN;
			switch (align) {
				case "left":
					xReal = 0;
					break;
				case "center":
					xReal = parentWidth/2 - obj.width/2;
					break;
				case "right":
					xReal = parentWidth - obj.width;
					break;
			}
			
			return xReal;
		}
		
		public static function getRealY(obj:DisplayObject, parentHeight:Number, y:*, yDefault: *, marginTop:Number, marginBottom:Number):Number {
			
			if (isNaN(marginTop)) marginTop = 0;
			if (isNaN(marginBottom)) marginBottom = 0;
			
			var yReal:Number  = y ? parseFloat(y) : parseFloat(yDefault);
			if (isNaN(yReal))
				yReal = mapAlignY(obj, parentHeight, y ? y.toString() : yDefault.toString());
			
			return yReal + marginTop - marginBottom;
		}
		
		public static function mapAlignY(obj:DisplayObject, parentHeight:Number, align:String):Number {
			var yReal:Number = NaN;
			switch (align) {
				case "top":
					yReal = 0;
					break;
				case "center":
					yReal = parentHeight/2 - obj.height/2;
					break;
				case "bottom":
					yReal = parentHeight - obj.height;
					break;
			}
			
			return yReal;
		}
	}
}