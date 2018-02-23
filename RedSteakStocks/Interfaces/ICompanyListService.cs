using RedSteakStocks.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSteakStocks.Interfaces
{
    public interface ICompanyListService
    {
        List<Company> GetCompanyList();
    }
}
