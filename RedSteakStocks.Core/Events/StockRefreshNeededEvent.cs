using Prism.Events;
using RedSteakStocks.Core.EventParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaVantageApiWrapper.AlphaVantageApiWrapper;

namespace RedSteakStocks.Core.Events
{
    /// <summary>
    /// Published when user wants to refresh some stock data
    /// </summary>
    public class StockRefreshNeededEvent: PubSubEvent<RefreshNeededEventParam>
    {
    }
}
