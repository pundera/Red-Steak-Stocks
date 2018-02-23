using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace RedSteakStocks.Classes
{
    public class Company
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string LastSale { get; set; }
        public string MarketCap { get; set; }
        public string ADR_TSO { get; set; }
        public string IPOyear { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Summary_Quote { get; set; }
    }
}
