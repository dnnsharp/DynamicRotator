﻿

(function ($) {

    function avtRot(container, opts) {

        this.settings = {
            currentSlide: -1,

            delayStart: 500,

            fadeInDuration: 1000,
            fadeOutDuration: 800,
            fadeInEasing: "easeInQuad",
            fadeOutEasing: "easeOutQuad",

            textDelay: 500,

            slideStart: 0,
            paused: false

        };

        this.container = container;
        this.settings = $.extend(this.settings, opts);

        // setup container
        this.container.css({
            "width": this.settings.stageWidth,
            "height": this.settings.stageHeight,
            "overflow": "hidden",
            "font-family": "Verdana",
            "font-size": "14px"
        });

        // append background/fader element
        this.container.append(
            $("<div class='avtRotBg'></div>").css({
                "position": "absolute",
                "width": this.settings.stageWidth,
                "height": this.settings.stageHeight,
                "background-color": this.settings.fadeColor
            })
        );

        // append slides
        for (var i = 0; i < this.settings.slides.length; i++) {
            var s = this.settings.slides[i];
            var slide;

            if (s.linkUrl && s.linkClickAnywhere) {
                slide = $("<a href='" + s.linkUrl + "' class='avtSlide'></a>");
            } else {
                slide = $("<div class='avtSlide'></div>");
            }

            slide.css({
                "position": "absolute",
                "display": "block",
                "width": this.settings.stageWidth,
                "height": this.settings.stageHeight,
                "background-color": s.bkGradFrom,
                "display": "none",
                "overflow": "hidden"
            });


            slide[0]["iSlide"] = i;
            this.container.append(slide);

            // append top titles
            if (this.settings.showTopTitle == "yes") {
                var slideTitle = $("<div class='avtSlideTitle'></div>").css({
                    "position": "absolute",
                    "width": this.settings.stageWidth,
                    "height": "32px",
                    "display": "none",
                    "z-index": 999
                });

                slideTitle.append($("<div class='avtSlideTitleBk'></div>").css({
                    "position": "absolute",
                    "width": this.settings.stageWidth,
                    "height": "32px",
                    "opacity": "0." + this.settings.topTitleBgTransparency,
                    "background-color": this.settings.topTitleBackground
                }));

                slideTitle.append($("<div class='avtSlideTitleText'>" + s.title + "</div>").css({
                    "position": "absolute",
                    "width": this.settings.stageWidth,
                    "color": this.settings.topTitleTextColor,
                    "padding": "8px",
                    "font-weight": "bold",
                    "font-family": "Verdana",
                    "font-size": "14px"
                }));

                this.container.append(slideTitle);
            }

            // append objects
            for (var j = 0; j < s.slideObjects.length; j++) {
                var o = s.slideObjects[j];
                var sobj = $("<div class='avtSlideObj'></div>").css({
                    "position": "absolute",
                    "display": "none",
                    "width": o.width > 0 ? o.width : "auto"
                });

                if (o.itemType == "Text") {
                    sobj.append($("<div class='avtSlideObjContent'></div>").css({
                        "position": "absolute",
                        "color": o.textColor,
                        "padding": o.textBackgroundPadding,
                        "z-index": 800
                    }).html(o.htmlContents.replace("\n", "<br/>"))
                    );

                    // add background
                    var bk = $("<div class='avtSlideObjBk'></div>").css({
                        //"position": "absolute",
                        "background-color": o.textBackgroundColor,
                        "opacity": "0." + o.textBackgroundOpacity
                    });
                    bk.append($("<div style='visibility: hidden; padding: " + o.textBackgroundPadding + "px;'>" + o.htmlContents.replace("\n", "<br/>") + "</div>"));
                    sobj.append(bk);

                    sobj.css({ "z-index": 800 });

                } else if (o.itemType == "Image") {
                    sobj.css({ "z-index": 700 }).append("<img border='0' src= '" + o.resUrl + "' />");
                } else { // TODO: flash
                }

                if (o.linkUrl) {
                    sobj = $("<a href='" + o.linkUrl + "'></a>").append(sobj);
                }

                slide.append(sobj);
            }
        }


        // append button pane
        if (this.settings.showBottomButtons == "yes") {
            this.container.append(
                $("<div class='avtBtnPane'></div>").css({
                    "position": "absolute",
                    "margin-left": this.settings.smallButtonsXoffset,
                    "margin-top": this.settings.stageHeight - this.settings.smallButtonsYoffset,
                    "z-index": 900
                })
            );

            if (this.settings.showTimerBar == "yes") {
                var pc = $("<div class='avtProgress'></div>").css({
                    "border": "1px solid #424242",
                    "background-color": this.settings.smallButtonsColor
                });

                pc.append($("<div class='avtProgressIndicator'></div>").css({
                    "height": "4px",
                    "width": "0%",
                    "background-color": this.settings.smallButtonsNumberColor
                })
                );

                this.container.find(".avtBtnPane").append(pc);
            }

            for (var i = 0; i < this.settings.slides.length; i++) {
                var btn = $("<button class='avtBtnSlide' onclick='return false;'>" + (this.settings.smallButtonsType == 2 ? "&#160;" : (i + 1)) + "</button>");
                btn[0]["iSlide"] = i;
                btn.css({
                    "color": this.settings.smallButtonsNumberColor,
                    "background-color": this.settings.smallButtonsColor
                }).click(function () {
                    _self.switchSlide(this["iSlide"]);
                });

                if (this.settings.showTopTitle == "yes") {
                    btn.hoverIntent({
                        over: function () {
                            var _root = $(this).parents(".avtRot:first");
                            _root.find(".avtSlideTitle:eq(" + this["iSlide"] + ")").stop(true, true).slideDown();
                        },
                        timeout: 200,
                        out: function () {
                            var _root = $(this).parents(".avtRot:first");
                            _root.find(".avtSlideTitle:eq(" + this["iSlide"] + ")").stop(true, true).slideUp();
                        }
                    });
                }

                this.container.find(".avtBtnPane").append(btn);
            }

            var _self = this;
            if (this.settings.showPlayPauseControls == "yes") {
                var btn = $("<button onclick='return false;' class='avtBtnCtrl " + (this.settings.startSlideShow == "yes" ? "avtBtnPause" : "avtBtnPlay") + "'>" + (this.settings.startSlideShow == "yes" ? "Pause" : "Play") + "</button>");
                btn.css({
                    "margin": "2px",
                    "color": this.settings.smallButtonsNumberColor,
                    "background-color": this.settings.smallButtonsColor,
                    "border": "1px solid #424242",
                    "cursor": "pointer"
                }).click(function () {
                    if ($(this).hasClass("avtBtnPause")) {
                        $(this).removeClass("avtBtnPause").addClass("avtBtnPlay").html("Play");
                        _self.pause();
                    } else {
                        _self.settings.paused = true;
                        $(this).addClass("avtBtnPause").removeClass("avtBtnPlay").html("Pause");
                        _self.play();
                    }
                });

                this.container.find(".avtBtnPane").append(btn);
            }
        }


        this.switchSlide = function (iSlide, resetTimer) {

            var _self = this;
            iSlide = parseInt(iSlide);
            if (iSlide == this.settings.currentSlide) {
                if (resetTimer) {
                    this.settings.slideStart = new Date().getTime();
                    this.settings.slides[iSlide].timerChange = setTimeout(function () {
                        var nextSlide = iSlide + 1;
                        if (nextSlide > _self.settings.slides.length)
                            nextSlide = 0;
                        _self.switchSlide(nextSlide);
                    }, _self.settings.slides[iSlide].duration * 1000);
                }
                return;
            } else {
                _self.settings.slideStart = new Date().getTime();
            }

            if (iSlide >= this.settings.slides.length)
                iSlide = 0;

            clearTimeout(_self.settings.slides[iSlide].timerChange);

            this.container.find(".avtBtnSlide").removeClass("avtBtnSlideActive"); //.css("font-weight", "normal");
            this.container.find(".avtBtnSlide:eq(" + iSlide + ")").addClass("avtBtnSlideActive"); //.css("font-weight", "bold");

            var _fnNextSlide = function () {
                // setup timer for next slide
                var iNextSlide = this["iSlide"] + 1;
                if (iNextSlide >= _self.settings.slides.length)
                    iNextSlide = 0;

                if (!_self.settings.paused) {
                    _self.settings.slides[this["iSlide"]].timerChange = setTimeout(function () {
                        _self.switchSlide.apply(_self, [iNextSlide]);
                    }, _self.settings.slides[this["iSlide"]].duration * 1000);
                }
            };

            if (this.settings.currentSlide != -1) {
                _self._hideSlide(this.settings.currentSlide, function () {
                    _self._showSlide(iSlide, _fnNextSlide);
                });
            } else {
                _self._showSlide(iSlide, _fnNextSlide);
            }

            this.settings.currentSlide = iSlide;
        };

        this._showSlide = function (iSlide, fn) {
            var _slide = this.container.find(".avtSlide:eq(" + iSlide + ")");
            this.container.find(".avtRotBg").fadeOut(this.settings.fadeOutEasing, this.settings.fadeOutEasing);
            _slide.fadeIn(this.settings.fadeInDuration, this.settings.fadeInEasing, fn);

            // animate objects
            _slide.find(".avtSlideObj").hide();
            for (var j = 0; j < this.settings.slides[iSlide].slideObjects.length; j++) {
                var _self = this;
                var o = _self.settings.slides[iSlide].slideObjects[j];
                var sObj = _slide.find(".avtSlideObj:eq(" + j + ")");

                (function (o, sObj) {
                    o.showTimer = setTimeout(function () {
                        _self._showObject(sObj, o);
                    }, o.itemType == "Text" ? _self.settings.textDelay : o.delay);
                })(o, sObj);
            }
        };

        this._hideSlide = function (iSlide, fn) {
            var _slide = this.container.find(".avtSlide:eq(" + iSlide + ")");
            var _self = this;
            clearTimeout(_slide.timerChange);
            //if (!this.settings.paused) {
            _slide.timerChange = setTimeout(function () {
                _self.container.find(".avtRotBg").fadeIn(_self.settings.fadeInDuration, _self.settings.fadeInEasing);
                _slide.fadeOut(_self.settings.fadeOutEasing, _self.settings.fadeOutEasing, fn);
            }, this.settings.textDelay);
            //}

            // animate objects
            for (var j = 0; j < this.settings.slides[iSlide].slideObjects.length; j++) {
                var _self = this;
                var o = _self.settings.slides[iSlide].slideObjects[j];
                var sObj = _slide.find(".avtSlideObj:eq(" + j + ")");

                (function (o, sObj) {
                    o.showTimer = setTimeout(function () {
                        _self._hideObject(sObj, o);
                    }, o.itemType == "Text" ? 0 : _self.settings.textDelay + o.delay);
                })(o, sObj);
            }
        };

        this._showObject = function (sObj, opts) {

            sObj.css({
                "margin-left": opts.posx,
                "margin-top": opts.posy
            });

            if (opts.appearMode == "Slide") {
                var pos = { x: opts.posx, y: opts.posy };
                switch (opts.slideFrom) {
                    case "Left":
                        pos.x = -2 * sObj.width();
                        break;
                    case "Right":
                        pos.x = this.settings.stageWidth + sObj.width();
                        break;
                    case "Top":
                        pos.y = -2 * sObj.height();
                        break;
                    case "Bottom":
                        pos.y = this.settings.stageHeight + sObj.height();
                        break;
                }
                sObj.stop(true, true).css({
                    "margin-left": pos.x,
                    "margin-top": pos.y
                }).show().animate({
                    "margin-left": opts.posx,
                    "margin-top": opts.posy
                }, opts.duration * 1000, "easeOutExpo");

            } else {
                sObj.stop(true, true).fadeIn(opts.duration * 1000, "easeOutExpo");
            }
        }

        this._hideObject = function (sObj, opts) {

            clearTimeout(opts.showTimer);

            if (opts.appearMode == "Slide") {
                var pos = { x: opts.posx, y: opts.posy };
                switch (opts.slideFrom) {
                    case "Left":
                    case "Right":
                        pos.x = sObj.position().left + sObj.width() / 2;
                        pos.x = pos.x < this.settings.stageWidth / 2 ? -2 * sObj.width() : this.settings.stageWidth + sObj.width();
                        break;
                    case "Bottom":
                    case "Top":
                        pos.y = sObj.position().top + sObj.height() / 2;
                        pos.y = pos.y < this.settings.stageHeight / 2 ? -2 * sObj.height() : this.settings.stageHeight + sObj.height();
                        break;
                }
                sObj.stop(true, true).animate({
                    "margin-left": pos.x,
                    "margin-top": pos.y
                }, opts.duration * 1000, "easeOutQuart", function () { $(this).hide(); });

            } else {
                sObj.stop(true, true).fadeOut(opts.duration * 1000, "easeOutCubic");
            }
        }

        this.play = function (cSlide) {
            if (typeof (cSlide) == "undefined")
                cSlide = this.settings.currentSlide;
            this.settings.paused = false;
            this.switchSlide(cSlide, true);
        };

        this.pause = function () {
            for (var i = 0; i < this.settings.slides.length; i++) {
                clearTimeout(_self.settings.slides[i].timerChange);
            }
            this.settings.paused = true;
        };

        this.settings.paused = this.settings.startSlideShow != "yes";
        this.settings.slideStart = new Date().getTime();

        var _self = this;
        setTimeout(function () {
            _self.switchSlide(_self.settings.currentSlide + 1);
        }, this.settings.delayStart);

        // update progressbar
        if (this.settings.showTimerBar == "yes") {
            setInterval(function () {
                var _pc = _self.container.find(".avtProgress");
                if (_self.settings.paused) {
                    _pc.hide();
                } else {
                    if (_self.settings.currentSlide == -1 || !_self.settings.slideStart)
                        return;

                    _pc.show();
                    var d = _self.settings.slides[_self.settings.currentSlide].duration * 1000 + _self.settings.fadeInDuration + _self.settings.fadeOutDuration - 500; // 2 second for effects
                    var p = (new Date().getTime() - _self.settings.slideStart) / d;
                    if (p > 1)
                        p = 1;
                    _self.container.find(".avtProgressIndicator").css("width", (p * _pc.width()) + "px");
                }
            }, 100);
        }
    };


    $.fn.avtRot = function (opts) {
        $(this).data('avtRot.instance', new avtRot(this, opts));
    };


})(avtRot_jQuery_1_5_1);

