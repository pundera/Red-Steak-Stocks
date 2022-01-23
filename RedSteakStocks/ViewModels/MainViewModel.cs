using Autofac;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using RedSteakStocks.Classes;
using RedSteakStocks.Interfaces;
using RedSteakStocks.Notifications;
using RedSteakStocks.ViewModels.Base;
using System;
using System.Collections.ObjectModel;

using System.ComponentModel.Composition;
using System.Windows.Input;

namespace RedSteakStocks.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : ViewModelBase
    {
        private ICompanyListService _companyListService;
        private IContainer container;

        [ImportingConstructor]
        public MainViewModel(IEventAggregator aggregator, IContainer container, ICompanyListService companyListService) : base()
        {
            eventAggregator = aggregator;

            this.container = container;

            this.CompanySelectionRequest = new InteractionRequest<CompanySelectionNotification>();

            // --- commands ---
            RaiseCompaniesPopupViewCommand = new DelegateCommand(() => RaiseCompaniesPopupView());

            RemoveSelected = new DelegateCommand(() => DoRemoveSelected());
            ReloadSelected = new DelegateCommand(() => DoReloadSelected());
            CheckSelected = new DelegateCommand(() => DoCheckSelected());
            UncheckSelected = new DelegateCommand(() => DoUncheckSelected());

            ExpandAll = new DelegateCommand(() => DoExpandAll());
            CollapseAll = new DelegateCommand(() => DoCollapseAll());
            ReloadAll = new DelegateCommand(() => DoReloadAll());
            CheckAll = new DelegateCommand(() => DoCheckAll());
            UncheckAll = new DelegateCommand(() => DoUncheckAll());

            RemoveAll = new DelegateCommand(() => DoRemoveAll());

            CheckAllInterval = new DelegateCommand<string>(DoCheckAllInterval);
            UncheckAllInterval = new DelegateCommand<string>(DoUncheckAllInterval);

            _companyListService = companyListService;
        }

        private IEventAggregator eventAggregator;

        private void DoCheckAllInterval(string api)
        {
            foreach (var c in CompaniesToShow)
            {
                foreach (var a in c.Apis)
                {
                    if (a.Interval.Equals(api)) a.IsChecked = true;
                }
            }
        }

        private void DoUncheckAllInterval(string api)
        {
            foreach (var c in CompaniesToShow)
            {
                foreach (var a in c.Apis)
                {
                    if (a.Interval.Equals(api)) a.IsChecked = false;
                }
            }
        }

        private void DoUncheckSelected()
        {
            if (SelectedCompany != null)
                foreach (var a in SelectedCompany.Apis)
                {
                    a.IsChecked = false;
                }
        }

        private void DoCheckSelected()
        {
            if (SelectedCompany != null)
                foreach (var a in SelectedCompany.Apis)
                {
                    a.IsChecked = true;
                }
        }

        private void DoReloadSelected()
        {
            if (SelectedCompany != null)
                foreach (var a in SelectedCompany.Apis)
                {
                    if (a.IsChecked) a.Refresh.Execute(null);
                }
        }

        private void DoUncheckAll()
        {
            foreach (var c in CompaniesToShow)
            {
                foreach (var a in c.Apis)
                {
                    a.IsChecked = false;
                }
            }
        }

        private void DoCheckAll()
        {
            foreach (var c in CompaniesToShow)
            {
                foreach (var a in c.Apis)
                {
                    a.IsChecked = true;
                }
            }
        }

        private void DoReloadAll()
        {
            foreach (var c in CompaniesToShow)
            {
                foreach (var a in c.Apis)
                {
                    if (a.IsChecked) a.Refresh.Execute(null);
                }
            }
        }

        private void DoCollapseAll()
        {
            foreach (var c in CompaniesToShow)
            {
                c.IsExpanded = false;
            }
        }

        private void DoExpandAll()
        {
            foreach (var c in CompaniesToShow)
            {
                c.IsExpanded = true;
            }
        }

        private void DoRemoveSelected()
        {
            CompaniesToShow.Remove(SelectedCompany);
        }

        private void DoRemoveAll()
        {
            CompaniesToShow = new ObservableCollection<CompanyToShow>();
        }

        public ICommand RemoveSelected { get; private set; }
        public ICommand ReloadSelected { get; private set; }
        public ICommand CheckSelected { get; private set; }
        public ICommand UncheckSelected { get; private set; }

        public ICommand ExpandAll { get; private set; }
        public ICommand CollapseAll { get; private set; }
        public ICommand ReloadAll { get; private set; }
        public ICommand CheckAll { get; private set; }
        public ICommand UncheckAll { get; private set; }

        public ICommand RemoveAll { get; private set; }

        public ICommand CheckAllInterval { get; private set; }
        public ICommand UncheckAllInterval { get; private set; }

        private CompanySelectionViewModel companySelectionViewModel;

        [Import]
        public CompanySelectionViewModel CompanySelectionViewModel
        {
            get { return companySelectionViewModel; }
            set { SetProperty(ref companySelectionViewModel, value); }
        }

        private ObservableCollection<CompanyToSelect> companies = new ObservableCollection<CompanyToSelect>();
        public ObservableCollection<CompanyToSelect> Companies { get { return companies; } set { this.SetProperty(ref companies, value); } }

        private CompanyToShow selectedCompany;

        public CompanyToShow SelectedCompany
        {
            get { return selectedCompany; }
            set
            {
                SetProperty(ref selectedCompany, value);
                foreach (var c in CompaniesToShow)
                {
                    if (c == value)
                    {
                        c.IsSelected = true;
                    }
                    else
                    {
                        c.IsSelected = false;
                    }
                }

                this.RaisePropertyChanged("SelectedCompanyHeader");
                this.RaisePropertyChanged("IsSelected");
            }
        }

        public bool IsSelected => (SelectedCompany != null);

        public string SelectedCompanyHeader => (SelectedCompany != null ? $"{SelectedCompany?.Symbol}: {SelectedCompany?.Name}" : " - please add some companies - ");

        private ObservableCollection<CompanyToShow> companiesToShow = new ObservableCollection<CompanyToShow>();
        public ObservableCollection<CompanyToShow> CompaniesToShow { get { return companiesToShow; } set { this.SetProperty(ref companiesToShow, value); } }

        public CompanyToShow ResultCompany { get; set; }

        private InteractionRequest<CompanySelectionNotification> companySelectionRequest;
        public InteractionRequest<CompanySelectionNotification> CompanySelectionRequest { get { return companySelectionRequest; } set { SetProperty(ref companySelectionRequest, value); } }
        public ICommand RaiseCompaniesPopupViewCommand { get; private set; }

        private void RaiseCompaniesPopupView()
        {
            var viewModel = CompanySelectionViewModel;

            var notification = new CompanySelectionNotification(container)
            {
                Title = "Companies"
            };

            this.CompanySelectionRequest.Raise(notification,
                new Action<CompanySelectionNotification>((n) =>
                {
                    var action = new Action(() =>
                    {
                        if (!n.Confirmed) return;

                        var c = n.ViewModel.SelectedItem;
                        if (c != null)
                        {
                            if (SelectedCompany != null) SelectedCompany.IsSelected = false;
                            var newCompany = new CompanyToShow(eventAggregator, c.Symbol, c.Name);
                            CompaniesToShow.Add(newCompany);
                            SelectedCompany = newCompany;
                        }
                    });

                    action.Invoke();
                })
            );
        }
    }
}