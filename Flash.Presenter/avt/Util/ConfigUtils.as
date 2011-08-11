package avt.Util {
	

	public class ConfigUtils {
		
		public static function parseStringFromProp(obj:*, prop:String, defaultValue:String=""):String {
			if (obj.hasOwnProperty(prop))
				return obj[prop].toString();
			
			return defaultValue;
		}
		
		public static function parseNumberFromProp(obj:*, prop:String, defaultValue:Number=NaN, minValue:Number=NaN, maxValue:Number=NaN):Number {
			if (obj.hasOwnProperty(prop))
				return parseNumber(obj[prop], defaultValue, minValue, maxValue);
			
			return defaultValue;
		}
		
		public static function parseNumber(numberObject:*, defaultValue:Number=NaN, minValue:Number=NaN, maxValue:Number=NaN):Number {
			var n:Number = NaN;
			if (numberObject != undefined) {
				try { n = parseFloat(numberObject); } catch (err:Error) { }
			}
			
			if (isNaN(n)) {
				if (isNaN(defaultValue))
					return NaN;
				n = defaultValue;
			}
			
			if (!isNaN(minValue) && n < minValue)
				n = minValue;
				
			if (!isNaN(maxValue) && n > maxValue)
				n = maxValue;
				
			return n;
		}
		
		public static function parseBooleanFromProp(obj:*, prop:String, defaultValue:Boolean):Boolean {
			if (obj.hasOwnProperty(prop))
				return parseBoolean(obj[prop], defaultValue);
			
			return defaultValue;
		}
		
		public static function parseBoolean(booleanObject:*, defaultValue:Boolean):Boolean {
			
			if (booleanObject == null || booleanObject == undefined) {
				return defaultValue;
			}
			
			var strBool:String = booleanObject.toString().toLowerCase();
			return strBool == "true" || strBool == "yes" || strBool == "1";
		}
	}
}