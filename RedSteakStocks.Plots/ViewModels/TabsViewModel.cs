using OxyPlot;
using Prism.Events;
using Prism.Mvvm;
using RedSteakStocks.Core.Events;
using RedSteakStocks.Plots.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apiWrapper = AlphaVantageApiWrapper;

namespace RedSteakStocks.Plots.ViewModels
{
    public class TabsViewModel: BindableBase
    {
        public TabsViewModel(IEventAggregator eventAggregator)
        {
            Tabs = new ObservableCollection<TabItemSymbol>();

            var ev = eventAggregator.GetEvent<StockRefreshNeededEvent>();
            ev.Subscribe((p) => {
                Console.WriteLine("In subscribe ... (StockRefreshNeededEvent)");
                AddTabAsync(p.Pars, p.Name);
            });
        }

        private async Task AddTabAsync(List<apiWrapper.AlphaVantageApiWrapper.ApiParam> pars, string name)
        {
            if (pars == null)
            {
                throw new ArgumentNullException(nameof(pars));
            }

            var root = await apiWrapper.AlphaVantageApiWrapper.GetTechnical(pars, "A903Z1G7NAB6A551");

            var title = pars[1].ParamValue;


            var isNewTabSymbol = !Tabs.Any((t) => t.Name.Equals(title));
            var tabSymbol = Tabs.Where((t) => t.Name.Equals(title)).FirstOrDefault() ?? (new TabItemSymbol(title));

            var isNewTabPlot = !tabSymbol.Tabs.Any((t) => t.Name.Equals(name));
            var tabPlot = tabSymbol.Tabs.Where((t) => t.Name.Equals(name)).FirstOrDefault() ?? (new TabItemPlot(name)); //new TabItemPlot(pars.Count > 3 ? pars[2].ParamValue : " - - - ");

            var data = new ObservableCollection<DataPoint>();

            var ix = 0;
            foreach (var t in root.TechnicalsByDate)
            {
                //var start = "";

                //var ddd = t.Data.Aggregate(start, (a, b) => a + b.TechnicalKey + "->" + b.TechnicalValue);
                //Console.WriteLine(t.Date + ":" + ddd);

                foreach (var v in t.Data) if (v.TechnicalKey.Equals("1. open")) data.Add(new DataPoint(t.Date.Ticks, v.TechnicalValue));
                ix++;

            }
            
            if (isNewTabPlot) tabSymbol.Tabs.Add(tabPlot);
            if (isNewTabSymbol) Tabs.Add(tabSymbol);

            tabPlot.Points = data;

        }

        private ObservableCollection<TabItemSymbol> tabs;
        public ObservableCollection<TabItemSymbol> Tabs {
            get
            {
                return tabs;
            }
            set
            {
                SetProperty(ref tabs, value);
            }
        }
    }
}
