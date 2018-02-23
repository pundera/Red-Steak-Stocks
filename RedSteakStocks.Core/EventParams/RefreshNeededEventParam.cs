using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaVantageApiWrapper.AlphaVantageApiWrapper;

namespace RedSteakStocks.Core.EventParams
{
    public class RefreshNeededEventParam
    {
        public List<ApiParam> Pars { get; set; }
        public string Name { get; set; }
    }
}
