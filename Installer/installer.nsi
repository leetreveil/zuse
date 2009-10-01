; The name of the installer
Name "Zuse"

; The file to write
OutFile "ZuseSetup.exe"

; The default installation directory
InstallDir "C:\Program Files\Zune"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\Zuse" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

!include LogicLib.nsh

Function .onInit
  SetOutPath $TEMP
  File /oname=spltmp.bmp "splash.bmp"

  advsplash::show 1000 600 400 -1 $TEMP\spltmp

  Pop $0 ; $0 has '1' if the user closed the splash screen early,
         ; '0' if everything closed normally, and '-1' if some error occurred.

  Delete $TEMP\spltmp.bmp
FunctionEnd

;--------------------------------
; Pages

Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section "Zuse (required)"
  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "Zuse.exe"
  File "log4net.dll"

  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\Zuse "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Zuse" "DisplayName" "Zuse"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Zuse" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Zuse" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Zuse" "NoRepair" 1
  WriteUninstaller "ZuseUninstall.exe"
  
  CreateShortcut "$DESKTOP\Zuse.lnk" "$INSTDIR\Zuse.exe"
  CreateDirectory "$SMPROGRAMS\Zuse\"
  CreateShortcut "$SMPROGRAMS\Zuse\Zuse.lnk" "$INSTDIR\Zuse.exe"
  CreateShortcut "$SMPROGRAMS\Zuse\Uninstall.lnk" "$INSTDIR\ZuseUninstall.exe"
SectionEnd

Section "Microsoft .NET Framework v3.5"
  ReadRegDWORD $0 HKLM 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5' Install

  ${If} $0 == 1
    DetailPrint ".NET Framework 3.5 already installed."
  ${Else}
    MessageBox MB_OK "The .NET Framework version 3.5 was not found. Please install the Microsoft .NET Framework 3.5 before running Zuse!"
    ExecShell "open" "http://www.microsoft.com/downloads/details.aspx?displaylang=en&FamilyID=333325fd-ae52-4e35-b531-508d977d32a6"
  ${EndIf}
SectionEnd

;--------------------------------
; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Zuse"
  DeleteRegKey HKLM SOFTWARE\Zuse

  ; Remove files and uninstaller
  Delete $INSTDIR\Zuse.exe
  Delete $INSTDIR\log4net.dll

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\Zuse\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\Zuse"

SectionEnd
