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
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
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
        public static void Init(string log_path)
        {
            var rfa = new RollingFileAppender();
            rfa.AppendToFile = true;
            rfa.DatePattern = "yyyy-MM-dd";
            rfa.File = log_path;
            rfa.StaticLogFileName = false;
            rfa.Layout = new XmlLayout();
            rfa.MaxFileSize = 1024*1024;
            rfa.Threshold = Level.All;
            rfa.LockingModel = new FileAppender.MinimalLock();
            rfa.ActivateOptions();

            ILoggerRepository repo = LogManager.CreateRepository("Zuse");
            BasicConfigurator.Configure(repo, rfa);
        }

        public static void Send(Type sender, LogLevel level, string msg)
        {
            Send(sender, level, msg, null);
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
        }
    }
}