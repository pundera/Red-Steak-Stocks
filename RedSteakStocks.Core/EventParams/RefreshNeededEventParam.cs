using System.Collections.Generic;
using static AlphaVantageApiWrapper.AlphaVantageApiWrapper;

namespace RedSteakStocks.Core.EventParams
{
    public class RefreshNeededEventParam
    {
        public List<ApiParam> Pars { get; set; }
        public string Name { get; set; }
    }
}