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
using System.Linq;
using System.Text;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository;

namespace Zuse.Core
{
    public enum LogLevel
    {
        Error = 2,
        Warning = 1,
        Info = 0
    }

    public static class Logger
    {
        static Logger()
        {
        }

        public static void Init(string log_path)
        {
            RollingFileAppender rfa = new RollingFileAppender();
            rfa.AppendToFile = true;
            rfa.DatePattern = "yyyy-MM-dd";
            rfa.File = log_path;
            rfa.StaticLogFileName = false;
            rfa.Layout = new log4net.Layout.XmlLayout();
            rfa.MaxFileSize = 1024 * 1024;
            rfa.Threshold = Level.All;
            rfa.LockingModel = new FileAppender.MinimalLock();
            rfa.ActivateOptions();

            ILoggerRepository repo = LogManager.CreateRepository("Zuse");
            BasicConfigurator.Configure(repo, rfa);
        }

        public static void Send(Type sender, LogLevel level, string msg)
        {
            Logger.Send(sender, level, msg, null);
        }

        public static void Send(Type sender, LogLevel level, string msg, Exception e)
        {
            ILog log = LogManager.GetLogger("Zuse", sender);

            switch (level)
            {
                case LogLevel.Error:
                    log.Error(msg, e);
                    break;
                case LogLevel.Warning:
                    if (e == null) log.Warn(msg);
                    else log.Warn(msg, e);
                    break;
                case LogLevel.Info:
                    log.Info(msg);
                    break;
            }

            Type llt = typeof(Zuse.Core.LogLevel);
            LogLevel growlLevel = (LogLevel)Enum.ToObject(llt, ZuseSettings.LoggerGrowlLevel);

            /* GrowlLevel says that anything greater or equal to it's value is Growl'd on screen.
             * 
             * For example:
             *   level = Error/2; (an error being logged),
             *   growlLevel = Warning/1; (growl everything if it's a warning or worse)
             *   
             * if (level >= growlLevel) would be true;
             * 
             * But for example:
             *   level = Info/0; (trivial info being logged),
             *   growlLevel = Error/2; (growl only Errors)
             *   
             * if (level >= growlLevel) would be false;
             */
            if (level >= growlLevel)
            {
                string alertType = Enum.GetName(llt, level);

                Growler.Notify("Program " + alertType, "Zuse " + alertType, msg);
            }
        }
    }
}
