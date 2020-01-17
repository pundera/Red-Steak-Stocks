using Prism.Events;
using RedSteakStocks.Plots.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSteakStocks.Plots.Events
{
    public class TabsHolderChanged: PubSubEvent<ObservableCollection<TabItemSymbol>>
    {

    }
}
