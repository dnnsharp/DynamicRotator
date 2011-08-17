package avt.Presenter.SlideObjects {
	import flash.display.MovieClip;
	import avt.Presenter.Slide;
	import flash.net.URLRequest;
	import flash.display.Loader;
	import br.com.stimuli.loading.BulkLoader;
	import flash.display.Bitmap;
	import flash.events.Event;
	import flash.text.TextFormat;
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;
	import flash.text.StyleSheet;
	import avt.Util.ConfigUtils;
	import flash.text.AntiAliasType;
	
	
	public dynamic class HtmlText extends SlideObjectBase {

		private var _textField:TextField;

		public function HtmlText(slide:Slide) {
			super(slide);
			
			_textField = new TextField();
			_textField.autoSize = TextFieldAutoSize.LEFT;
		}

		public override function parseConfiguration(config:*,loader:BulkLoader): void {
			
			if (!config || config == undefined || !config.hasOwnProperty("content"))
				return;
			
			if (config.hasOwnProperty("props")) {
				// parse and remove props that shouldn't get to base movieclip
				if (config.props.hasOwnProperty("width")) {
					_textField.width = parseFloat(config.props.width);
					config.props.width = undefined;
				}
				if (config.props.hasOwnProperty("wordWrap")) {
					_textField.wordWrap = (config.props.wordWrap.toString().toLowerCase() == "true");
					config.props.wordWrap = undefined;
				} else {
					_textField.wordWrap = true;
				}
				if (config.props.hasOwnProperty("selectable")) {
					_textField.selectable = (config.props.selectable.toString().toLowerCase() == "true");
					config.props.selectable = undefined;
				} else {
					_textField.selectable = false;
				}
				if (config.props.hasOwnProperty("antiAliasType")) {
					_textField.antiAliasType = (config.props.antiAliasType.toString().toLowerCase() == AntiAliasType.ADVANCED ? AntiAliasType.ADVANCED : AntiAliasType.NORMAL);
					config.props.antiAliasType = undefined;
				} else {
					_textField.antiAliasType = AntiAliasType.NORMAL;
				}
			}
				
			// parse base configuration
			super.parseConfiguration(config, loader);		
				
			// load styles
			_textField.styleSheet = new StyleSheet();
			
			// default styles applt to a <body> root object (that we wrap manually around the content)
			if (config.hasOwnProperty("defaultStyles"))
				_textField.styleSheet.parseCSS("body {" + config.defaultStyles + "}");
			
			// now check the presence of a stylesheet that allows writting CSS styles with selectors and everythign
			// see http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/text/StyleSheet.html
			if (config.hasOwnProperty("stylesheet")) {
				if (config.stylesheet.hasOwnProperty("src")) {
					loader.add(config.stylesheet.src.toString(), { id: objectId });
					loader.addEventListener(BulkLoader.COMPLETE, function(evt:Event) {
						_textField.styleSheet.parseCSS(loader.getText(objectId));
					});
				} else {
					_textField.styleSheet.parseCSS(config.stylesheet);
				}
			}
			
			// load text
			if (config.content.hasOwnProperty("src")) {
				loader.add(config.content.src.toString(), { id: objectId });
				loader.addEventListener(BulkLoader.COMPLETE, function(evt:Event) {
					_textField.htmlText = loader.getText(objectId);
				});
			} else {
				_textField.htmlText = "<body>"+ config.content + "</body>";
			}
			
			// finally, add it to the scene (by now there should also be a background and everything set)
			addChild(_textField);
			
			//
//			
//			
//			trace("  > Initialize Text Format");
//			var myFormat:TextFormat = new TextFormat();
//			if (config.hasOwnProperty("defaultTextFormat")) {
//				var defaultTextFormat:* = config.defaultTextFormat.children ? config.defaultTextFormat.children() : config.defaultTextFormat;
//				for (var styleName in defaultTextFormat) {
//					trace("    Set style " + styleName + " = " + defaultTextFormat[styleName]);
//					if (myFormat.hasOwnProperty(styleName)) {
//						myFormat[styleName] = defaultTextFormat[styleName];
//					}
//				}
//			}
			
			
			//ConfigUtils.parseStringFromProp(config.font
			//myFormat.font = "MedievalSharp";
			//myFormat.font = "Arial";
			//myFormat.size = 24;
			//myFormat.
			
			/* Create a new TextField object, assign the text format using the 
			   defaultTextFormat property, and set the embedFonts property to true. */
			//var myTextField:TextField = new TextField();
			//_textField.width = 260;
			// _textField.wordWrap = true;
			
			//_textField.defaultTextFormat = myFormat;
			
			
			// myTextField.embedFonts = true;
			//myTextField.styleSheet = new StyleSheet();
			//myTextField.styleSheet.parseCSS("p { font-family: Arial Bold; } ");
//			myTextField.htmlText = "The quick brown fox jumped over the lazy dog Test";
			//  <img src='http://highslide.com/samples/full2.jpg' />
//			addChild(myTextField);

			//if (config.src && config.src != undefined) {
//				loader.add(config.src.toString(), { id: objectId });
//				loader.addEventListener(BulkLoader.COMPLETE, function(evt:Event) {
//					var obj : * = loader.getContent(objectId).parent;
//					addChild(obj);
//				});
//			}
		}
		
		//private function allDone() {
//			addChild(_textField);
//		}

	}
	
}
