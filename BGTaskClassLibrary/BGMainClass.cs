using System;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System.IO;
using Windows.Media.Devices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BGTaskClassLibrary 
{
    public sealed class BGMainClass : IBackgroundTask
    {
        private static Microsoft.Extensions.Logging.ILogger _logger;
        //private static UploadDataConfig _uploadConfigData;
        private static TimestampFileHelper _timestampFileHelper;

        string _schedulerConfigFolder = "";
        string _filetoread = "";
        string _timestampfilename = "";

        public BGMainClass()
        {
            // Initialize logger and notification service
            ConfigureLogging(@"C:\ProgramData\FairbancData", "BGTaskError.log");
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new SerilogLoggerProvider());
            });
            _logger = loggerFactory.CreateLogger<BGMainClass>();
        }

        void IBackgroundTask.Run(IBackgroundTaskInstance taskInstance)
        {
            _timestampFileHelper = new TimestampFileHelper();
            _schedulerConfigFolder = @"C:\ProgramData\FairbancData";
            _filetoread = "DateTimeInfo.ini";
            _timestampfilename = "TimeStampFile.ini";

            _logger.LogInformation($"Background task: BGTask starting...");
            //StartChecking();
            Log.CloseAndFlush();

            Debug.WriteLine($"this is：{taskInstance.Task.Name} background");
            SendToast("Background task: Auto upload task performed ...");



        }

        private static void SendToast(string message)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
            XmlNodeList textElements = toastXml.GetElementsByTagName("text");
            textElements[0].AppendChild(toastXml.CreateTextNode("Fairbanc - Auto Upload Schedule"));
            textElements[1].AppendChild(toastXml.CreateTextNode(message));
            ToastNotification notification = new(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }

        private static void ConfigureLogging(string folder, string filename)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(Path.Combine(folder, filename), rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
