@echo off
setlocal enabledelayedexpansion
set curPath=%~dp0
set serverPath="%curPath%..\..\..\ResServer\BaseProjectFGUI\"
set webglPath="%curPath%Release\webgl\"
set resPath="%curPath%Bundles\WebGL\DynamicAssets\v1.0\"

del /q /s !serverPath!
xcopy /s /i /y %webglPath% %serverPath%
xcopy /s /i /y %resPath% %serverPath%StreamingAssets\yoo\DynamicAssets
xcopy /s /i /y %resPath% %serverPath%CDN\WebGL\v1.0

:toEnd
echo end
pause
