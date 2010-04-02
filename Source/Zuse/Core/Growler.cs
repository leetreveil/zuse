/*
 * Zuse - A Zune Last.fm plugin
 * Copyright (C) 2007-2010 Zachary Howe
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
using System.Drawing;
using Growl.Connector;
using Growl.CoreLibrary;

namespace Zuse.Core
{
    internal static class Growler
    {
        private static GrowlConnector growl;
        private static Application growlZuseApp;
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
                Logger.Send(typeof (Growler), LogLevel.Info, "Growl is running");

                growlZuseApp = new Application("Zune");
                growlZuseApp.Icon = Icon.ExtractAssociatedIcon("Zune.exe").ToBitmap();

                var notifyTypeOpen = new NotificationType("Program Opening");
                var notifyTypeClose = new NotificationType("Program Closing");
                var notifyTypeError = new NotificationType("Program Error");
                var notifyTypeWarning = new NotificationType("Program Warning");
                var notifyTypeInfo = new NotificationType("Program Info");
                var notifyTypePlaying = new NotificationType("Now Playing");

                var notificationTypes = new[]
                                            {
                                                notifyTypeOpen, notifyTypeClose, notifyTypeWarning, notifyTypeError,
                                                notifyTypeInfo, notifyTypePlaying
                                            };

                growl.Register(growlZuseApp, notificationTypes);

                isReady = true;
            }
            else
            {
                Logger.Send(typeof (Growler), LogLevel.Info, "Growl is not running, disabling support temporarily.");
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

                    growl.Notify(new Notification(growlZuseApp.Name, notificationName, "0", title, msg, icn, false,
                                                  Priority.Emergency, ""));
                }
                catch (Exception e)
                {
                    Logger.Send(typeof (Growler), LogLevel.Error, e.Message, e);
                }
            }
        }
    }
}