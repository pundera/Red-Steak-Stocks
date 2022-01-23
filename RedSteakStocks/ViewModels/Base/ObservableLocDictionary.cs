using Newtonsoft.Json;
using RedSteakStocks.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace RedSteakStocks.ViewModels.Base
{
    /// <summary>
    /// Implementation of IObservableMap that supports reentrancy for use as a default view
    /// model.
    /// </summary>
    public class ObservableLocDictionary : ObservableCollection<KeyValue>
    {
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public ObservableLocDictionary(ViewModelBase viewModel) : base()
        {
            ViewModel = viewModel;
        }

        public ObservableLocDictionary(ViewModelBase viewModel, IEnumerable<KeyValue> collection) : base(collection)
        {
            ViewModel = viewModel;
        }

        public ObservableLocDictionary(ViewModelBase viewModel, List<KeyValue> list) : base(list)
        {
            ViewModel = viewModel;
        }

        public ViewModelBase ViewModel { get; }

        public void Reload(LanguageEnum language)
        {
            dictionary = new Dictionary<string, string>();

            this.ToList().ForEach(kv => Remove(kv));

            var vmName = ViewModel.GetType().Name;
            var locName = vmName.Replace("ViewModel", "").Replace("Page", "");

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = $"RedSteakStocks.Localization.{language}.{locName}.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(reader))
                {
                    string val = null;
                    string key = null;

                    while (jsonReader.Read())
                    {
                        if (jsonReader.Value != null)
                        {
                            if (jsonReader.TokenType == JsonToken.String)
                            {
                                val = jsonReader.Value.ToString();
                            }
                            if (jsonReader.TokenType == JsonToken.PropertyName)
                            {
                                var k = jsonReader.Value.ToString();
                                key = k[0].ToString().ToUpperInvariant() + k.Substring(1);
                            }
                        }

                        if (val != null && key != null)
                        {
                            Add(key, val);
                            val = null;
                            key = null;
                        }
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void ClearItems()
        {
            base.ClearItems();
        }

        protected override void InsertItem(int index, KeyValue item)
        {
            base.InsertItem(index, item);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, KeyValue item)
        {
            base.SetItem(index, item);
        }

        public void Add(string key, string value)
        {
            if (value != null)
            {
                dictionary.Add(key, value);
                Add(new KeyValue(key, value));
            }
        }

        public string this[string key]
        {
            get
            {
                return this.dictionary[key];
            }
        }
    }
}