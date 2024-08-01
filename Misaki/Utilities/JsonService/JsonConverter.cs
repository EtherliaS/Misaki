using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Misaki.Utilities.JsonService
{
    public static class JsonConverter
    {
        public static async Task WriteJsonToFileAsync<T>(T obj, string filepath, bool IncludePrivateFields = true)
        {
            EnsureDirectoryExists(filepath);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            if (IncludePrivateFields)
            {
                options.Converters.Add(new PrivateFieldConverter<T>());
            }

            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                await JsonSerializer.SerializeAsync(fs, obj, options);
            }
        }
        public static async Task<T?> ReadJsonFileAsync<T>(string filepath)
        {
            EnsureDirectoryExists(filepath);
            if (File.Exists(filepath))
            {
                try
                {
                    using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        return await JsonSerializer.DeserializeAsync<T>(fs);
                    }
                }
                catch(Exception ex)
                {
                    await Logger.Log("Failed to read json from " + filepath + "\n" + ex.Message, InfoSource.Misaki, InfoType.Error);
                }
            }
            return default;
        }
        private static void EnsureDirectoryExists(string filepath)
        {
            string? directory = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
