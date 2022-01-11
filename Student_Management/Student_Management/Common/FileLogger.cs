using System;
using System.Globalization;
using System.IO;

namespace Student_Management.Common
{
    public class FileLogger
    {
        public static void Log(string message, string fileType = null)
        {
            var line = Environment.NewLine + Environment.NewLine;
            try
            {
                var fullFilePath = CreateFile(fileType);
                using (StreamWriter sw = File.AppendText(fullFilePath))
                {
                    sw.WriteLine("-----------Checkpoint Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine("Info: " + message);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public static void LogException(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;
            try
            {
                var fullFilePath = CreateFile();
                var logDetails = "Exception Details: " + ex.ToJson() + line;
                using (StreamWriter sw = File.AppendText(fullFilePath))
                {
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(logDetails);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        private static string CreateFile(string fileType = null)
        {
            var baseLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            var dailyFolderPath = Path.Combine(baseLogPath, DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
            var fullFilePath = Path.Combine(dailyFolderPath, "LogFile_" + DateTime.Now.ToString("HH", CultureInfo.InvariantCulture) + ".txt");
            if (!string.IsNullOrEmpty(fileType))
            {
                fullFilePath = Path.Combine(dailyFolderPath, fileType + DateTime.Now.ToString("HH", CultureInfo.InvariantCulture) + ".txt");
            }

            if (!Directory.Exists(baseLogPath))
            {
                Directory.CreateDirectory(baseLogPath);
            }
            if (!Directory.Exists(dailyFolderPath))
            {
                Directory.CreateDirectory(dailyFolderPath);
            }
            if (!File.Exists(fullFilePath))
            {
                File.Create(fullFilePath).Dispose();
            }

            return fullFilePath;
        }
    }
}