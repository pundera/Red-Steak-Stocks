using RedSteakStocks.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RedSteakStocks.Classes;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using RedSteakStocks.Interfaces;

namespace RedSteakStocks.ViewModels
{
    [Export(typeof(CompanySelectionViewModel))]
    public class CompanySelectionViewModel : BindableBase
    {
        [ImportingConstructor]
        public CompanySelectionViewModel(ICompanyListService companyListService, ISelectionCompanyListService selectionService)
        {
            var list = companyListService.GetCompanyList();
            var companies = (list.ConvertAll((ccc) => new CompanyToSelect()
            {
                Symbol = ccc.Symbol,
                Name = ccc.Name,
                Industry = ccc.Industry,
                Sector = ccc.Sector
            }));

            selectionService.Companies = companies;

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
