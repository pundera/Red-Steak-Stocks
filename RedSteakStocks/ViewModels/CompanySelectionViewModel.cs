using Prism.Mvvm;
using RedSteakStocks.Classes;
using RedSteakStocks.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace RedSteakStocks.ViewModels
{
    [Export(typeof(CompanySelectionViewModel))]
    public class CompanySelectionViewModel : BindableBase
    {
        [ImportingConstructor]
        public CompanySelectionViewModel(ICompanyListService companyListService, ISelectionCompanyListService selectionService)
        {
            var list = companyListService.GetCompanies();
            var companies = (list.Select((ccc) => new CompanyToSelect()
            {
                Symbol = ccc.Symbol,
                Name = ccc.Name,
                Industry = ccc.Industry,
                Sector = ccc.Sector
            }));

            selectionService.Companies = companies.ToList();

            this.Items = new ObservableCollection<CompanyToSelect>(selectionService.Companies);
            if (items?.Count > 0) SelectedItem = items[0];

            Confirmed = false;
        }

        private ObservableCollection<CompanyToSelect> items;

        public ObservableCollection<CompanyToSelect> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }

        public CompanyToSelect SelectedItem { get; set; }

        public bool Confirmed { get; set; }

        public ICommand SelectItemCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}