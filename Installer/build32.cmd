@echo off

copy ..\Release\Zuse.exe .\
copy ..\Release\log4net.dll .\
copy ..\Release\Growl.Connector.dll .\
copy ..\Release\Growl.CoreLibrary.dll .\

"C:\Program Files\NSIS\makensis.exe" Installer.nsi

del Zuse.exe
del log4net.dll
del Growl.Connector.dll
del Growl.CoreLibrary.dll

pause