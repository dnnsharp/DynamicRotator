
set formDir=d:\http\dnn\DotNetNuke_05.06.01\DesktopModules\FlashRotator\
set buildDir=%~dp0Asp.NET-1.2.0\
set packageName=avt.DynamicRotator.Control.1.2.0-

xcopy "%formDir%avt.DynamicFlashRotator.WebManage\act\*.*" "%buildDir%WebManage\act\*.*" /e /s /y /q
xcopy "%formDir%avt.DynamicFlashRotator.WebManage\res\*.*" "%buildDir%WebManage\res\*.*" /e /s /y /q
xcopy "%formDir%avt.DynamicFlashRotator.WebManage\*.ascx" "%buildDir%WebManage\*.*" /y /q
xcopy "%formDir%avt.DynamicFlashRotator.WebManage\*.aspx" "%buildDir%WebManage\*.*" /y /q
xcopy "%formDir%avt.DynamicFlashRotator.WebManage\bin\*.dll" "%buildDir%*.*" /e /s /y /q

xcopy "%formDir%Test.WebApplication\*.*" "%buildDir%Test.WebApplication\*.*" /e /s /y /q

cd "%buildDir%WebManage\"

for %%i in (*.as?x) do "c:\tools\UnixTools\sed.exe" -e "s/CodeFile\s*=\"/CodeBehind=\"/ig" "%%i" > "%%i-1" && move "%%i-1" "%%i"
cd..

"d:\tools\infozip\zip.exe" -9 "..\%packageName%.zip" -r *.* >>..\log.txt
cd..