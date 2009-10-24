@echo off

if "%PROCESSOR_ARCHITECTURE%" == "x86" set REFS=References32
if "%PROCESSOR_ARCHITECTURE%" == "AMD64" set REFS=References64

if not exist "%REFS%" mkdir "%REFS%"

copy "C:\Program Files\Zune\UIX.dll" "%REFS%\"
copy "C:\Program Files\Zune\UIX.RenderApi.dll" "%REFS%\"
copy "C:\Program Files\Zune\UIXControls.dll" "%REFS%\"
copy "C:\Program Files\Zune\ZuneDBApi.dll" "%REFS%\"
copy "C:\Program Files\Zune\ZuneShell.dll" "%REFS%\"

pause
