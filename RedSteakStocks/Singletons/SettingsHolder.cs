using Newtonsoft.Json;
using RedSteakStocks.UI_Classes;
using System.Collections.Generic;
using System.IO;

namespace RedSteakStocks.Singletons
{
    public sealed class SettingsHolder
    {
        private static Settings settings;

        public Settings GetSettings()
        {
            return settings;
        }

        private static SettingsHolder instance = null;

        public static SettingsHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    var resourceName = $"RedSteakStocks.Settings.Settings.json";

                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEnd();
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            List<LanguageUI> list = new List<LanguageUI>();
                            settings = JsonConvert.DeserializeObject<Settings>(json);
                        }
                    }

                    instance = new SettingsHolder();
                }
                return instance;
            }
        }

        private SettingsHolder()
        {
        }
    }
}