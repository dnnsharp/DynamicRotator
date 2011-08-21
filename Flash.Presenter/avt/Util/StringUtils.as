
package avt.Util
{
	public class StringUtils {
		
		public static function format(strFormat:String, ...args):String {
			var replaceRegex:RegExp = /\{([0-9]+)\}/g;
			
			return strFormat.replace(replaceRegex, function():String {
				if(args == null) { 
					return arguments[0]; 
				} else {
					var resultIndex:uint = uint(StringUtils.between(arguments[0], '{', '}'));
					return (resultIndex < args.length) ? args[resultIndex] : arguments[0];
				}
			});

		}
		
		public static function formatObj(strFormat:String, obj:Object):String {
			var replaceRegex:RegExp = /\{([\w_][\w_0-9]*)\}/g;
			
			return strFormat.replace(replaceRegex, function():String {
				if(obj == null) { 
					return arguments[0]; 
				} else {
					var prop:String = StringUtils.between(arguments[0], '{', '}');
					if (obj.hasOwnProperty(prop))
						return obj[prop];
					return arguments[0];
				}
			});

		}
		
		public static function trim(s:String):String { 
			return s ? s.replace(/^\s+|\s+$/gs, '') : ""; 
		}
		
		public static function between(p_string:String, p_start:String, p_end:String):String {
			var str:String = '';
			if (p_string == null) { return str; }
			var startIdx:int = p_string.indexOf(p_start);
			if (startIdx != -1) {
				startIdx += p_start.length; // RM: should we support multiple chars? (or ++startIdx);
				var endIdx:int = p_string.indexOf(p_end, startIdx);
				if (endIdx != -1) { str = p_string.substr(startIdx, endIdx-startIdx); }
			}
			return str;
		}
		
	}
	
}