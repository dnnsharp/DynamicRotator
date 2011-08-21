def getXmlAndJsonRegions():
	return [Region(find("1313926462819.png").below(200)),Region(find("JSON.png").below(200))]	

def closeCurrentWindow():
	click("x2.png")

def test0010():
	#Test 0010-001
	click("0010LoadSlid.png")
	wait("001SimpleSli.png")
	click("001SimpleSli-1.png")
	wait("JSONRedSlide-1.png", 5)
	
	#Test 0010-002
	click("002FromExter.png")
	wait("JSONRedExter.png", 5)
	
	#Test 0010-003
	click("003MultipleS.png")
	wait("JSONRedExter-1.png", 5)
	wait("JSONBlueInli.png", 5)
#test0010()

def test0020():
	# Test 0020-001
	click("0020SlideAdv.png")
	wait("001Tuner002C.png")
	click("001Timer.png")
	wait("JSONRedSlide.png",5)
	wait("JSONBlueSlid.png",5)
	
	# test 0020-002
	click("002Click.png")
	wait("JSONRedSlide-2.png", 5)
	wait(15)
	click("JSONRedSlide-3.png")
	click("JSONRedSlide-4.png")
	exists("JSONBlueSlid-1.png")
	
	# test 0020-003
	click("003MixedHmei.png")
	wait("JSONRedSlide-5.png",5)
	wait("JSONBlueSlid-2.png",5)
	click("RedSlideclic.png")
	exists("BlueSlidecli.png")
	click("JSONRedSlide-6.png")
	exists("JSONBlueSlid-3.png")

	#test0020-004
	click("004Disabled.png")
	click("JSONRedSlide-7.png")
	click("JSONRedSlide-8.png")
	exists("JSONRedSlide-9.png")
	wait(20)
	exists("JSONRedSlide-9.png")	
#test0020()

def test0030():
	#test0030-001
	click("0030SlideBac.png")
	wait("001Solid102L.png")
	click("001Solid.png")
	wait("JSONSolidRed.png",5)

	#test0030-102
	click("102LinearGra.png")
	wait("JSON2KeyPoin.png")
	click("2KeyPointsGr.png")
	click("JSON2KeyPoin-1.png")

	click("KePointsGree.png")
	click("JSONKePoints-1.png")
	
	click("Rotated30deg.png")
	click("JSONRotated3.png")

	click("shiftedwith5.png")
	click("JSONshiftedw.png")

	click("Iphasettogre.png")
	click("JSONIphasett.png")

	click("scalesetto40.png")
	click("JSONscaleset.png")

	exists("JSON2KeyPoin-2.png")

	#test0030-103
	click("103LinearGra.png")
	wait("JSONDefaultr.png",5)
	click("Defaultrefle.png")
	click("JSONDefaultr-1.png")

	click("Defaultrefle-1.png")
	click("JSONDefaultr-2.png")

	click("Eaeatmethodg.png")
	click("JSONEaeatmet.png")

	click("Rgueatmethod.png")
	click("JSONRgueatme.png")

	click("Repeatmethod.png")
	click("JSONPadmetho.png")
	
	click("Padmethodand.png")
	click("JSONPadmetho-1.png")

	#test0030-200
	click("200RadialGra.png")
	wait("JSON2KeyPoin-3.png")

	click("2KeyPointsGr-1.png")
	click("JSON2KeyPoin-4.png")

	click("3KeyPointsGr.png")
	click("JSON3KeyPoin.png")

	click("focalpointmo.png")
	click("JSONfocalpoi.png")

	click("ralEF.png")
	click("JSONralEF.png")

	click("grgjtranslat-1.png")
	click("JSONgrgjtran.png")

	click("QefizssmcI1Y.png")
	click("JSONQefizssm.png")
	exists("JSON2KeyPoin-5.png")
	
#test0030()

def test0040():
	click("0040SlideTra.png")
	wait("010NoneFadeP.png")
	click("010NoneFadeP-1.png")
	wait("JSONNotransi.png",5)
	click("Notransition.png")
	click("JSONNotransi-1.png")
	wait("JSONSlidefad.png",5)
	
	click("Slidefadesin.png")
	click("JSONSlidefad-1.png")
	wait("JSONNewslide.png")

	click("Newslidepush.png")
	click("JSONNewslide-1.png")
	wait("JSONSlidepus.png")

	click("Slidepushesf.png")
	click("JSONSlidepus-1.png")
	wait("JSONSlidepus-2.png")

	click("Slidepushesf-1.png")
	click("JSONSlidepus-3.png")
	wait("JSONSlidepus-4.png")

	click("Slidepushesf-2.png")
	click("JSONSlidepus-5.png")
	exists("JSONNotransi-2.png")
	
#test0040()

#setShowActions(True)
Settings.MinSimilarity = 0.99;

#regXml = Region(find("1313925685869.png")).below(200)
#regJson = Region(628,264,400,200)

def test0050():

	click("0050SlideObi.png")
	wait("010Load020Pr.png",5)
	click("010Load020.png")
	wait("SlideObiects.png",5)

	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("Slidewithone.png")
		r.click("1313925441951.png")
		r.click("1313925447430.png")
		r.click("1313925454273.png")
		r.find("Slidewithone.png")	

	
	#test 0050-020
	click("gl020Prgpert.png")
	wait("SlideObiects-2.png",5)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("Noprops.png")
		r.click("Marginstople.png")
		r.click("Rotationarou-3.png")
		r.click("Rotationarou-4.png")
		r.click("Rotationarou-5.png")
		r.click("Iphachannelo.png")
		r.click("ScaleXandY-1.png")
		r.find("Noprops.png")
		
	#test0050-110
	click("110Transitio.png")
	wait("SlideObiects-1.png",5)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("Notransition-3.png")
		wait(3)
		r.click("EnterwithFly-2.png")
		wait(3)
		r.click("bflllWIUII8C.png")
		wait(3)
		r.click("tllIWlUlHYFD.png")
		wait(3)
		r.find("Notransition-3.png")


	#test 0050-120
	click("120Transitio.png")
	wait("SlideObiects-3.png",5)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.wait("Votransition.png",15)
		assert exists("Votransition.png")
		r.highlight(1)
		r.wait("nterwithFlyl.png",15)
		assert exists("nterwithFlyl.png")
		r.highlight(1)
		r.wait("DKEFWIUII8CI.png",15)
		assert exists("DKEFWIUII8CI.png")
		r.highlight(1)
		r.wait("lllWlUlHYFDO.png",15)
		assert exists("lllWlUlHYFDO.png")
		r.highlight(1)
		r.wait("Votransition.png",15)
		assert exists("Votransition.png")
		r.highlight(1)


	#test 0050-130
	click("130AllTransi.png")
	wait("SlideObiects-4.png",5)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("NOKFHDSIEIOD.png")
		
		r.wait("Blinds-1.png",15)
		assert exists("Blinds-1.png")
		r.highlight(1)
		r.click("Blinds-1.png")

		r.wait("lB.png",15)
		assert exists("lB.png")
		r.highlight(1)
		r.click("lB.png")

		r.wait("Fly-1.png",15)
		assert exists("Fly-1.png")
		r.highlight(1)
		r.click("Fly-1.png")

		r.wait("Iris-1.png",15)
		assert exists("Iris-1.png")
		r.highlight(1)
		r.click("Iris-1.png")

		r.wait("Photo-1.png",15)
		assert exists("Photo-1.png")
		r.highlight(1)
		r.click("Photo-1.png")

		r.wait("PixeDissove-1.png",15)
		assert exists("PixeDissove-1.png")
		r.highlight(1)
		r.click("PixeDissove-1.png")

		r.wait("Rotate-1.png",15)
		assert exists("Rotate-1.png")
		r.highlight(1)
		r.click("Rotate-1.png")

		r.wait("Squeeze-1.png",15)
		assert exists("Squeeze-1.png")
		r.highlight(1)
		r.click("Squeeze-1.png")

		r.wait("Wipe-1.png",15)
		assert exists("Wipe-1.png")
		r.highlight(1)
		r.click("Wipe-1.png")

		r.wait("Zoom.png",15)
		assert exists("Zoom.png")
		r.highlight(1)
		r.click("Zoom.png")

		r.wait("NOKFHDSIEIOD.png",15)
		r.highlight(1)
		
	#test 0050-200
	click("210Animation.png")
	wait("SlideObjects.png",5)
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("Noanimation-1.png")
		r.wait("Rotateeachar-1.png",10)
		assert(r.exists("Rotateeachar-1.png"))
		r.highlight(1)
		
		r.click("Rotateeachar-1.png")
		r.wait("ScaleObjects-1.png",10)
		assert(r.exists("ScaleObjects-1.png"))
		r.highlight(1)
		
		r.click("ScaleObjects-1.png")
		r.wait("SimpleScenar.png",10)
		assert(r.exists("SimpleScenar.png"))
		r.highlight(1)


#test0050()	

def test0060():
	click("0060Links.png")
	wait("010WiholeSli.png",5)
	click("010WiholeSli-1.png")
	wait("LinksWholeSl.png",5)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.wait("licktoopente.png",12)
		r.click("licktoopente.png")
		wait("010VVholeSli.png",10)
		assert exists ("010VVholeSli.png")

		click("010WiholeSli-1.png")
		wait("LinksWholeSl.png",5)

		r.wait("Clicktoopent.png",20)
		assert r.exists("Clicktoopent.png")
		r.click("Clicktoopent.png")		
		wait("SecondTestPa.png",10)
		assert exists("SecondTestPa.png")
		closeCurrentWindow()
		
	#test 0060-020
	click("iks020SlideO.png")
	wait("LinksSlideOb-1.png",5)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("Clicktoopent-3.png")
		wait("010VVholeSli.png",10)
		assert exists ("010VVholeSli.png")
		click("iks020SlideO.png")
		wait("LinksSlideOb-1.png",5)
		
		r.click("Clicktoopent-1.png")
		r.wait("Clicktoopent-4.png",10)
		assert r.exists("Clicktoopent-4.png")
		r.click("Clicktoopent-4.png")
		wait("SecondTestPa.png",10)
		assert exists("SecondTestPa.png")
		closeCurrentWindow()
		
	#test 0060-030
	click("030MixedLink.png")
	wait("Links1VixedL.png",5)
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("Clickredsqua.png")
		wait("010VVholeSli.png",10)
		assert exists ("010VVholeSli.png")
		click("030MixedLink.png")
		wait("Links1VixedL.png",5)

		r.click("lickredsquar.png")
		wait("SecondTestPa.png",10)
		assert exists("SecondTestPa.png")
		closeCurrentWindow()

#test0060()

def test0070():
	click("0070Fonts.png")
	wait("010DefaultFo.png",5)
	click("010DefaultFo-1.png")
	wait("FontsDefault.png",10)
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		assert r.exists("Textdisplaye.png")

	#test 0070-020
	click("020StandardF.png")
	wait("FontsStandar.png",10)
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		assert r.exists("Textdisplaye-1.png")

	#test 0070-030
	click("030CustomEmb.png")
	wait("FontsCustomE.png",10)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		assert r.exists("Customembede.png")
		r.click("Customembede.png")
		assert r.exists("Customembede-1.png")

	#test 0070-040
	click("dFont040Reve.png")
	wait("FontsInvalid.png",10)
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		assert r.exists("Textwithinva.png")
#test0070()

def test0080():
	click("0080Textsand.png")
	wait("010SgleTextw.png")
	click("010SimpleTex.png")
	wait("TextSimpleTe.png",5)

	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("NoStylesTheq.png")
		r.click("SmallArialIt.png")
		r.click("IndentedFirs.png")
		r.click("UnderlineRig.png")

	#test 0080-020
	click("IStyles020HT.png")
	wait("TextHTNILFor.png",5)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("NoStylesTheq-1.png")
		r.click("Simpleformat.png")
		r.click("Fontsizecolo.png")
		r.click("Textfromexte.png")

def test00801():
	#test 0080-030
	click("Texts030Prop.png")
	wait("TextpecificP.png",5)
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("WrapNoWrapTh.png")
		dragDrop("SelectableNo-1.png", "NotSelectabl.png")
		#r.click("SelectableNo.png") # TODO: selecting text triggers slide advance
		r.click("WidthThequic-1.png")

	#test 0080-040
	click("E040Styleshe.png")
	wait("TextCSSStyle.png",5)
	
	for r in getXmlAndJsonRegions():
		r.highlight(1)
		r.click("NoStylesTheq-2.png")
		r.click("Inlineelemen.png")
		r.click("Stylesfromex.png")
		r.click("Externalcont.png")
		
test00801()
