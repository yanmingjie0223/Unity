@echo off
setlocal enabledelayedexpansion
set curPath=%~dp0
set serverPath="%curPath%Release\BaseProjectMini\"
set webglPath="%curPath%Release\webgl\"
set resPath="%curPath%Bundles\WebGL\DynamicAssets\v1.0\"
set wxPath="%curPath%Wx"

rd /q /s !serverPath!
xcopy /s /i /y %webglPath% %serverPath%
xcopy /s /i /y %resPath% %serverPath%StreamingAssets\yoo\DynamicAssets
xcopy /s /i /y %resPath% %serverPath%CDN\WebGL\v1.0
xcopy /s /i /y %wxPath% %serverPath%Wx
rd /q /s "%curPath%Release\webgl"

:toEnd
echo end
pause
