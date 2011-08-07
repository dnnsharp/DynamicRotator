package avt.Util {
	
	import com.adobe.serialization.json.JSON;
	
	public class FileUtils {
		
		static function GetExtension(strPath:String):String {
			return strPath.substring(strPath.lastIndexOf(".")+1, strPath.length).toLowerCase();
		}
		
		public static function parseConfigurationObject(strContents:String, strFilePath:String):* {
			trace("> Determine configuration file format...");
			var ext = GetExtension(strFilePath);
			if (ext == "xml") {
				trace("  XML detected.");
				return new XML(strContents);
			} else if (ext == "json") {
				trace("  JSON detected.");
				return JSON.decode(strContents) as Object;
			}
				
			// try to determine from content
			
			if (strContents.match("</\s*[^>]+\s*>")) {
				trace("  XML detected.");
				return new XML(strContents);
			} else if (strContents.match(/{\s*"[^"]+"\s*:\s*"/)) {
				trace("  JSON detected.");
				return JSON.decode(strContents) as Object;
			}
			
			trace("  INVALID FORMAT!");
			return null;
		}
	}
}