using Prism.Events;
using RedSteakStocks.Plots.Classes;
using RedSteakStocks.Plots.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSteakStocks.Plots.Singletons
{
    public sealed class TabsHolder
    {
        TabsHolder()
        {

        }

        TabsHolder(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private IEventAggregator eventAggregator;

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            eventAggregator.GetEvent<TabsHolderChanged>().Publish(Items);
        }

        private ObservableCollection<TabItemSymbol> items = new ObservableCollection<TabItemSymbol>();
        public ObservableCollection<TabItemSymbol> Items { get { return items; } set { items = value; } }

        private static readonly object padlock = new object();
        private static TabsHolder instance = null;

        public static TabsHolder GetInstance(IEventAggregator aggregator)
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new TabsHolder(aggregator);
                    }
                }
            }
            return instance;
        }

        public static TabsHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new TabsHolder();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
