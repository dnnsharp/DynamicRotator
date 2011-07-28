package avt.Util {
	
	import fl.transitions.easing.*;
	
	public class EasingHelper {
			
		public static function fromString(strEasing:String):Function {
			var strType = strEasing.substr(0, strEasing.indexOf('.'));
			var strMethod = strEasing.substr(strEasing.indexOf('.')+1);
			
			switch (strType) {
//				case "None":
//					return None.easeNone;
				case "Regular":
					return Regular[strMethod] as Function;
				case "Bounce":
					return Bounce[strMethod] as Function;
				case "Back":
					return Back[strMethod] as Function;
				case "Elastic":
					return Elastic[strMethod] as Function;
				case "Strong":
					return Strong[strMethod] as Function;
			}
			
			return None.easeNone;
		}
	}
}