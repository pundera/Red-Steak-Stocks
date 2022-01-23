using OxyPlot;
using Prism.Events;
using Prism.Mvvm;
using RedSteakStocks.Core.Events;
using RedSteakStocks.Plots.Classes;
using RedSteakStocks.Plots.Events;
using RedSteakStocks.Plots.Singletons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using apiWrapper = AlphaVantageApiWrapper;

namespace RedSteakStocks.Plots.ViewModels
{
    public class TabsViewModel : BindableBase
    {
        public TabsViewModel(IEventAggregator eventAggregator)
        {
            aggregator = eventAggregator;

            Tabs = new ObservableCollection<TabItemSymbol>();

            eventAggregator.GetEvent<TabsHolderChanged>().Subscribe(items =>
            {
                Tabs = items;
            });

            var ev = eventAggregator.GetEvent<StockRefreshNeededEvent>();
            ev.Subscribe(async (p) =>
            {
                Console.WriteLine("In subscribe ... (StockRefreshNeededEvent)");
                await AddTabAsync(p.Pars, p.Name);

                //.ContinueWith(task => Tabs.ToList().ForEach(tab => tab.Tabs.ToList().ForEach(t => t.Model.InvalidatePlot(true))));
            });
        }

        private readonly IEventAggregator aggregator;

        private async Task AddTabAsync(List<apiWrapper.AlphaVantageApiWrapper.ApiParam> pars, string name)
        {
            if (pars == null)
            {
                throw new ArgumentNullException(nameof(pars));
            }

            var root = await apiWrapper.AlphaVantageApiWrapper.GetTechnical(pars, "A903Z1G7NAB6A551");

            if (root == null)
            {
                // NO DATA
                // ToDo: handle it !!!

                return;
            }

            var title = pars[1].ParamValue;

            var isNewTabSymbol = !TabsHolder.GetInstance(aggregator).Items.Any((t) => t.Name.Equals(title));
            var tabSymbol = TabsHolder.GetInstance(aggregator).Items.Where((t) => t.Name.Equals(title)).FirstOrDefault() ?? (new TabItemSymbol(title));

            var isNewTabPlot = !tabSymbol.Tabs.Any((t) => t.Name.Equals(name));
            var tabPlot = tabSymbol.Tabs.Where((t) => t.Name.Equals(name)).FirstOrDefault() ?? (new TabItemPlot(name)); //new TabItemPlot(pars.Count > 3 ? pars[2].ParamValue : " - - - ");

            var data = new List<DataPoint>();

            var ix = 0;
            foreach (var t in root.TechnicalsByDate)
            {
                //var start = "";

                //var ddd = t.Data.Aggregate(start, (a, b) => a + b.TechnicalKey + "->" + b.TechnicalValue);
                //Console.WriteLine(t.Date + ":" + ddd);

                foreach (var v in t.Data) if (v.TechnicalKey.Equals("1. open")) data.Add(new DataPoint(t.Date.Ticks, v.TechnicalValue));
                ix++;
            }

            if (isNewTabPlot) Application.Current.Dispatcher.Invoke(() =>
            {
                tabSymbol.Tabs.Add(tabPlot);
            });

            if (isNewTabSymbol) Application.Current.Dispatcher.Invoke(() =>
            {
                Tabs.Add(tabSymbol);
            });

            var series = new OxyPlot.Series.LineSeries();
            series.Points.AddRange(data);

            if (tabPlot.Model.Series.Any()) tabPlot.Model.Series.Clear();
            tabPlot.Model.Series.Add(series);
            TabsHolder.GetInstance(aggregator).Items = Tabs;

            //await Task.Yield();

            tabPlot.Model.InvalidatePlot(true);
        }

        private ObservableCollection<TabItemSymbol> tabs;

        public ObservableCollection<TabItemSymbol> Tabs
        {
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