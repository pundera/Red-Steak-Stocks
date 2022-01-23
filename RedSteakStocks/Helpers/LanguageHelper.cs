using RedSteakStocks.Singletons;
using RedSteakStocks.UI_Classes;
using System.Collections.Generic;

namespace RedSteakStocks.Helpers
{
    public class LanguageHelper
    {
        public static IList<LanguageUI> GetLanguages()
        {
            return SettingsHolder.Instance.GetSettings().Languages;
        }
    }
}