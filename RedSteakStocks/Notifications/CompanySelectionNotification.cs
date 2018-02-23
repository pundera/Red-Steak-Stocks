using Autofac;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using RedSteakStocks.Classes;
using RedSteakStocks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RedSteakStocks.Notifications
{
    [Export(typeof(CompanySelectionNotification))]
    public class CompanySelectionNotification : Confirmation, IInteractionRequestAware
    {
        [ImportingConstructor]
        public CompanySelectionNotification(IContainer container)
        {

            var viewModel = container.Resolve<CompanySelectionViewModel>();

            ViewModel = viewModel;

            ViewModel.SelectItemCommand = new DelegateCommand(AcceptSelectedItem);
            ViewModel.CancelCommand = new DelegateCommand(CancelInteraction);

            Width = 420;
        }

        public double Width { get; set; }

        public CompanySelectionViewModel ViewModel { get; set; }

        // Both the FinishInteraction and Notification properties will be set by the PopupWindowAction
        // when the popup is shown.
        public Action FinishInteraction { get; set; }
        private INotification notification;
        public INotification Notification
        {
            get
            {
                return this.notification;
            }
            set
            {
                if (value is CompanySelectionNotification)
                {
                    // To keep the code simple, this is the only property where we are raising the PropertyChanged event,
                    // as it's required to update the bindings when this property is populated.
                    // Usually you would want to raise this event for other properties too.
                    this.notification = value as CompanySelectionNotification;
                }
            }
        }

        public void AcceptSelectedItem()
        {

            this.Confirmed = true;
            this.FinishInteraction();
        }

        public void CancelInteraction()
        {

            this.Confirmed = false;
            this.FinishInteraction();
        }
    }
}