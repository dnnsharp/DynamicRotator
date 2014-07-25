call ..\..\..\..\..\..\..\paths.cmd

set projectDir=%~dp0..\avt.DynamicFlashRotator.Dnn\
set buildDir=%~dp0package-files\
set buildDirModLocation=%~dp0package-files\DesktopModules\DnnSharp\DynamicRotator\

set packageName=avt.DynamicRotator.Dnn_%1_Install

rmdir %buildDir% /s /q
mkdir %buildDir%

"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//packages/package" -attr="version" --file="DnnSharp.DynamicRotator.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//assemblies/assembly[1]/version" --file="DnnSharp.DynamicRotator.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//assemblies/assembly[2]/version" --file="DnnSharp.DynamicRotator.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//scripts/script[@type='UnInstall']/version" --file="DnnSharp.DynamicRotator.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//component[@type='Cleanup']" -attr="version" --file="DnnSharp.DynamicRotator.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1.txt" --xpath="//component[@type='Cleanup']" -attr="fileName" --file="DnnSharp.DynamicRotator.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//scripts/script[name='DataProviders\SqlDataProvider\v2.SqlDataProvider']/version" --file="DnnSharp.DynamicRotator.dnn"
echo %1 > version.txt



rem xcopy "%projectDir%act\*.*" "%buildDirModLocation%act\*.*" /e /s /y /q
rem xcopy "%projectDir%Config\*.*" "%buildDirModLocation%Config\*.*" /e /s /y /q
rem xcopy "%projectDir%js\*.*" "%buildDirModLocation%js\*.*" /e /s /y /q
xcopy "%projectDir%res\*.*" "%buildDirModLocation%res\*.*" /e /s /y /q
xcopy "%projectDir%static\*.*" "%buildDirModLocation%static\*.*" /e /s /y /q
xcopy "%projectDir%bin\release\*.dll" "%buildDir%*.*" /e /s /y /q
rem xcopy "%projectDir%*.css" "%buildDirModLocation%*.*" /y /q
xcopy "%projectDir%*.ascx" "%buildDirModLocation%*.*" /y /q
xcopy "%projectDir%*.aspx" "%buildDirModLocation%*.*" /y /q
xcopy "%projectDir%*.ashx" "%buildDirModLocation%*.*" /y /q
xcopy "%projectDir%*.css" "%buildDirModLocation%*.*" /y /q
xcopy "%projectDir%RegCore\res\*.*" "%buildDirModLocation%RegCore\res\*.*" /e /s /y /q
xcopy "%projectDir%RegCore\*.aspx" "%buildDirModLocation%RegCore\*.aspx" /e /s /y /q
xcopy "%projectDir%RegCore\*.ascx" "%buildDirModLocation%RegCore\*.ascx" /e /s /y /q
xcopy "%projectDir%DataProviders\*.*" "%buildDir%DataProviders\*.*" /e /s /y /q
xcopy "%projectDir%*.txt" "%buildDir%*.*" /y /q
xcopy "%projectDir%*.html" "%buildDir%*.*" /y /q
xcopy "%projectDir%*.dnn" "%buildDir%*.*" /y /q

cd "%buildDirModLocation%"
for %%i in (*.as?x) do "%ToolsPath%UnixTools\sed.exe" -e "s/CodeFile\s*=\"/CodeBehind=\"/ig" "%%i" > "%%i-1" && move "%%i-1" "%%i"

cd "%buildDirModLocation%RegCore\"
for %%i in (*.as?x) do "%ToolsPath%UnixTools\sed.exe" -e "s/CodeFile\s*=\"/CodeBehind=\"/ig" "%%i" > "%%i-1" && move "%%i-1" "%%i"

cd "%buildDir%"

rem add files that need removal
rem --------------------------------------------------------------------------
del ??.??.??.txt
echo bin\avt.DynamicFlashRotator.Dnn.dll > %1%.txt
echo bin\avt.DynamicFlashRotator.WebManage.dll >> %1%.txt
echo bin\avt.DynamicFlashRotator.Net.dll >> %1%.txt
echo DesktopModules\AvatarSoft\DynamicRotator >> %1%.txt


"%ToolsPath%infozip\zip.exe" -r -9 Resources.zip DesktopModules >>..\log.txt
"%ToolsPath%infozip\zip.exe" -r -9 "..\%packageName%.zip" DataProviders *.dll *.dnn *.txt *.html Resources.zip >>..\log.txt
cd..

"%ToolsPath%s3.exe" put dl.dnnsharp.com/dynamic-rotator/ %packageName%.zip /key:%S3Key% /secret:%S3Secret% 
move %packageName%.zip "%BuildsFolder%\Dynamic Rotator\Builds\Dev\%packageName%.zip"

echo http://dl.dnnsharp.com/dynamic-rotator/%packageName%.zip
echo http://dl.dnnsharp.com/dynamic-rotator/%packageName%.zip | clip

