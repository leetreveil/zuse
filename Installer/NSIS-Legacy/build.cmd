@echo off

if "%1" == "" goto ZUSE_ASK_VERSION
set ZUSE_CURRENT_VERSION=%1
goto BUILD_START

:ZUSE_ASK_VERSION
echo Enter current version of Zuse: 
set /p ZUSE_CURRENT_VERSION=

:BUILD_START
copy ..\Release\Zuse.exe .\
copy ..\Release\log4net.dll .\
copy ..\Release\Growl.Connector.dll .\
copy ..\Release\Growl.CoreLibrary.dll .\

if "%PROCESSOR_ARCHITECTURE%" == "x86" goto BUILD_INSTALLER_X86
if "%PROCESSOR_ARCHITECTURE%" == "AMD64" goto BUILD_INSTALLER_AMD64
goto BUILD_FAILED

:BUILD_INSTALLER_X86
echo *** Building x86 (32-bit) installer for Zuse
"C:\Program Files\NSIS\makensis.exe" installer.nsi "/XOutFile ZuseSetup-%ZUSE_CURRENT_VERSION%-32bit.exe"
goto BUILD_CLEANUP

:BUILD_INSTALLER_AMD64
echo *** Building AMD64 (64-bit) installer for Zuse
"C:\Program Files (x86)\NSIS\makensis.exe" installer.nsi "/XOutFile ZuseSetup-%ZUSE_CURRENT_VERSION%-64bit.exe"
goto BUILD_CLEANUP

:BUILD_FAILED
echo !!! Building installer failed

:BUILD_CLEANUP
echo *** Cleaning up
del Zuse.exe
del log4net.dll
del Growl.Connector.dll
del Growl.CoreLibrary.dll

pause
