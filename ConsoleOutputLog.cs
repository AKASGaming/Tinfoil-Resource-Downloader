using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tinfoil_Resource_Downloader
{
    public class Output
    {
        private readonly string LogDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tinfoil Resource Downloader" + @"\logs");

        private static Output _outputSingleton;
        private static Output OutputSingleton
        {
            get
            {
                if (_outputSingleton == null)
                {
                    _outputSingleton = new Output();
                }
                return _outputSingleton;
            }
        }

        public StreamWriter SW { get; set; }

        public Output()
        {
            EnsureLogDirectoryExists();
            InstantiateStreamWriter();
        }

        ~Output()
        {
            if (SW != null)
            {
                try
                {
                    SW.Dispose();
                }
                catch (ObjectDisposedException) { } // object already disposed - ignore exception
            }
        }

        public static void WriteLine(string str, Enum Level = default)
        {
            switch (Level)
            {
                case LevelType.Warning:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Warning", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Warning", str));
                    break;
                case LevelType.Error:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Error", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Error", str));
                    break;
                case LevelType.Debug:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Debug", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Debug", str));
                    break;
                case LevelType.Info:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Info", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Info", str));
                    break;
                default:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Debug", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Debug", str));
                    break;
            }
        }

        public static void Write(string str, Enum Level = default)
        {
            switch (Level)
            {
                case LevelType.Warning:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Warning", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Warning", str));
                    break;
                case LevelType.Error:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Error", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Error", str));
                    break;
                case LevelType.Debug:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Debug", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Debug", str));
                    break;
                case LevelType.Info:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Info", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Info", str));
                    break;
                default:
                    Console.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Debug", str));
                    OutputSingleton.SW.Write(string.Format(CultureInfo.InvariantCulture, "[{0}] [{1}] - {2}\n", DateTime.Now, "Debug", str));
                    break;
            }
        }

        private void InstantiateStreamWriter()
        {
            string filePath = Path.Combine(LogDirPath, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")) + ".txt";
            try
            {
                SW = new StreamWriter(filePath);
                SW.AutoFlush = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new ApplicationException(string.Format("Access denied. Could not instantiate StreamWriter using path: {0}.", filePath), ex);
            }
        }

        private void EnsureLogDirectoryExists()
        {
            if (!Directory.Exists(LogDirPath))
            {
                try
                {
                    Directory.CreateDirectory(LogDirPath);
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw new ApplicationException(string.Format("Access denied. Could not create log directory using path: {0}.", LogDirPath), ex);
                }
            }
        }
    }
    public enum LevelType
    {
        Debug,
        Warning,
        Error,
        Info
    }
}
