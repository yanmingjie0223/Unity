@echo off
setlocal enabledelayedexpansion
set CUR_PATH=%~dp0
set PROTOC=..\Tools\protoc\bin\protoc.exe
set OUT_DIR=.\Assets\Scripts\Protocol
set PROTO_DIR=..\Protocol

if not exist "%OUT_DIR%" (
	mkdir "%OUT_DIR%"
)

REM 执行编译
"%PROTOC%" ^
    --csharp_out="%OUT_DIR%" ^
    --proto_path="%PROTO_DIR%" ^
    "%PROTO_DIR%\*.proto"

if exist "%OUT_DIR%" (
    cd "%OUT_DIR%"
	for %%i in (*.cs) do (
		set name=%%i
		set tsName=!name:~0,-3!
		if not exist "%CUR_PATH%%PROTO_DIR%\!tsName!.proto" (
			del /q /s "%CUR_PATH%%OUT_DIR%\!tsName!.cs"
		)
	)

	for %%i in (*.meta) do (
		set name=%%i
		set tsName=!name:~0,-5!
		if not exist "%CUR_PATH%%OUT_DIR%\!tsName!" (
			del /q /s !name!
		)
	)
)

echo end
pause
