using Prism.Events;
using RedSteakStocks.Core.EventParams;

namespace RedSteakStocks.Core.Events
{
    /// <summary>
    /// Published when user wants to refresh some stock data
    /// </summary>
    public class StockRefreshNeededEvent : PubSubEvent<RefreshNeededEventParam>
    {
    }
}