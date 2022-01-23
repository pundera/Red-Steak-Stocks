using RedSteakStocks.Classes;
using RedSteakStocks.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace RedSteakStocks.Services
{
    [Export(typeof(ISelectionCompanyListService))]
    public class SelectionCompanyListService : ISelectionCompanyListService
    {
        public IList<CompanyToSelect> Companies { get; set; }
    }
}