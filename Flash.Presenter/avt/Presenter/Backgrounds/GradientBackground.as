package avt.Presenter.Backgrounds {
	
	import flash.display.Sprite;
	import flash.geom.Matrix;
	import flash.display.GradientType;
	import flash.display.SpreadMethod;
	import flash.geom.Point;
	import flash.display.InterpolationMethod;
	import avt.Util.ConfigUtils;
		
	public class GradientBackground implements IBackground {
		
		public function GradientBackground() {

		}
		
		private var _style:String;
		private var _scale:Point;
		private var _rotation:Number;
		private var _spreadMethod:String;
		private var _interpolationMethod:String;
		private var _focalPointPosX:Number;
		private var _translation:Point;
		
		private var _colors: Array;
		private var _alphas: Array;
		private var _ratios: Array;
		
		public function init(config:*):void {
			
			trace("  > Slide background is gradient")
			
			_style = (config.style == "radial" ? GradientType.RADIAL : GradientType.LINEAR);
			trace("    style: " + _style)
			
			_rotation = ConfigUtils.parseNumber(config.rotation, 0) * Math.PI / 180;
			trace("    rotation: " + _rotation);
			
			_scale = new Point();
			if (config.scale && config.scale != undefined) {
				_scale.x = ConfigUtils.parseNumber(config.scale.x, 0, 0, 1);
				_scale.y = ConfigUtils.parseNumber(config.scale.y, 0, 0, 1);
			}
			trace("    size: " + _scale);
			
			_translation = new Point();
			if (config.translation && config.translation != undefined) {
				_translation.x = ConfigUtils.parseNumber(config.translation.x, 0);
				_translation.y = ConfigUtils.parseNumber(config.translation.y, 0);
			}
			trace("    translation: " + _translation)
			
			_spreadMethod = (config.spread == "pad" ? SpreadMethod.PAD : ( config.spread == "repeat" ? SpreadMethod.REPEAT : SpreadMethod.REFLECT  ) );			
			trace("    spread: " + _spreadMethod)
			
			_interpolationMethod = (config.interpolation == "linear" ? InterpolationMethod.LINEAR_RGB : InterpolationMethod.RGB);
			trace("    interpolation: " + _interpolationMethod)
			
			// parse focal point
			_focalPointPosX = ConfigUtils.parseNumber(config.focalPointPosX, 0, -1, 1);
			trace("    focal point: " + _focalPointPosX);
			
			// parse points
			var ptArr = config.keyPoints.length != undefined ? config.keyPoints : config.keyPoints.children();
			var ptArrLen = config.keyPoints.length != undefined ? config.keyPoints.length : config.keyPoints.children().length();
			
			_colors = new Array();
			_alphas = new Array();
			_ratios = new Array();
			
			for (var ipt:Number=0; ipt <ptArrLen; ipt++) {
				
				var at:Number = ConfigUtils.parseNumber(ptArr[ipt].at, ipt / (ptArrLen == 1 ? 1 : ptArrLen - 1), 0, 1);
				_ratios.push(2.55 * at * 100); // needs to be on 0-255 scale
				
				var color:uint = 0xffffff;
				try { color = parseInt(ptArr[ipt].color); } catch (err:Error) { color = 0xffffff; }
				_colors.push(color);
				
				var alpha:Number = ConfigUtils.parseNumber(ptArr[ipt].alpha, 1, 0, 1);
				_alphas.push(alpha);
				
				trace("    key point at " + at + " (color: "+ color +", alpha: "+ alpha +")");
			}
		
		}
		
		public function addTo(obj:Sprite, width: Number, height:Number):void {
			
			var matr:Matrix = new Matrix();
			matr.createGradientBox(
				_scale.x > 0 ? _scale.x * width: width, 
				_scale.y > 0 ? _scale.y * height: height, 
				_rotation, 
				_translation.x, _translation.y
			);
			 
			var bg:Sprite = new Sprite();
			bg.graphics.beginGradientFill(_style, _colors, _alphas, _ratios, matr, _spreadMethod, _interpolationMethod, _focalPointPosX);        
			bg.graphics.drawRect(0, 0, width, height);
			bg.graphics.endFill();
			obj.addChild(bg);
			
		}
		
	}
	
}