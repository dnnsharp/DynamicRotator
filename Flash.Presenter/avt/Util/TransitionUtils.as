package avt.Util {
	
	import fl.transitions.*;
	import fl.transitions.easing.*;
	
	public class TransitionUtils {
			
		public static function transtionFromString(strType:String):* {
			switch(strType) {
				case "Blinds":
					return Blinds;
				case "Fade":
					return Fade;
				case "Fly":
					return Fly;
				case "Iris":
					return Iris;
				case "Photo":
					return Photo;
				case "PixelDissolve":
					return PixelDissolve;
				case "Rotate":
					return Rotate;
				case "Squeeze":
					return Squeeze;
				case "Wipe":
					return Wipe;
				case "Zoom":
					return Zoom;
			}
			
			return null;
		}
		
		public static function easingFromString(strEasing:String):Function {
			
			if (!strEasing || strEasing.indexOf('.') == -1)
				return None.easeNone;
				
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