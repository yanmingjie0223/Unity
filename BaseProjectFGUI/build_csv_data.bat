@echo off
setlocal enabledelayedexpansion
set curPath=%~dp0
set csvPath=%curPath%Design\CSV\
set csOutPath=%curPath%Assets\DynamicAssets\CSV\

cd Design/CSV
for %%i in (*.csv) do (
	copy "%csvPath%%%i" "%csOutPath%%%i.bytes"
)

:toEnd
endlocal
pause
echo end