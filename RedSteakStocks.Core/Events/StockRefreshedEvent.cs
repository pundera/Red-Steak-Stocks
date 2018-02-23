using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaVantageApiWrapper.AlphaVantageApiWrapper;

namespace RedSteakStocks.Core.Events
{
    public class StockRefreshedEvent: PubSubEvent<AlphaVantageRootObject>
    {
    }
}
