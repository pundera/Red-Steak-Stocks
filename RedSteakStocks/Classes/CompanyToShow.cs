using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RedSteakStocks.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AlphaVantageApiWrapper.AlphaVantageApiWrapper;

namespace RedSteakStocks.Classes
{
    public class CompanyToShow: BindableBase
    {
        private bool isSelected;
        public bool IsSelected {
            get
            {
                return isSelected;
            }
            set
            {
                SetProperty(ref isSelected, value);
            }
        }
        public string Symbol { get; set; }
        public string Name { get; set; }

        private bool isExpanded;
        public bool IsExpanded {
            get
            {
                return isExpanded;
            }
            set
            {
                SetProperty(ref isExpanded, value);
            }
        }

        private IList<Api> apis;
        public IList<Api> Apis {
            get
            {
                return apis;
            }
            set
            {
                apis = value;
            }
        }

        public CompanyToShow(IEventAggregator eventAggregator, string symbol, string name)
        {

            Symbol = symbol;
            Name = name;

            // for every Company will be reevalueted :-( 
            var apiList = GetEnumDescriptionsHelper.GetDescriptions(typeof(AvIntervalEnum)).Select((x) => new Api(eventAggregator, Symbol, x.Item1, x.Item2)).ToList();
            Apis = apiList;

            IsExpanded = true;

        }

    }
}
