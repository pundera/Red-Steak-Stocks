using Newtonsoft.Json;
using RedSteakStocks.Classes;
using RedSteakStocks.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;

namespace RedSteakStocks.Services
{
    [Export(typeof(ICompanyListService))]
    public class CompanyListService : ICompanyListService
    {
        private List<Company> GetCompanyListFromApi()
        {
            var location = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var fi = new FileInfo($"{location}/Data/companylist.json");
            var stream = fi.OpenText();
            var text = stream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Company>>(text);
        }

        public IList<Company> GetCompanies()
        {
            var companies = GetCompanyListFromApi();
            return companies;
        }
    }
}