@echo off
setlocal enabledelayedexpansion
set curPath=%~dp0
set csvPath=%curPath%Design\CSV\
set csOutPath=%curPath%Assets\Scripts\CSV\

cd Tools/CSV/win-x64
call com.chgames.nervergiveup.csv.exe cs %csvPath% %csOutPath%

:toEnd
endlocal
pause
echo end