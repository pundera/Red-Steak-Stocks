using RedSteakStocks.Enums;

namespace RedSteakStocks.Singletons
{
    public sealed class LanguageHolder
    {
        private LanguageEnum language = LanguageEnum.Czech;

        public LanguageEnum GetLanguage()
        {
            return language;
        }

        public void SwitchLanguage(LanguageEnum lang)
        {
            language = lang;
        }

        private static LanguageHolder instance = null;

        public static LanguageHolder Instance
        {
            get
            {
                if (instance == null)
                    instance = new LanguageHolder();
                return instance;
            }
        }

        private LanguageHolder()
        {
        }

        public override string ToString()
        {
            return language.ToString();
        }
    }
}