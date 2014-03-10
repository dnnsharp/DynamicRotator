call ..\..\..\..\..\..\..\paths.cmd

set projectDir=%~dp0..\avt.DynamicFlashRotator.Dnn\
set buildDir=%~dp0package-files\
set packageName=avt.DynamicRotator.Dnn_%1_Install

"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//packages/package" -attr="version" --file="avt.DynamicFlashRotator.Net.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//assemblies/assembly[1]/version" --file="avt.DynamicFlashRotator.Net.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//assemblies/assembly[2]/version" --file="avt.DynamicFlashRotator.Net.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//assemblies/assembly[3]/version" --file="avt.DynamicFlashRotator.Net.dnn"
"%OwnToolsPath%xpath-update\bin\release\xpath-update.exe" --value="%1" --xpath="//scripts/script[@type='UnInstall']/version" --file="avt.DynamicFlashRotator.Net.dnn"
echo %1 > version.txt



rem xcopy "%projectDir%act\*.*" "%buildDir%act\*.*" /e /s /y /q
rem xcopy "%projectDir%Config\*.*" "%buildDir%Config\*.*" /e /s /y /q
rem xcopy "%projectDir%js\*.*" "%buildDir%js\*.*" /e /s /y /q
xcopy "%projectDir%res\*.*" "%buildDir%res\*.*" /e /s /y /q
xcopy "%projectDir%bin\release\*.dll" "%buildDir%*.*" /e /s /y /q
rem xcopy "%projectDir%*.css" "%buildDir%*.*" /y /q
xcopy "%projectDir%*.ascx" "%buildDir%*.*" /y /q
xcopy "%projectDir%*.aspx" "%buildDir%*.*" /y /q
xcopy "%projectDir%*.ashx" "%buildDir%*.*" /y /q
xcopy "%projectDir%*.css" "%buildDir%*.*" /y /q
xcopy "%projectDir%RegCore\res\*.*" "%buildDir%RegCore\res\*.*" /e /s /y /q
xcopy "%projectDir%RegCore\*.aspx" "%buildDir%RegCore\*.aspx" /e /s /y /q
xcopy "%projectDir%RegCore\*.ascx" "%buildDir%RegCore\*.ascx" /e /s /y /q
xcopy "%projectDir%DataProviders\*.*" "%buildDir%DataProviders\*.*" /e /s /y /q
xcopy "%projectDir%*.txt" "%buildDir%*.*" /y /q
xcopy "%projectDir%*.html" "%buildDir%*.*" /y /q
xcopy "%projectDir%*.dnn" "%buildDir%*.*" /y /q

cd "%buildDir%"
for %%i in (*.as?x) do "%ToolsPath%UnixTools\sed.exe" -e "s/CodeFile\s*=\"/CodeBehind=\"/ig" "%%i" > "%%i-1" && move "%%i-1" "%%i"

cd "%buildDir%RegCore\"
for %%i in (*.as?x) do "%ToolsPath%UnixTools\sed.exe" -e "s/CodeFile\s*=\"/CodeBehind=\"/ig" "%%i" > "%%i-1" && move "%%i-1" "%%i"

cd "%buildDir%"


"%ToolsPath%infozip\zip.exe" -r -9 Resources.zip act res RegCore *.aspx *.ascx *.ashx *.css License.txt >>..\log.txt
"%ToolsPath%infozip\zip.exe" -r -9 "..\%packageName%.zip" DataProviders *.dll *.dnn *.txt *.html Resources.zip >>..\log.txt
cd..

"%ToolsPath%s3.exe" put dl.dnnsharp.com/ADROT/ %packageName%.zip /key:%S3Key% /secret:%S3Secret% 

echo http://dl.dnnsharp.com/ADROT/%packageName%.zip
echo http://dl.dnnsharp.com/ADROT/%packageName%.zip | clip

