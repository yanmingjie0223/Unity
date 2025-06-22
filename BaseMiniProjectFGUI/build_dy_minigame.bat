@echo off
setlocal enabledelayedexpansion
set curPath=%~dp0
set serverPath="%curPath%Release\BaseMiniProjectFGUI\"
set webglPath="%curPath%Release\webgl\"
set resPath="%curPath%Bundles\WebGL\DynamicAssets\"

rd /q /s !serverPath!
xcopy /s /i /y %webglPath% %serverPath%
xcopy /s /i /y %resPath% %serverPath%StreamingAssets\yoo\DynamicAssets

cd %resPath%

@REM Scan and copy folders (excluding OutputCache) to CDN\WebGL
for /d %%d in (*) do (
    if /i not "%%d"=="OutputCache" (
        xcopy /s /i /y "%%d" "%serverPath%CDN\WebGL\%%d"
    )
)

:toEnd
echo end
pause