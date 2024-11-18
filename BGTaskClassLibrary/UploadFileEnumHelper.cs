using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;

namespace BGTaskClassLibrary
{
    public sealed class UploadFileEnumHelper
    {
        // Use a private setter to allow initialization but prevent external modification
        public static ALogger _logger;




        private static string GetFiles(string strPattern, string dirPath)
        {
            var searchPattern = $"*{strPattern}*.xls*";
            var file = new DirectoryInfo(dirPath)
                .GetFiles(searchPattern, SearchOption.TopDirectoryOnly)
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();

            return file != null
                ? file.FullName
                : $">>>> [OUTPUT] No excel file found with name '*{strPattern}*'";
        }

        private static FileInfo GetListFilesInfo(string strPattern, string dirPath)
        {
            var searchPattern = $"*{strPattern}*.xls*";
            return new DirectoryInfo(dirPath)
                .GetFiles(searchPattern, SearchOption.TopDirectoryOnly)
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();
        }

        private static List<string> GetLatestFileInfo(List<FileInfo> files)
        {
            files.RemoveAll(item => item == null);
            if (!files.Any()) return null;

            var latestFile = files.OrderByDescending(f => f.LastWriteTime).First();
            return new List<string> { latestFile.FullName, latestFile.Name };
        }



        public static void Finished(string sourceDir, string destDir)
        {
            _logger.LogInformation(">>>> [INFO] Excel Data Sharing process will be completed soon!");
            _logger.LogInformation(">>>> [INFO] Please wait, data is being uploaded.\n");
        }

        public static string GetLatestFileName(IList<string> strFilePattern, string strPath, int mode, string strSearchSubFolder, ALogger logger) /// Note int MODE param was the FileTypeEnum, but failed to pass compiler checking
        {
            _logger = logger;
            IList<string> list2 = null;
            if (mode == 0)
            {
                List<FileInfo> list = new List<FileInfo>();
                foreach (string item in strFilePattern)
                {
                    _logger.LogInformation(">>>> [OUTPUT] Mencari file Penjualan yang mempunyai nama '*" + item.Trim() + "*'...");
                    _logger.LogInformation(GetFiles(item, strPath));
                    if (GetListFilesInfo(item, strPath) != null)
                    {
                        list.Add(GetListFilesInfo(item, strPath));
                    }
                    if (!(strSearchSubFolder == "Y"))
                    {
                        break;
                    }
                    DirectoryInfo directoryInfo = new DirectoryInfo(strPath);
                    DirectoryInfo[] directories = directoryInfo.GetDirectories();
                    foreach (DirectoryInfo directoryInfo2 in directories)
                    {
                        if (GetListFilesInfo(item, directoryInfo2.FullName) != null && directoryInfo2.Name != "upload")
                        {
                            _logger.LogInformation(GetFiles(item, directoryInfo2.FullName));
                            list.Add(GetListFilesInfo(item, directoryInfo2.FullName));
                        }
                    }
                }
                list2 = strLatesFileOf(list);
                if (list2 != null)
                {
                    _logger.LogInformation("============================================");
                    _logger.LogInformation(">>>> [RESULT] File Penjualan yang akan di upload adalah : \n" + list2.First());
                }
                else
                {
                    _logger.LogInformation("********************************************");
                    _logger.LogInformation(">>>> [RESULT] Tidak ada file Penjualan di temukan !! ");
                    _logger.LogInformation("********************************************\n");
                }
            }
            else if (mode == 1)
            {
                List<FileInfo> list3 = new List<FileInfo>();
                foreach (string item2 in strFilePattern)
                {
                    _logger.LogInformation(">>>> [OUTPUT] Mencari file Pembayaran yang mempunya nama '*" + item2.Trim() + "*'...");
                    _logger.LogInformation(GetFiles(item2, strPath));
                    if (GetListFilesInfo(item2, strPath) != null)
                    {
                        list3.Add(GetListFilesInfo(item2, strPath));
                    }
                    if (!(strSearchSubFolder == "Y"))
                    {
                        break;
                    }
                    DirectoryInfo directoryInfo3 = new DirectoryInfo(strPath);
                    DirectoryInfo[] directories2 = directoryInfo3.GetDirectories();
                    foreach (DirectoryInfo directoryInfo4 in directories2)
                    {
                        if (GetListFilesInfo(item2, directoryInfo4.FullName) != null && directoryInfo4.Name != "upload")
                        {
                            _logger.LogInformation(GetFiles(item2, directoryInfo4.FullName));
                            list3.Add(GetListFilesInfo(item2, directoryInfo4.FullName));
                        }
                    }
                }
                list2 = strLatesFileOf(list3);
                if (list2 != null)
                {
                    _logger.LogInformation("============================================");
                    _logger.LogInformation(">>>> [RESULT] File Pembayaran yang akan di upload adalah : \n" + list2.First());
                }
                else
                {
                    _logger.LogInformation("********************************************");
                    _logger.LogInformation(">>>> [RESULT] Tidak ada file Pembayaran di temukan !!");
                    _logger.LogInformation("********************************************\n");
                }
            }
            else
            {
                List<FileInfo> list4 = new List<FileInfo>();
                foreach (string item3 in strFilePattern)
                {
                    _logger.LogInformation(">>>> [OUTPUT] Mencari file Outlet yang mempunya nama '*" + item3.Trim() + "*'...");
                    _logger.LogInformation(GetFiles(item3, strPath));
                    if (GetListFilesInfo(item3, strPath) != null)
                    {
                        list4.Add(GetListFilesInfo(item3, strPath));
                    }
                    if (!(strSearchSubFolder == "Y"))
                    {
                        break;
                    }
                    DirectoryInfo directoryInfo5 = new DirectoryInfo(strPath);
                    DirectoryInfo[] directories3 = directoryInfo5.GetDirectories();
                    foreach (DirectoryInfo directoryInfo6 in directories3)
                    {
                        if (GetListFilesInfo(item3, directoryInfo6.FullName) != null && directoryInfo6.Name != "upload")
                        {
                            _logger.LogInformation(GetFiles(item3, directoryInfo6.FullName));
                            list4.Add(GetListFilesInfo(item3, directoryInfo6.FullName));
                        }
                    }
                }
                list2 = strLatesFileOf(list4);
                if (list2 != null)
                {
                    _logger.LogInformation("============================================");
                    _logger.LogInformation(">>>> [RESULT] File Outlet yang akan di upload adalah : \n" + list2.First());
                }
                else
                {
                    _logger.LogInformation("********************************************");
                    _logger.LogInformation(">>>> [RESULT] Tidak ada file Outlet di temukan !!");
                    _logger.LogInformation("********************************************\n");
                }
            }
            if (list2 != null)
            {
                _logger.LogInformation(">>>> [OUTPUT] Dan waktu akses terakhir file tsb : " + File.GetLastWriteTime(list2.First()).ToLocalTime());
                _logger.LogInformation("============================================\n");
                return list2.First();
            }
            else
            {
                return "";
            }
        }

        private static List<string> strLatesFileOf(List<FileInfo> files)
        {
            files.RemoveAll((item) => item == null);
            string text = "";
            string text2 = "";
            if (files.Any())
            {
                text = files.First().FullName;
                DateTime dateTime = files.First().LastWriteTime;
                foreach (FileInfo file in files)
                {
                    DateTime lastWriteTime = file.LastWriteTime;
                    if (lastWriteTime > dateTime)
                    {
                        dateTime = lastWriteTime;
                        text = file.FullName;
                        text2 = file.Name;
                    }
                }
                return new List<string> { text, text2 };
            }
            return null;
        }
    }




    public sealed class ALogger
    {
        private static readonly Lazy<ALogger> _instance = new Lazy<ALogger>(() => new ALogger());
        private static string _logFolder;
        private static string _logFile;
        private static readonly object _lock = new object();

        // Singleton instance
        public static ALogger Instance => _instance.Value;

        public ALogger() { }

        public void Configure(string logFolder, string logFile)
        {
            _logFolder = logFolder;
            _logFile = logFile;

            // Ensure the log directory exists
            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }
        }

        public void LogInformation(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        public void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }

        private void WriteLog(string logLevel, string message)
        {
            var logFilePath = Path.Combine(_logFolder, _logFile);
            var logEntry = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{logLevel}] {message}";

            lock (_lock)
            {
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
        }
    }

}
