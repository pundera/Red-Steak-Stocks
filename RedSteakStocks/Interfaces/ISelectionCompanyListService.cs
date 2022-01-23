using RedSteakStocks.Classes;
using System.Collections.Generic;

namespace RedSteakStocks.Interfaces
{
    public interface ISelectionCompanyListService
    {
        IList<CompanyToSelect> Companies { get; set; }
    }
}