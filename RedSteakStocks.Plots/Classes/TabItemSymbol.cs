using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSteakStocks.Plots.Classes
{
    public class TabItemSymbol: BindableBase
    {
        public TabItemSymbol(string name)
        {
            Tabs = new ObservableCollection<TabItemPlot>();
            Name = name;
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

        private ObservableCollection<TabItemPlot> tabs;
        public ObservableCollection<TabItemPlot> Tabs
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
