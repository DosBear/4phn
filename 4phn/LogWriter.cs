using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4phn
{
    public class LogWriter
    {
        private static LogWriter instance;
        private static Queue<Log> logQueue;
        public static string logDir;
        private static string logFile = "log.txt";
        private static int maxLogAge = 5;
        private static int queueSize = 1;
        private static DateTime LastFlushed = DateTime.Now;


        private LogWriter() { }

        public static LogWriter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogWriter();
                    logQueue = new Queue<Log>();
                }
                return instance;
            }
        }

        public void WriteToLog(string message)
        {
            lock (logQueue)
            {
                Log logEntry = new Log(message);
                logQueue.Enqueue(logEntry);

                if (logQueue.Count >= queueSize || DoPeriodicFlush())
                {
                    FlushLog();
                }
            }
        }

        private bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - LastFlushed;
            if (logAge.TotalSeconds >= maxLogAge)
            {
                LastFlushed = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FlushLog()
        {
            while (logQueue.Count > 0)
            {
                Log entry = logQueue.Dequeue();
                string logPath = logDir + entry.LogDate + "_" + logFile;
                try
                {
                    using (FileStream fs = File.Open(logPath, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter log = new StreamWriter(fs))
                        {
                            log.WriteLine(string.Format("{0}\t{1}", entry.LogTime, entry.Message));
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }

    public class Log
    {
        private string _Message;
        private string _LogTime;
        private string _LogDate;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string LogTime
        {
            get { return _LogTime; }
            set { _LogTime = value; }
        }
        public string LogDate
        {
            get { return _LogDate; }
            set { _LogDate = value; }
        }

        public Log(string message)
        {
            Message = message;
            LogDate = DateTime.Now.ToString("yyyy-MM-dd");
            LogTime = DateTime.Now.ToString("hh:mm:ss.fff tt");
        }
    }
}
