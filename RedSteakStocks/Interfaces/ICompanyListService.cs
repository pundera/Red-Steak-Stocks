using RedSteakStocks.Classes;
using System.Collections.Generic;

namespace RedSteakStocks.Interfaces
{
    public interface ICompanyListService
    {
        IList<Company> GetCompanies();
    }
}