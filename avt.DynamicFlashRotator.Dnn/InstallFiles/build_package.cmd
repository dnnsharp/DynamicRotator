
set formDir=d:\http\dnn\DotNetNuke_05.06.01\DesktopModules\FlashRotator\avt.DynamicFlashRotator.Dnn\
set buildDir=%~dp01.2.0\
set packageName=avt.DynamicRotator.DNN.1.2.0-

xcopy "%formDir%act\*.*" "%buildDir%act\*.*" /e /s /y /q
rem xcopy "%formDir%Config\*.*" "%buildDir%Config\*.*" /e /s /y /q
rem xcopy "%formDir%js\*.*" "%buildDir%js\*.*" /e /s /y /q
xcopy "%formDir%res\*.*" "%buildDir%res\*.*" /e /s /y /q
xcopy "%formDir%bin\release\*.dll" "%buildDir%*.*" /e /s /y /q
rem xcopy "%formDir%*.css" "%buildDir%*.*" /y /q
xcopy "%formDir%*.ascx" "%buildDir%*.*" /y /q
xcopy "%formDir%*.aspx" "%buildDir%*.*" /y /q

cd "%buildDir%"

for %%i in (*.as?x) do "c:\tools\UnixTools\sed.exe" -e "s/CodeFile\s*=\"/CodeBehind=\"/ig" "%%i" > "%%i-1" && move "%%i-1" "%%i"

"d:\tools\infozip\zip.exe" -r -9 Resources.zip act res *.aspx *.ascx License.txt >>..\log.txt
"d:\tools\infozip\zip.exe" -9 "..\%packageName%.zip" *.SqlDataProvider *.dll *.dnn *.dnn5 *.txt Resources.zip >>..\log.txt
cd..