@echo off

copy ..\Release\Zuse.exe .\
copy ..\Release\Zuse.exe.config .\
copy ..\Release\ZuseHelper.exe .\
copy ..\Release\log4net.dll .\
copy ..\Release\MusicBrainz.dll .\

"C:\Program Files (x86)\NSIS\makensis.exe" Installer.nsi

del Zuse.exe
del Zuse.exe.config
del ZuseHelper.exe
del log4net.dll
del MusicBrainz.dll

pause