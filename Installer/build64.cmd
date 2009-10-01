@echo off

copy ..\Release\Zuse.exe .\
copy ..\Release\log4net.dll .\

"C:\Program Files (x86)\NSIS\makensis.exe" Installer.nsi

del Zuse.exe
del log4net.dll

pause