@echo off

:BUILD_START
copy ..\Build\Release\Zuse32.exe .\
copy ..\Build\Release\Zuse64.exe .\
copy ..\References\log4net.dll .\
copy ..\References\Growl.Connector.dll .\
copy ..\References\Growl.CoreLibrary.dll .\

if "%PROCESSOR_ARCHITECTURE%" == "x86" goto BUILD_INSTALLER_X86
if "%PROCESSOR_ARCHITECTURE%" == "AMD64" goto BUILD_INSTALLER_AMD64
goto BUILD_FAILED

:BUILD_INSTALLER_X86
echo *** Building x86 (32-bit) installer for Zuse
"C:\Program Files\Inno Setup 5\iscc.exe" installer.iss
goto BUILD_CLEANUP

:BUILD_INSTALLER_AMD64
echo *** Building AMD64 (64-bit) installer for Zuse
"C:\Program Files (x86)\Inno Setup 5\iscc.exe" installer.iss
goto BUILD_CLEANUP

:BUILD_FAILED
echo !!! Building installer failed

:BUILD_CLEANUP
echo *** Cleaning up
del Zuse32.exe
del Zuse64.exe
del log4net.dll
del Growl.Connector.dll
del Growl.CoreLibrary.dll

pause
