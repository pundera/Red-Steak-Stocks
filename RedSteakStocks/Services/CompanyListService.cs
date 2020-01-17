using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;

using FileHelpers;

using RedSteakStocks.Classes;
using RedSteakStocks.Interfaces;
using Newtonsoft.Json;

namespace RedSteakStocks.Services
{
    [Export(typeof(ICompanyListService))]
    public class CompanyListService: ICompanyListService
    {
        public List<Company> GetCompanyList()
        {
            var location =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var fi = new FileInfo($"{location}/Data/companylist.json");
            var stream = fi.OpenText();
            var text = stream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Company>>(text);
        }

    }
}
