﻿using System;

namespace XLog
{
    public class Logger : ILogger
    {
        private readonly string _tag;
        private readonly LogConfig _config;

        public Logger(string tag, LogConfig config)
        {
            _tag = tag;
            _config = config;
        }

        public string Tag
        {
            get { return _tag; }
        }

        public bool IsTraceEnabled
        {
            get { return IsEnabled(LogLevel.Trace); }
        }

        public bool IsDebugEnabled
        {
            get { return IsEnabled(LogLevel.Debug); }
        }

        public bool IsInfoEnabled
        {
            get { return IsEnabled(LogLevel.Info); }
        }

        public bool IsWarnEnabled
        {
            get { return IsEnabled(LogLevel.Warn); }
        }

        public bool IsErrorEnabled
        {
            get { return IsEnabled(LogLevel.Error); }
        }

        public bool IsFatalEnabled
        {
            get { return IsEnabled(LogLevel.Fatal); }
        }

        public void Trace(string message, Exception ex = null)
        {
            Log(LogLevel.Trace, message, ex);
        }

        public void Trace(string message, params object[] ps)
        {
            Log(LogLevel.Trace, message, ps);
        }

        public void Debug(string message, Exception ex = null)
        {
            Log(LogLevel.Debug, message, ex);
        }

        public void Debug(string message, params object[] ps)
        {
            Log(LogLevel.Debug, message, ps);
        }

        public void Info(string message, Exception ex = null)
        {
            Log(LogLevel.Info, message, ex);
        }

        public void Info(string message, params object[] ps)
        {
            Log(LogLevel.Info, message, ps);
        }

        public void Warn(string message, Exception ex = null)
        {
            Log(LogLevel.Warn, message, ex);
        }

        public void Warn(string message, params object[] ps)
        {
            Log(LogLevel.Warn, message, ps);
        }

        public void Error(string message, Exception ex = null)
        {
            Log(LogLevel.Error, message, ex);
        }

        public void Error(string message, params object[] ps)
        {
            Log(LogLevel.Error, message, ps);
        }

        public void Fatal(string message, Exception ex = null)
        {
            Log(LogLevel.Fatal, message, ex);
        }

        public void Fatal(string message, params object[] ps)
        {
            Log(LogLevel.Fatal, message, ps);
        }

        public void Log(int logLevel, string message, Exception ex)
        {
            LogInternal(logLevel, message, null, ex, false);
        }

        public void Log(int logLevel, string message, params object[] ps)
        {
            LogInternal(logLevel, message, ps, null, true);
        }

        public bool IsEnabled(int level)
        {
            return _config.IsLevelEnabled(level);
        }

        private void LogInternal(int logLevel, string message, object[] ps, Exception ex, bool doFormat)
        {
            if (!_config.IsEnabled)
            {
                return;
            }

            if (!_config.IsLevelEnabled(logLevel))
            {
                return;
            }

            if (doFormat)
            {
                message = string.Format(message, ps);
            }

            var entry = new Entry(logLevel, _tag, message, ex);
            foreach (var target in _config.GetTargets(logLevel))
            {
                try
                {
                    target.Write(entry);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Target write failed. --> {0}", e);
                }
            }
        }
    }
}
