using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RedSteakStocks.Core.Events;
using RedSteakStocks.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using static AlphaVantageApiWrapper.AlphaVantageApiWrapper;

namespace RedSteakStocks.Classes
{
    public class Api : BindableBase
    {
        public Api(IEventAggregator eventAggregator, string symbol, string name, string interval)
        {
            Refresh = new DelegateCommand(DoRefresh);
            Name = name;
            IsChecked = true;
            Interval = interval;

            this.symbol = symbol;

            this.eventAggregator = eventAggregator;
        }

        private IEventAggregator eventAggregator;

        private void DoRefresh()
        {
            var descs = GetEnumDescriptionsHelper.GetDescriptions(typeof(AvIntervalEnum));
            var d = descs.First(x => x.Item2 == Interval).Item1.ToString();
            var interval = d.Replace(" ", "");

            var function = GetEnumDescriptionsHelper.GetInfos(typeof(AvIntervalEnum)).First(x => x.Item2 == Interval).Item1;
            var trigger = GetEnumDescriptionsHelper.GetTriggers(typeof(AvIntervalEnum)).First(x => x.Item2 == Interval).Item1;

            var pars = trigger ? new List<ApiParam>() {
                new ApiParam("function", function),
                new ApiParam("symbol", symbol),
                new ApiParam("interval", interval),
                new ApiParam("outputsize", "full")
            } : new List<ApiParam>()
            {
                new ApiParam("function", function),
                new ApiParam("symbol", symbol)
            };

            var ev = eventAggregator.GetEvent<StockRefreshNeededEvent>();
            ev.Publish(new Core.EventParams.RefreshNeededEventParam() { Pars = pars, Name = d });
        }

        private string symbol;

        public string Interval { get; set; }

        public string Name { get; set; }
        public ICommand ExpandCollapse { get; private set; }
        public ICommand Refresh { get; private set; }

        private bool isChecked;

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                SetProperty(ref isChecked, value);
            }
        }
    }
}