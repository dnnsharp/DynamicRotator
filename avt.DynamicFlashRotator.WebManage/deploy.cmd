
copy "%~dp0/ManageRotator.aspx" "%~dp0../Test.WebApplication/Controls/Rotator/ManageRotator.aspx"
copy "%~dp0/AdminApi.aspx" "%~dp0../Test.WebApplication/Controls/Rotator/AdminApi.aspx"
xcopy "%~dp0/res" "%~dp0..\Test.WebApplication\Controls\Rotator\res" /e /s /y

copy "%~dp0/ManageRotator.aspx" "%~dp0../avt.DynamicFlashRotator.Dnn/ManageRotator.aspx"
copy "%~dp0/AdminApi.aspx" "%~dp0../avt.DynamicFlashRotator.Dnn/AdminApi.aspx"
xcopy "%~dp0/res" "%~dp0..\avt.DynamicFlashRotator.Dnn\res" /e /s /y

