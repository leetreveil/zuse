/*
 * Zuse - A Zune Last.fm plugin
 * Copyright (C) 2007-2009 Zachary Howe
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using log4net;
using Growl.Connector;
using Growl.CoreLibrary;

namespace Zuse.Core
{
    static class Growler
    {
        private static Growl.Connector.Application growlZuseApp;
        private static GrowlConnector growl;
        private static bool isReady;

        static Growler()
        {
            isReady = false;
        }

        public static void Init()
        {
            growl = new GrowlConnector();

            if (growl.IsGrowlRunning())
            {
                Logger.Send(typeof(Zuse.Core.Growler), LogLevel.Info, "Growl is running");

                growlZuseApp = new Growl.Connector.Application("Zune");
                growlZuseApp.Icon = Icon.ExtractAssociatedIcon("Zune.exe").ToBitmap();

                NotificationType notifyTypeOpen = new NotificationType("Program Opening");
                NotificationType notifyTypeClose = new NotificationType("Program Closing");
                NotificationType notifyTypeError = new NotificationType("Program Error");
                NotificationType notifyTypeWarning = new NotificationType("Program Warning");
                NotificationType notifyTypeInfo = new NotificationType("Program Info");
                NotificationType notifyTypePlaying = new NotificationType("Now Playing");

                NotificationType[] notificationTypes = new NotificationType[] { notifyTypeOpen, notifyTypeClose, notifyTypeWarning, notifyTypeError, notifyTypeInfo, notifyTypePlaying };

                growl.Register(growlZuseApp, notificationTypes);

                isReady = true;
            }
            else
            {
                Logger.Send(typeof(Zuse.Core.Growler), LogLevel.Info, "Growl is not running, disabling support temporarily.");
            }
        }

        public static void Notify(string notificationName, string title, string msg)
        {
            if (isReady)
            {
                Notify(notificationName, title, msg, growlZuseApp.Icon);
            }
        }

        public static void Notify(string notificationName, string title, string msg, string imageUrl)
        {
            if (isReady)
            {
                Resource icn = imageUrl;

                Notify(notificationName, title, msg, icn);
            }
        }

        private static void Notify(string notificationName, string title, string msg, Resource icn)
        {
            if (isReady)
            {
                try
                {
                    if (icn == null)
                    {
                        icn = growlZuseApp.Icon;
                    }

                    growl.Notify(new Growl.Connector.Notification(growlZuseApp.Name, notificationName, "0", title, msg, icn, false, Priority.Emergency, ""));
                }
                catch (Exception e)
                {
                    Logger.Send(typeof(Zuse.Core.Growler), LogLevel.Error, e.Message, e);
                }
            }
        }
    }
}
