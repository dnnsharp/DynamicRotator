package avt.Util {
	

	public class ConfigUtils {
		
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
	}
}