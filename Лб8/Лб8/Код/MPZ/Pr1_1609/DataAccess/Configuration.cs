using System;
using System.IO;
using System.Text.Json;

namespace Pr1_1609.DataAccess
{
    public sealed class Configuration
    {
        public string DataDirectory { get; set; } = "JSONData";
        public string LogFile { get; set; } = "log.txt";
        public bool BackupOnSave { get; set; } = true;

        public static Configuration Load(string fileName = "appsettings.json")
        {
            string path = Path.Combine(AppContext.BaseDirectory, fileName);
            if (!File.Exists(path)) return new Configuration();

            try
            {
                return JsonSerializer.Deserialize<Configuration>(File.ReadAllText(path)) ?? new Configuration();
            }
            catch (JsonException)
            {
                Console.WriteLine($"[Попередження] {fileName} пошкоджено, використано налаштування за замовчуванням.");
                return new Configuration();
            }
        }

        public string GetDataPath(string fileName) => Path.Combine(Resolve(DataDirectory), fileName);
        public string GetLogPath() => Resolve(LogFile);

        private static string Resolve(string path) =>
            Path.IsPathRooted(path) ? path : Path.Combine(AppContext.BaseDirectory, path);
    }
}
