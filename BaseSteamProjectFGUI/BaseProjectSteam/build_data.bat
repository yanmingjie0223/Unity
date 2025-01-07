set WORKSPACE=.\
set LUBAN_DLL=%WORKSPACE%Tools\Luban\Luban.dll
set CONF_ROOT=%WORKSPACE%..\Design\Datas

dotnet %LUBAN_DLL% ^
    -t client ^
    -c cs-bin ^
    -d bin  ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=%WORKSPACE%Assets\DynamicAssets\Luban\Codes ^
    -x outputDataDir=%WORKSPACE%Assets\DynamicAssets\Luban\Datas

pause