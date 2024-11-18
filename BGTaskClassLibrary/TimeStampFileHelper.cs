using System;
using System.IO;

namespace BGTaskClassLibrary
{
    public sealed class TimestampFileHelper
    {
        public bool IsAlreadyNextDay(string logFilePath)
        {
            DateTime lastUpdateDateTime = GetLastUpdateDateTime(logFilePath);
            return DateTime.Today > lastUpdateDateTime.Date;
        }

        private DateTime GetLastUpdateDateTime(string logFilePath)
        {
            if (File.Exists(logFilePath))
            {
                return File.GetLastWriteTime(logFilePath);
            }
            else
            {
                File.Create(logFilePath);
                return File.GetLastWriteTime(logFilePath).AddDays(-1);
            }
        }

        public bool PutTimeStamp(string logFilePath)
        {
            File.Create(logFilePath);
            return true;
        }
    }
}
