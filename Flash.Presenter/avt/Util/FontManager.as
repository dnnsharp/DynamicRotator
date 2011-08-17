package avt.Util {
	import avt.Presenter.Config.IConfigurable;
	import flash.display.Loader;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.display.LoaderInfo;
	import flash.net.URLRequest;
	import flash.text.Font;
	import flash.system.ApplicationDomain;
	

	public class FontManager implements IConfigurable {
		
		var _fnDoneLoading:Function;
		var _fontsLoaded:int;
		var _fontsToLoad:int;
		
		public function FontManager(fnDoneLoading:Function) {
			_fnDoneLoading = fnDoneLoading;
			_fontsLoaded = 0;
			_fontsToLoad = 0;
		}
		
		public function parseConfig(config:*):void {
			
			if (!config || config == undefined) {
				_fnDoneLoading();
				return;
			}
			
			trace("Loading Fonts...");
			
			var fonts:Array = new Array();
			var fontsArr = config.length != undefined ? config : config.children();
			var fontsArrLen = config.length != undefined ? config.length : config.children().length();
		
			
			for (var i:int=0; i < fontsArrLen; i++) {
				var o:Object = { "url": fontsArr[i].url, "symbols": [] };
				if (!o.url) {
					trace("  Font "+ i +" skipped because there's no URL to load!");
					continue;
				}
				
				if (!fontsArr[i].hasOwnProperty("symbols")) {
					trace("  Font "+ i +" skipped because there are no symbols to load!");
					continue;
				}
				
				// iterate all symbols
				var symbolArr = fontsArr[i].symbols.length != undefined ? fontsArr[i].symbols : fontsArr[i].symbols.children();
				var symbolArrLen = fontsArr[i].symbols.length != undefined ? fontsArr[i].symbols.length : fontsArr[i].symbols.children().length();
				for (var iSymbol:int=0; iSymbol < symbolArrLen; iSymbol++) {
					if (!symbolArr[iSymbol])
						continue;
					o.symbols.push(symbolArr[iSymbol]);
				}
				
				if (o.symbols.length == 0) {
					trace("  Font "+ i +" skipped because there are no symbols to load!");
					continue;
				}
				
				fonts.push(o);
				trace("  Loading font " + o.url + " (symbols: "+ o.symbols.join(", ") +")");
				
				_fontsToLoad++;
			}
			
			fonts.forEach(function(item:Object, index:int, list:Array) {
				
				var fontLoader:Loader = new Loader();
				
				fontLoader.contentLoaderInfo.addEventListener(Event.COMPLETE, function (event:Event):void {
					var domain:ApplicationDomain = (event.target as LoaderInfo).applicationDomain;
					
					for each (var s:String in item.symbols) {
						Font.registerFont( Class( domain.getDefinition(s)) );
					}
					
					_fontsLoaded++;
					if (_fontsLoaded == _fontsToLoad)
						_fnDoneLoading();
				});
				
				fontLoader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, function (event:IOErrorEvent):void {
					trace("ERROR: Could not find font "+event.target.loaderURL);
				});			
				fontLoader.load(new URLRequest(item.url));
			});
			
			if (_fontsToLoad == 0) {
				// no fonts to load, invoke callback instantly
				_fnDoneLoading();
			}
		}

		
		private function onFontLoaded(event:Event):void
		{
			var domain:ApplicationDomain = (event.target as LoaderInfo).applicationDomain;
			var symbols:Array = (event.target as LoaderInfo).loader["symbols"];
			
			for each (var s:String in symbols) {
				Font.registerFont( Class( domain.getDefinition(s)) );
			}
			
			_fontsLoaded++;
			if (_fontsLoaded == _fontsToLoad)
				_fnDoneLoading();
		}
//		
//		function getFileNameWithoutExtension(fullPath: String) : String
//		{
//			var fSlash: int = fullPath.lastIndexOf("/");
//			var bSlash: int = fullPath.lastIndexOf("\\"); // reason for the double slash is just to escape the slash so it doesn't escape the quote!!!
//			var slashIndex: int = fSlash > bSlash ? fSlash : bSlash;
//			return fullPath.substr(slashIndex + 1).replace(".swf", "");
//		}
//		
//		private function onFontsLoadComplete():void
//		{
//			trace(fonts.length + " fonts have been loaded");
//			for each (var f:Font in Font.enumerateFonts()) {
//				trace(f.fontName);
//			}
//			
//			loadCSS();
//		}
		
	}
}