﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;



namespace NCron.Integration.log4net
{
    /// <summary>
    /// Implements the <see cref="Logging.ILogFactory"/> interface using "log4net" as log provider.
    /// </summary>
    public class LogFactory : NCron.Logging.ILogFactory
    {
        

        static LogFactory()
        {
            
            BasicConfigurator.Configure();
        }

        public  NCron.Logging.ILog GetLogForJob(NCron.ICronJob job)
        {
            
            var internalLogger = LogManager.GetLogger(job.GetType());
            return new LogAdapter(internalLogger);
        }
        
        internal class LogAdapter : NCron.Logging.ILog
        {
            private readonly ILog _log;

            public LogAdapter(ILog log)
            {
                _log = log;
            }

            public void Debug(Func<string> msgCallback)
            {
                if (_log.IsDebugEnabled)
                    _log.Debug(msgCallback());
            }

            public void Debug(Func<string> msgCallback, Func<Exception> exCallback)
            {
                if (_log.IsDebugEnabled)
                    _log.Debug(msgCallback(), exCallback());
            }

            public void Info(Func<string> msgCallback)
            {
                if (_log.IsInfoEnabled)
                    _log.Info(msgCallback());
            }

            public void Info(Func<string> msgCallback, Func<Exception> exCallback)
            {
                if (_log.IsInfoEnabled)
                    _log.Info(msgCallback(), exCallback());
            }

            public void Warn(Func<string> msgCallback)
            {
                if (_log.IsWarnEnabled)
                    _log.Warn(msgCallback());
            }

            public void Warn(Func<string> msgCallback, Func<Exception> exCallback)
            {
                if (_log.IsWarnEnabled)
                    _log.Warn(msgCallback(), exCallback());
            }

            public void Error(Func<string> msgCallback)
            {
                if (_log.IsErrorEnabled)
                    _log.Error(msgCallback());
            }

            public void Error(Func<string> msgCallback, Func<Exception> exCallback)
            {
                if (_log.IsErrorEnabled)
                    _log.Error(msgCallback(), exCallback());
            }

            public void Dispose()
            {
            }
        }
    }
}
