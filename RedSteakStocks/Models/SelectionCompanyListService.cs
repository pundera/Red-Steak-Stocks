using RedSteakStocks.Classes;
using RedSteakStocks.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSteakStocks.Models
{
    [Export(typeof(ISelectionCompanyListService))]
    public class SelectionCompanyListService : ISelectionCompanyListService
    {
        public IList<CompanyToSelect> Companies { get; set; }
    }
}
