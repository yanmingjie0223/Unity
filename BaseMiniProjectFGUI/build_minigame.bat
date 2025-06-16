@echo off
setlocal enabledelayedexpansion
set curPath=%~dp0
set serverPath="%curPath%Release\ChipMerge\"
set webglPath="%curPath%Release\webgl\"
set resPath="%curPath%Bundles\WebGL\DynamicAssets\"
set wxPath="%curPath%Wx"

rd /q /s !serverPath!
xcopy /s /i /y %webglPath% %serverPath%
xcopy /s /i /y %resPath% %serverPath%StreamingAssets\yoo\DynamicAssets
xcopy /s /i /y %wxPath% %serverPath%Wx
rd /q /s "%curPath%Release\webgl"

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