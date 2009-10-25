#define AppName "Zuse"
#define AppVersion GetFileVersion(AddBackslash(SourcePath) + "Zuse32.exe")

[Setup]
AppName={#AppName}
AppVerName={#AppName} version {#AppVersion}
DefaultDirName={pf}\Zune
DefaultGroupName=Zune
UninstallDisplayIcon={app}\Zune.exe
OutputBaseFilename=Zuse-v{#AppVersion}
LicenseFile={#file AddBackslash(SourcePath) + "..\LICENSE.txt"}
AppID={{AD91D676-ABD7-4E41-A321-2D7F93376BC0}
ArchitecturesAllowed=x86 x64
; ArchitecturesInstallIn64BitMode=x64

[Files]
Source: Zuse32.exe; DestName: Zuse.exe; DestDir: {app}
; Source: Zuse64.exe; DestName: Zuse.exe; DestDir: {app}; Flags: 64bit
Source: Growl.Connector.dll; DestDir: {app}
Source: Growl.CoreLibrary.dll; DestDir: {app}
Source: log4net.dll; DestDir: {app}
; Source: README.txt; DestDir: {app}; Flags: isreadme

[Icons]
Name: {commondesktop}\Zuse; Filename: {app}\Zuse.exe
Name: {group}\Zuse; Filename: {app}\Zuse.exe

[UninstallDelete]
Type: files; Name: {app}\Zuse.exe
Type: files; Name: {app}\Growl.Connector.dll
Type: files; Name: {app}\Growl.CoreLibrary.dll
Type: files; Name: {app}\log4net.dll

