@echo off

copy ..\Release\Zuse.exe .\
copy ..\Release\log4net.dll .\

"C:\Program Files\NSIS\makensis.exe" Installer.nsi

del Zuse.exe
del log4net.dll

pause