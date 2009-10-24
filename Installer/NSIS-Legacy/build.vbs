Set args = WScript.Arguments
Set fso = CreateObject("Scripting.FileSystemObject")
Set shell = CreateObject("WScript.Shell")

currentdir = fso.GetParentFolderName(Wscript.ScriptFullName)

version = fso.GetFileVersion(currentdir & "\..\Release\Zuse.exe")

shell.CurrentDirectory = currentdir
shell.Run "build.cmd " & version

Wscript.Quit
