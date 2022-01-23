using Prism.Mvvm;
using RedSteakStocks.Singletons;

namespace RedSteakStocks.ViewModels.Base
{
    public class ViewModelBase : BindableBase
    {
        private ObservableLocDictionary __;

        public ObservableLocDictionary _
        {
            get { return __; }
            set { SetProperty(ref __, value); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase()
        {
            _ = new ObservableLocDictionary(this);

            var lang = LanguageHolder.Instance.GetLanguage();
            _.Reload(lang);
        }

        public virtual void Destroy()
        {
        }
    }
}