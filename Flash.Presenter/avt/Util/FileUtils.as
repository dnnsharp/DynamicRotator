package avt.Util {
	
	import com.adobe.serialization.json.JSON;
	
	public class FileUtils {
		
		static function GetExtension(strPath:String):String {
			return strPath.substring(strPath.lastIndexOf(".")+1, strPath.length);
		}
		
		public static function parseConfigurationObject(strContents:String):* {
			trace("> Determine configuration file format...");
			if (strContents.match("</\s*[^>]+\s*>")) {
				// it's an XML
				trace("  XML detected.");
				return new XML(strContents);
			} else if (strContents.match(/{\s*"[^"]+"\s*:\s*{/)) {
				// it's json
				trace("  JSON detected.");
				return JSON.decode(strContents) as Object;
			} else {
				trace("  INVALID FORMAT!");
				return null;
			}
		}
	}
}