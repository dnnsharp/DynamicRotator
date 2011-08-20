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
	import flash.display.DisplayObject;
	import flash.events.TextEvent;
	import avt.Util.StringUtils;
	import flash.text.TextFieldType;
	
	// see supported tags 
	// http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/text/TextField.html#htmlText
	public dynamic class HtmlText extends SlideObjectBase {

		private var _textField:TextField;

		public function HtmlText(slide:Slide) {
			super(slide);
			
			_textField = new TextField();
			_textField.wordWrap = true;
			_textField.multiline = true;
			_textField.selectable = false;
			_textField.antiAliasType = AntiAliasType.NORMAL;
			_textField.width = slide.presentation.slideWidth / 2;
			_textField.type = TextFieldType.DYNAMIC;
		}

		public override function parseConfiguration(config:*,loader:BulkLoader): void {
			
			if (!config || config == undefined || !config.hasOwnProperty("content"))
				return;

			if (config.hasOwnProperty("props")) {
				// parse and remove props that shouldn't get to base movieclip
				if (config.props.hasOwnProperty("width")) {
					_textField.width = parseFloat(config.props.width);
					delete config.props.width;
				}
				if (config.props.hasOwnProperty("wordWrap")) {
					_textField.wordWrap = (config.props.wordWrap.toString().toLowerCase() == "true");
					delete config.props.wordWrap;
				}
				if (config.props.hasOwnProperty("selectable")) {
					_textField.selectable = (config.props.selectable.toString().toLowerCase() == "true");
					delete config.props.selectable;
				}
				//if (config.props.hasOwnProperty("antiAliasType")) {
//					_textField.antiAliasType = (config.props.antiAliasType.toString().toLowerCase() == "advanced" ? AntiAliasType.ADVANCED : AntiAliasType.NORMAL);
//					_textField.sharpness = -400;
//					delete config.props.antiAliasType;
//				}
			}
				
			// parse base configuration
			super.parseConfiguration(config, loader);

			// load styles
			loadStyles(config,loader);
			
			// set padding
			_textField.x = padding.left;
			_textField.y = padding.top;

			// load text
			if (config.content.hasOwnProperty("src")) {
				loader.add(config.content.src.toString(), { id: objectId });
				loader.addEventListener(BulkLoader.COMPLETE, function(evt:Event) {
					setHTML(loader.getText(objectId));
				});
			} else {
				setHTML(config.content);
			}
			
			// finally, add it to the scene (by now there should also be a background and everything set)
			// so the stacking if respected (text on top of background)
			addChild(_textField);
			
			//_textField.addEventListener(TextEvent.LINK, onHyperLinkEvent);
			
			//_textField.background  = true;
			//_textField.backgroundColor = 0x0000ff;
			
			//_textField.x = 20;
			//trace(_textField.width)
			//trace(width);
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

		private function loadStyles(config:*,loader:BulkLoader):void {
			 
			 _textField.styleSheet = new StyleSheet();
			
			// default styles applt to a <body> root object (that we wrap manually around the content)
			if (config.hasOwnProperty("defaultStyles"))
				_textField.styleSheet.parseCSS("body { " + config.defaultStyles + " }");
				
			var bodyStyles = _textField.styleSheet.getStyle("body");
			if (!bodyStyles)
				bodyStyles = {};
			if (!bodyStyles["fontSize"]) 
				bodyStyles["fontSize"] = "16";
			_textField.styleSheet.setStyle("body", bodyStyles);
			
			var aStylesLink = _textField.styleSheet.getStyle("a:link");
			if (!aStylesLink)
				aStylesLink = {};
				
			if (!aStylesLink["color"]) aStylesLink["color"] = "#0033cc";
			if (!aStylesLink["textDecoration"]) aStylesLink["textDecoration"] = "none";
			
			_textField.styleSheet.setStyle("a:link", aStylesLink);
			
			var aStylesHover = _textField.styleSheet.getStyle("a:hover");
			if (!aStylesHover)
				aStylesHover = {};
				
			if (!aStylesHover["color"]) aStylesHover["color"] = "#0000ee";
			if (!aStylesHover["textDecoration"]) aStylesHover["textDecoration"] = "underline";
			
			_textField.styleSheet.setStyle("a:hover", aStylesHover);
			
			//_textField.styleSheet.setStyle("a:link", {color:'#0000CC',textDecoration:'none'});
//			_textField.styleSheet.setStyle("a:hover", {color:'#0000FF',textDecoration:'underline'});

			
			// now check the presence of a stylesheet that allows writting CSS styles with selectors and everythign
			// see http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/text/StyleSheet.html
			if (config.hasOwnProperty("stylesheet")) {
				if (config.stylesheet.hasOwnProperty("src")) {
					loader.add(config.stylesheet.src.toString(), { id: objectId + "-css" });
					loader.addEventListener(BulkLoader.COMPLETE, function(evt:Event) {
						_textField.styleSheet.parseCSS(loader.getText(objectId + "-css"));
						// autosize only works with wordWrap
						if (_textField.wordWrap)
							_textField.autoSize = TextFieldAutoSize.LEFT;
					});
				} else {
					_textField.styleSheet.parseCSS(config.stylesheet);
				}
			}
			
			// autosize only works with wordWrap
			if (_textField.wordWrap)
				_textField.autoSize = TextFieldAutoSize.LEFT;
		}


		public function setHTML(htmlText:String):void {
			
			_textField.htmlText = "<body>"+ StringUtils.trim(htmlText) + "</body>";
			
			var loader:Loader = _textField.getImageReference("image") as Loader;
			if (loader) {
				// add event listener for when the htmlTextField is resized from adding the image
				_textField.addEventListener(Event.CHANGE, onImageAdded);
			} else {
				// there is no image reference, lock the height of the text field
				lockHeight();
			}
		}
		
		private function onImageAdded(event:Event):void {
			// access the image from the image reference in the text field
			var loader:Loader = _textField.getImageReference("image") as Loader;
			var image:DisplayObject = loader.content;
			// adjust the x position of the image to offset hspace
			image.x -= 5;
			// lock the height of the text field to prevent scrolling bug
			lockHeight();
		}
					
		private function lockHeight():void 	{
			
			// turn autosize off
			_textField.autoSize = TextFieldAutoSize.NONE;
			_textField.height = _textField.textHeight;
			// get height adjustment
			// first, loop through StyleSheet styles to get highest leading value
			var highestLeading:int = 0;
			for each (var style:String in _textField.styleSheet.styleNames) {
				var leading:int = int(_textField.styleSheet.getStyle(style).leading);
				if (highestLeading < leading)
					highestLeading = leading;
			}
			
			// now, get the value of the height adjustment
			var heightAdjust:int = _textField.maxScrollV + highestLeading + 2;
			_textField.height += heightAdjust;
		}
		//
//		function onHyperLinkEvent(evt:TextEvent):void {
//			trace("**click**"+ evt.text);
//			var str:String = _textField.htmlText;
//			str = str.split("'event:"+evt.text+"'").join("'event:"+evt.text+"' class='visited' ");
//			_textField.htmlText = str;
//		}
	}
	
	
	// htmlTextField.addEventListener(TextEvent.LINK, textLinkHandler);
		
		//private function textLinkHandler(event:TextEvent):void
//		{
//			if (event.text == "increaseWidth")
//				changeWidth(blockWidth + 10);
//			if (event.text == "decreaseWidth")
//				changeWidth(blockWidth - 10);
//		}
//		
//		private function changeWidth(newWidth:int):void
//		{
//			blockWidth = newWidth;
//			// unlock the height of the text field
//			htmlTextField.autoSize = TextFieldAutoSize.LEFT;
//			// change the width
//			htmlTextField.width = blockWidth;
//			// re-lock the height
//			lockHeight();
//		}
	
	
}
