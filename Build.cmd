@echo off
"C:\Dev-Env\Nant\bin\nant.exe" /f:Build/Zuse.build build32
"C:\Dev-Env\Nant\bin\nant.exe" /f:Build/Zuse.build build64
"C:\Dev-Env\Nant\bin\nant.exe" /f:Build/Duubi.build setup
"C:\Dev-Env\Nant\bin\nant.exe" /f:Build/Duubi.build duubi32
"C:\Dev-Env\Nant\bin\nant.exe" /f:Build/Duubi.build duubi64
pause
