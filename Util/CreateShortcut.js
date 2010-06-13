var shell = new ActiveXObject("WScript.Shell");

var desktoppath = shell.SpecialFolders("Desktop");

var link = shell.CreateShortcut(desktoppath + "\\Zune (without Zuse).lnk");
link.Arguments = "/nozuse";
link.Description = "Launch the Zune software without Zuse";
link.HotKey = "";
link.IconLocation = "C:\\Program Files\\Zune\\Zune.exe,0";
link.TargetPath = "C:\\Program Files\\Zune\\Zune.exe";
link.WindowStyle = 3;
link.WorkingDirectory = "C:\\Program Files\\Zune";
link.Save();

shell.Popup("The new shortcut has been created!", 0, "Zuse Installer", 0);