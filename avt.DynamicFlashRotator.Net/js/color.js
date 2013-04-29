/** Color v1.0.2 - (C) 2012, William Van Rensselaer - http://williamvanr.com/projects/
 * Dual Licensed: MIT or GPL Version 2 license */
(function (l) {
    var u = l.document, s = l.Math, v = Object.prototype.toString, f = l.parseInt, w = l.parseFloat, o = s.random, t = s.floor, p = Array.isArray || function (a) { return "[object Array]" === v.call(a) }, x = function () { var a = u.createElement("div"); try { a.style.backgroundColor = "rgba(0,0,0,0.5)" } catch (b) { } return /rgba/.test(a.style.backgroundColor) }(), q, r = { slow: 600, def: 450, fast: 300 }, y = l.getComputedStyle ? function (a, b) {
        "borderColor" === b && (b = "borderTopColor"); return u.defaultView.getComputedStyle(a, null).getPropertyValue(b.replace(/([A-Z]|^ms)/g,
        "-$1").toLowerCase())
    } : function (a, b) { return a.currentStyle[b] }, c = l.Color = function (a) { if (!(this instanceof c)) return new c(a); this.set(a) }; c.prototype = c.pt = {
        set: function (a) { a = c.parse(a); this.r = a[0]; this.g = a[1]; this.b = a[2]; this.a = "number" === typeof a[3] ? a[3] : 1; return this }, toHex: function () { var a = c.rgbRange, b = a(this.r).toString(16), f = a(this.g).toString(16), a = a(this.b).toString(16); return "#" + (1 === b.length ? "0" + b : b) + (1 === f.length ? "0" + f : f) + (1 === a.length ? "0" + a : a) }, toRGB: function () {
            var a = c.rgbRange; return "rgb(" +
            a(this.r) + "," + a(this.g) + "," + a(this.b) + ")"
        }, toString: function () { var a = c.rgbRange, a = a(this.r) + "," + a(this.g) + "," + a(this.b); return x ? "rgba(" + a + "," + c.alphaRange(this.a) + ")" : "rgb(" + a + ")" }, equals: function (a) { return a instanceof c && a.r === this.r && a.g === this.g && a.b === this.b && a.a === this.a }
    }; c.sps = 40; c.animElem = function (a, b, f, i) {
        if (a.style && "object" === typeof b) {
            var j = a.color_data; if (j) { if (j.anim) return j.queue.push(function () { c.animElem(a, b, f, i) }) } else j = a.color_data = { queue: [] }; j.anim = !0; var d = [], k = [], h, n = 0;
            for (h in b) h in a.style && (d.push(y(a, h)), k.push(b[h]), b[n++] = h); this.anim(d, k, function () { if (p(this)) for (h = 0; h < this.length; h++) a.style[b[h]] = this[h].toString(); else a.style[b[0]] = this.toString() }, f, function () { j.anim = !1; if ("function" === typeof i) return i(); if (j.queue.length) return j.queue.shift()() })
        }
    }; c.anim = function (a, b, f, i, j) {
        if ("function" === typeof f) {
            !j && "function" === typeof i && (j = i); var i = i in r ? r[i] : "number" === typeof i && 0 < i ? i : r.def, d = [], k = [], h = [], n = i / (1E3 / c.sps), l = i / n, m, g, e; if (p(a) && p(b) && a.length ===
            b.length) for (g = 0; g < a.length; g++) d.push(new c(a[g])), k.push(c.parse(b[g])); else d[0] = new c(a), k[0] = c.parse(b); for (g = 0; g < d.length; g++) h.push([(k[g][0] - d[g].r) / n, (k[g][1] - d[g].g) / n, (k[g][2] - d[g].b) / n, (k[g][3] - d[g].a) / n]); m = 1 === d.length ? d[0] : d; g = 0; console.log("from:", a, "to:", b, "fromArr:", d, "toArr:", k); (function z() {
                if (++g < n) { for (e = 0; e < d.length; e++) d[e].r += h[e][0], d[e].g += h[e][1], d[e].b += h[e][2], d[e].a += h[e][3]; return !1 === f.call(m, g, m, l * g) ? "function" === typeof j ? j.call(m, !1, m) : 0 : setTimeout(z, l) } for (e = 0; e <
                d.length; e++) d[e].r = k[e][0], d[e].g = k[e][1], d[e].b = k[e][2], d[e].a = k[e][3]; f.call(m, g, m, i); return "function" === typeof j ? j.call(m, !0, m) : 0
            })()
        }
    }; c.rand = function () { return new c([t(256 * o()), t(256 * o()), t(256 * o()), 1 - o()]) }; c.rgbRange = function (a) { return "number" === typeof a ? 0 > a ? 0 : 255 < a ? 255 : s.round(a) : 255 }; c.alphaRange = function (a) { return "number" === typeof a ? 0 > a ? 0 : 1 < a ? 1 : a : 1 }; c.set = function (a, b) { return q[a] = c.parse(b) }; c.setSpeed = function (a, b) { return "number" === typeof b ? r[a] = b : !1 }; c.parse = function (a) {
        var b; return a instanceof
        c ? [a.r, a.g, a.b, a.a] : a in q ? q[a] : p(a) && 2 < a.length ? a : (b = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(a)) ? [f(b[1], 16), f(b[2], 16), f(b[3], 16), 1] : (b = /#([a-fA-F0-9])([a-fA-F0-9])([a-fA-F0-9])/.exec(a)) ? [f(b[1] + b[1], 16), f(b[2] + b[2], 16), f(b[3] + b[3], 16), 1] : (b = /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/.exec(a)) ? [f(b[1]), f(b[2]), f(b[3]), 1] : (b = /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*(0?(\.[0-9]*)?|1(\.0?)?)\s*\)/.exec(a)) ? [f(b[1]), f(b[2]), f(b[3]), w(b[4])] :
        [0, 0, 0, 0]
    }; q = {
        aqua: [0, 255, 255, 1], azure: [240, 255, 255, 1], beige: [245, 245, 220, 1], black: [0, 0, 0, 1], blue: [0, 0, 255, 1], brown: [165, 42, 42, 1], cyan: [0, 255, 255, 1], darkblue: [0, 0, 139, 1], darkcyan: [0, 139, 139, 1], darkgrey: [169, 169, 169, 1], darkgreen: [0, 100, 0, 1], darkkhaki: [189, 183, 107, 1], darkmagenta: [139, 0, 139, 1], darkolivegreen: [85, 107, 47, 1], darkorange: [255, 140, 0, 1], darkorchid: [153, 50, 204, 1], darkred: [139, 0, 0, 1], darksalmon: [233, 150, 122, 1], darkviolet: [148, 0, 211, 1], fuchsia: [255, 0, 255, 1], gold: [255, 215, 0, 1], green: [0, 128, 0, 1],
        indigo: [75, 0, 130, 1], khaki: [240, 230, 140, 1], lightblue: [173, 216, 230, 1], lightcyan: [224, 255, 255, 1], lightgreen: [144, 238, 144, 1], lightgrey: [211, 211, 211, 1], lightpink: [255, 182, 193, 1], lightyellow: [255, 255, 224, 1], lime: [0, 255, 0, 1], magenta: [255, 0, 255, 1], maroon: [128, 0, 0, 1], navy: [0, 0, 128, 1], olive: [128, 128, 0, 1], orange: [255, 165, 0, 1], pink: [255, 192, 203, 1], purple: [128, 0, 128, 1], red: [255, 0, 0, 1], silver: [192, 192, 192, 1], violet: [128, 0, 128, 1], white: [255, 255, 255, 1], yellow: [255, 255, 0, 1]
    }
})(window);

/** Gradient v1.1.0 - (C) 2012, William Van Rensselaer - http://williamvanr.com/projects/ - Requires Color.js project
 * Dual Licensed: MIT or GPL Version 2 license */
(function (p) {
    var t = p.document, u = p.Math, v = u.random, x = u.floor, l = p.parseInt, y = p.parseFloat, A = Object.prototype.toString, r = Array.isArray || function (a) { return "[object Array]" === A.call(a) }, B = function () { var a = t.createElement("div"); try { a.style.backgroundColor = "rgba(0,0,0,0.5)" } catch (b) { } return /rgba/.test(a.style.backgroundColor) }(), C = p.getComputedStyle ? function (a, b) { "borderColor" === b && (b = "borderTopColor"); return t.defaultView.getComputedStyle(a, null).getPropertyValue(b.replace(/([A-Z]|^ms)/g, "-$1").toLowerCase()) } :
    function (a, b) { return a.currentStyle[b] }, s = { slow: 600, def: 450, fast: 300 }, w, z = {}, n = 0, i = p.Color = function (a) { if (!(this instanceof i)) return new i(a); this.set(a) }, h = p.Gradient = function (a) { if (!(this instanceof h)) return new h(a); this.set(a) }; i.prototype = i.pt = {
        set: function (a) { a = i.parse(a); this.r = a[0]; this.g = a[1]; this.b = a[2]; this.a = "number" === typeof a[3] ? a[3] : 1; return this }, toHex: function () {
            var a = i.rgbRange, b = a(this.r).toString(16), d = a(this.g).toString(16), a = a(this.b).toString(16); return "#" + (1 === b.length ? "0" +
                b : b) + (1 === d.length ? "0" + d : d) + (1 === a.length ? "0" + a : a)
        }, toRGB: function () { var a = i.rgbRange; return "rgb(" + a(this.r) + "," + a(this.g) + "," + a(this.b) + ")" }, toString: function () { var a = i.rgbRange, a = a(this.r) + "," + a(this.g) + "," + a(this.b); return B ? "rgba(" + a + "," + i.alphaRange(this.a) + ")" : "rgb(" + a + ")" }, equals: function (a) { return a instanceof i && a.r === this.r && a.g === this.g && a.b === this.b && a.a === this.a }
    }; i.sps = 40; i.rand = function () { return i([x(256 * v()), x(256 * v()), x(256 * v()), 1 - v()]) }; i.rgbRange = function (a) {
        return "number" === typeof a ?
        0 > a ? 0 : 255 < a ? 255 : u.round(a) : 255
    }; i.alphaRange = function (a) { return "number" === typeof a ? 0 > a ? 0 : 1 < a ? 1 : a : 1 }; i.set = function (a, b) { return w[a] = i.parse(b) }; i.setSpeed = h.setSpeed = function (a, b) { return "number" === typeof b ? s[a] = b : !1 }; i.parse = function (a) {
        var b; return a instanceof i ? [a.r, a.g, a.b, a.a] : a in w ? w[a] : r(a) && 2 < a.length ? a : (b = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(a)) ? [l(b[1], 16), l(b[2], 16), l(b[3], 16), 1] : (b = /#([a-fA-F0-9])([a-fA-F0-9])([a-fA-F0-9])/.exec(a)) ? [l(b[1] + b[1], 16), l(b[2] + b[2], 16),
        l(b[3] + b[3], 16), 1] : (b = /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/.exec(a)) ? [l(b[1]), l(b[2]), l(b[3]), 1] : (b = /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*(0?(\.[0-9]*)?|1(\.0?)?)\s*\)/.exec(a)) ? [l(b[1]), l(b[2]), l(b[3]), y(b[4])] : [0, 0, 0, 0]
    }; i.anim = function (a, b, d, g, m) {
        if ("function" === typeof d) {
            !m && "function" === typeof g && (m = g); var g = g in s ? s[g] : "number" === typeof g && 0 < g ? g : s.def, c = [], k = [], h = [], l = 1E3 / i.sps, q = g / l, j, e, f; if (r(a) && r(b) && a.length === b.length) for (e = 0; e <
            a.length; e++) c.push(i(a[e])), k.push(i.parse(b[e])); else c[0] = i(a), k[0] = i.parse(b); for (e = 0; e < c.length; e++) h.push([(k[e][0] - c[e].r) / q, (k[e][1] - c[e].g) / q, (k[e][2] - c[e].b) / q, (k[e][3] - c[e].a) / q]); j = 1 === c.length ? c[0] : c; e = 0; (function D() {
                if (++e < q) { for (f = 0; f < c.length; f++) c[f].r += h[f][0], c[f].g += h[f][1], c[f].b += h[f][2], c[f].a += h[f][3]; return !1 === d.call(j, e, j, l * e) ? "function" === typeof m ? m.call(j, !1, j) : 0 : setTimeout(D, l) } for (f = 0; f < c.length; f++) c[f].r = k[f][0], c[f].g = k[f][1], c[f].b = k[f][2], c[f].a = k[f][3]; d.call(j,
                e, j, g); return "function" === typeof m ? m.call(j, !0, j) : 0
            })()
        }
    }; i.animElem = function (a, b, d, g) {
        if (a.style && "object" === typeof b) {
            var m = a.color_data; if (m) { if (m.anim) return m.queue.push(function () { i.animElem(a, b, d, g) }) } else m = a.color_data = { queue: [] }; m.anim = !0; var c = [], h = [], o, l = 0; for (o in b) o in a.style && (c.push(C(a, o)), h.push(b[o]), b[l++] = o); this.anim(c, h, function () { if (r(this)) for (o = 0; o < this.length; o++) a.style[b[o]] = this[o].toString(); else a.style[b[0]] = this.toString() }, d, function () {
                m.anim = !1; if ("function" ===
                typeof g) return g(); if (m.queue.length) return m.queue.shift()()
            })
        }
    }; w = {
        aqua: [0, 255, 255, 1], azure: [240, 255, 255, 1], beige: [245, 245, 220, 1], black: [0, 0, 0, 1], blue: [0, 0, 255, 1], brown: [165, 42, 42, 1], cyan: [0, 255, 255, 1], darkblue: [0, 0, 139, 1], darkcyan: [0, 139, 139, 1], darkgrey: [169, 169, 169, 1], darkgreen: [0, 100, 0, 1], darkkhaki: [189, 183, 107, 1], darkmagenta: [139, 0, 139, 1], darkolivegreen: [85, 107, 47, 1], darkorange: [255, 140, 0, 1], darkorchid: [153, 50, 204, 1], darkred: [139, 0, 0, 1], darksalmon: [233, 150, 122, 1], darkviolet: [148, 0, 211, 1], fuchsia: [255,
        0, 255, 1], gold: [255, 215, 0, 1], green: [0, 128, 0, 1], indigo: [75, 0, 130, 1], khaki: [240, 230, 140, 1], lightblue: [173, 216, 230, 1], lightcyan: [224, 255, 255, 1], lightgreen: [144, 238, 144, 1], lightgrey: [211, 211, 211, 1], lightpink: [255, 182, 193, 1], lightyellow: [255, 255, 224, 1], lime: [0, 255, 0, 1], magenta: [255, 0, 255, 1], maroon: [128, 0, 0, 1], navy: [0, 0, 128, 1], olive: [128, 128, 0, 1], orange: [255, 165, 0, 1], pink: [255, 192, 203, 1], purple: [128, 0, 128, 1], red: [255, 0, 0, 1], silver: [192, 192, 192, 1], violet: [128, 0, 128, 1], white: [255, 255, 255, 1], yellow: [255, 255,
        0, 1]
    }; h.prototype = h.pt = {
        set: function (a) { a = h.parse(a); this.angle = a[0]; for (var b = 1; b < a.length; b++) this[b - 1] = a[b]; this.length = b - 1; return this }, toString: function () {
            if (n) {
                if (1 === n) { for (var a = gradientPrefix + "linear-gradient(" + this.angle + "deg", b = 0; b < this.length; b++) a += "," + this[b][0].toString() + " " + this[b][1] + "%"; return a + ")" } if (2 === n) {
                    a = h.angleToDir(this.angle); a = gradientPrefix + "gradient(linear," + a.x1 + "% " + a.y1 + "%," + a.x2 + "% " + a.y2 + "%"; for (b = 0; b < this.length; b++) a += ",color-stop(" + this[b][1] + "%," + this[b][0].toString() +
                    ")"; return a + ")"
                } return "progid:DXImageTransform.Microsoft.gradient(startColorstr='" + this[0][0].toHex() + "',endColorstr='" + this[this.length - 1][0].toHex() + "')"
            } return this[0][0].toString()
        }
    }; h.type = function () { return n }; h.set = function (a, b) { return z[a] = h.parse(b) }; h.elem = function (a, b) { a && b && a.style && (b instanceof h || (b = h(b)), 1 === n || 2 === n ? a.style.backgroundImage = b.toString() : 3 === n ? a.style.filter = b.toString() : a.style.backgroundColor = b.toString()) }; h.anim = function (a, b, d, g, m) {
        if ("function" === typeof d) {
            !m &&
            "function" === typeof g && (m = g); var g = g in s ? s[g] : "number" === typeof g && 0 < g ? g : s.def, c = [], k = [], o = [], l = 1E3 / i.sps, q = g / l, j, e, f, n; if (r(a) && r(b) && a.length === b.length) for (j = 0; j < a.length; j++) c.push(h(a[j])), k.push(h.parse(b[j])); else c[0] = h(a), k[0] = h.parse(b); for (j = 0; j < k.length; j++) { var a = [(k[j][0] - c[j].angle) / q], p; for (e = 1; e < k[j].length; e++) b = k[j][e], p = c[j][e - 1], a.push([[(b[0].r - p[0].r) / q, (b[0].g - p[0].g) / q, (b[0].b - p[0].b) / q, (b[0].a - p[0].a) / q], (b[1] - p[1]) / q]); o.push(a) } n = 1 === c.length ? c[0] : c; j = 0; (function E() {
                if (++j <
                q) { for (e = 0; e < k.length; e++) { c[e].angle += o[e][0]; for (f = 0; f < k[e].length - 1; f++) c[e][f][0].r += o[e][f + 1][0][0], c[e][f][0].g += o[e][f + 1][0][1], c[e][f][0].b += o[e][f + 1][0][2], c[e][f][0].a += o[e][f + 1][0][3], c[e][f][1] += o[e][f + 1][1] } return !1 === d.call(n, j, n, l * j) ? "function" === typeof m ? m.call(n, !1, n) : 0 : setTimeout(E, l) } for (e = 0; e < k.length; e++) {
                    c[e].angle = k[e][0]; for (f = 0; f < k[e].length - 1; f++) c[e][f][0].r = k[e][f + 1][0].r, c[e][f][0].g = k[e][f + 1][0].g, c[e][f][0].b = k[e][f + 1][0].b, c[e][f][0].a = k[e][f + 1][0].a, c[e][f][1] = k[e][f +
                    1][1]
                } d.call(n, j, n, g); return "function" === typeof m ? m.call(n, !1, n) : 0
            })()
        }
    }; h.hoverButton = function (a, b) { if (a && a.nodeType) { var d = a.gradientProps = a.gradientProps || {}; if (b && (b.main && h.elem(a, d.main = h(b.main)), b.mouseover && (d.mouseover = h(b.mouseover)), b.mousedown && (d.mousedown = h(b.mousedown)), b.time)) d.time = l(b.time) || 0 } }; h.angleToDir = function (a) { if ("number" === typeof a) { var a = 0.017453292519943295 * a, b = 50 * u.cos(a) + 50, a = 50 * u.sin(a) + 50; return { x1: 100 - b, y1: a, x2: b, y2: 100 - a } } return { x1: 50, y1: 0, x2: 50, y2: 100 } }; h.dirToAngle =
    function (a) { var b; if ("string" === typeof a) if (0 < a.indexOf("deg")) b = y(a); else switch (a) { case "left": return 0; case "bottom left": case "left bottom": return 45; case "bottom": return 90; case "bottom right": case "right bottom": return 135; case "right": return 180; case "top right": case "right top": return 225; case "top left": case "left top": return 315; default: return 270 } else "number" === typeof a && (b = a % 360); return b || 270 }; h.parse = function (a) {
        var b = [], d = 1; if (a instanceof h) {
            b.push(a.angle); for (d = 0; d < a.length; d++) b.push([i(a[d][0]),
            a[d][1]]); return b
        } a in z ? (a = z[a], b.push(a[0])) : "string" === typeof a ? (a = a.split(/\s*,\s*(?![^\(\)]*\))/), b.push(h.dirToAngle(a[0]))) : (b.push(270), a = [0, "", ""]); for (; d < a.length; d++) if (r(a[d])) b.push([i(a[d][0]), a[d][1]]); else if (/.*%/.test(a[d])) { var g = a[d].split(/\s\s*/), m = y(g[g.length - 1]); g.splice(g.length - 1, 1); b.push([i(g.join("")), m]) } else b.push([i(a[d]), u.round(1E4 * (d - 1) / (a.length - 2)) / 100]); return b
    }; (function () {
        for (var a = t.createElement("div"), b = ["", "-webkit-", "-moz-", "-o-", "-ms-"], d = ["linear-gradient(top,#FFF,#000)",
        "gradient(linear,50% 0%,50% 100%,from(#FFF),to(#000))"], g = 0; g < b.length; g++) try { a.style.backgroundImage = b[g] + d[0]; if (0 <= a.style.backgroundImage.indexOf("gradient")) return gradientPrefix = b[g], n = 1; a.style.backgroundImage = b[g] + d[1]; if (0 <= a.style.backgroundImage.indexOf("gradient")) return gradientPrefix = b[g], n = 2 } catch (m) { } if ("filter" in a.style) {
            try { a.style.filter = "progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffffff',endColorstr='#000000')" } catch (c) { } if (0 <= a.style.filter.indexOf("gradient")) return n =
            3
        }
    })(); (function () {
        var a = function (a) { a = a || p.event; return a.target || a.srcElement }, b = function (a, b, g, d, i, l) { d.animating = i; d.time ? h.anim(b || d.curGrad, g, function (b, c) { if (d.animating !== i) return !1; h.elem(a, d.curGrad = c) }, d.time, l) : h.elem(a, g) }, d = {
            mouseover: function (d) { var d = a(d), c = d.gradientProps; c && c.mouseover && b(d, c.main, c.mouseover, c, 1) }, mouseout: function (d) { var d = a(d), c = d.gradientProps; c && c.main && b(d, null, c.main, c, 2, function () { c.animating = 0 }) }, mousedown: function (d) {
                var d = a(d), c = d.gradientProps; c && c.mousedown &&
                b(d, null, c.mousedown, c, 3)
            }, mouseup: function (d) { var d = a(d), c = d.gradientProps; c && c.mouseover && b(d, null, c.mouseover, c, 1) }
        }, g; for (g in d) t.addEventListener ? t.addEventListener(g, d[g], !1) : t.attachEvent("on" + g, d[g])
    })()
})(window);
