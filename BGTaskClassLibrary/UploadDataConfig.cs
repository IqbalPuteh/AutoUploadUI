using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGTaskClassLibrary
{
    public sealed class UploadDataConfig // : IDisposable
    {
        // To detect redundant calls
        //private bool disposedValue;

        public Int16 Date1 { get; private set; }
        public Int16 Date2 { get; private set; }
        public Int16 Date3 { get; private set; }
        public string Time { get; private set; }
        public string Sales { get; private set; }
        public string Repayment { get; private set; }
        public string Outlet { get; private set; }
        public string SourceDataFileFolder { get; private set; } = "";
        public Int32 RunHour { get; private set; } = -1;
        public Int32 RunMinute { get; private set; } = 0;
        public string DTid { get; private set; } = "0";
        public string DistName { get; private set; } = "Test";
        public string AppWorkingFolder { get; private set; }
        public string SchedulerConfigFolder { get; private set; }

        public UploadDataConfig(Int16 date1, Int16 date2, Int16 date3, string runtime,
                                string salesFile, string repaymentFile, string outletFile,
                                Int32 runHour, Int32 runMinute, string schedulerConfigFolder,
                                string sourceDataFileFolder, string dTid = "0", string distName = "Test")
        {
            Date1 = date1;
            Date2 = date2;
            Date3 = date3;
            Time = runtime;
            Sales = salesFile;
            Repayment = repaymentFile;
            Outlet = outletFile;
            RunHour = runHour;
            RunMinute = runMinute;
            SourceDataFileFolder = sourceDataFileFolder;
            SchedulerConfigFolder = schedulerConfigFolder;
            AppWorkingFolder = schedulerConfigFolder + @"\Datasharing-result";
        }

        //protected void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // Dispose managed state (managed objects).
        //            // none.
        //        }
        //        // Set large fields to null.
        //        Sales = null;
        //        Repayment = null;
        //        Outlet = null;
        //        SourceDataFileFolder = null;
        //        SchedulerConfigFolder = null;
        //        AppWorkingFolder = null;

        //        disposedValue = true;
        //    }
        //}

        //~UploadDataConfig()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method.
        //    Dispose(disposing: false);
        //}

        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method.
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
