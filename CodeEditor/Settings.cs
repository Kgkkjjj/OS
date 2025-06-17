using System;
using System.IO;
using System.Text.Json;

namespace CodeEditor
{
    public class EditorSettings
    {
        public string FontFamily { get; set; } = "Consolas";
        public double FontSize { get; set; } = 12;

        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

        public static EditorSettings Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    var settings = JsonSerializer.Deserialize<EditorSettings>(json);
                    if (settings != null)
                        return settings;
                }
            }
            catch { }
            return new EditorSettings();
        }

        public void Save()
        {
            try
            {
                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch { }
        }
    }
}
