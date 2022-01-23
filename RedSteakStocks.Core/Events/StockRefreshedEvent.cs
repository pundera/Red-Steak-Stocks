using Prism.Events;
using static AlphaVantageApiWrapper.AlphaVantageApiWrapper;

namespace RedSteakStocks.Core.Events
{
    public class StockRefreshedEvent : PubSubEvent<AlphaVantageRootObject>
    {
    }
}