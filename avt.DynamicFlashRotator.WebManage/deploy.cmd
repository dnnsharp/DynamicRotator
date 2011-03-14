
copy "%~dp0/ManageRotator.aspx" "%~dp0../Test.WebApplication/Controls/Rotator/ManageRotator.aspx"
copy "%~dp0/ManageRotatorBase.ascx" "%~dp0../Test.WebApplication/Controls/Rotator/ManageRotatorBase.ascx"
copy "%~dp0/AdminApi.aspx" "%~dp0../Test.WebApplication/Controls/Rotator/AdminApi.aspx"
copy "%~dp0/Activation.aspx" "%~dp0../Test.WebApplication/Controls/Rotator/Activation.aspx"
xcopy "%~dp0/res" "%~dp0..\Test.WebApplication\Controls\Rotator\res" /e /s /y /q

copy "%~dp0/ManageRotatorBase.ascx" "%~dp0../avt.DynamicFlashRotator/ManageRotatorBase.ascx"
copy "%~dp0/AdminApi.aspx" "%~dp0../avt.DynamicFlashRotator.Dnn/AdminApi.aspx"
xcopy "%~dp0/res" "%~dp0..\avt.DynamicFlashRotator.Dnn\res" /e /s /y /q

