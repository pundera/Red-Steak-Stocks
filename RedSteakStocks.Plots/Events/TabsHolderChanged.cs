using Prism.Events;
using RedSteakStocks.Plots.Classes;
using System.Collections.ObjectModel;

namespace RedSteakStocks.Plots.Events
{
    public class TabsHolderChanged : PubSubEvent<ObservableCollection<TabItemSymbol>>
    {
    }
}