@if "%PROCESSOR_ARCHITECTURE%" == "x86" set ZUSEEXE=Zuse32.exe
@if "%PROCESSOR_ARCHITECTURE%" == "AMD64" set ZUSEEXE=Zuse64.exe

copy Build\Release\%ZUSEEXE% "C:\Program Files\Zune\Zuse.exe"
copy References\log4net.dll "C:\Program Files\Zune\"
copy References\Growl.CoreLibrary.dll "C:\Program Files\Zune\"
copy References\Growl.Connector.dll "C:\Program Files\Zune\"

pause
