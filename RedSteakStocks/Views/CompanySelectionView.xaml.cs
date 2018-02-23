using Prism.Interactivity.InteractionRequest;
using RedSteakStocks.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RedSteakStocks.Views
{
    /// <summary>
    /// Interaction logic for ItemSelectionView.xaml
    /// </summary>
    [Export(typeof(CompanySelectionView))]
    public partial class CompanySelectionView : UserControl
    {
        //[ImportingConstructor]
        //public CompanySelectionView(CompanySelectionViewModel viewModel)
        //{
        //    InitializeComponent();
        //    this.DataContext = viewModel;
        //}

        public CompanySelectionView()
        {
            InitializeComponent();

            this.Loaded += CompanySelectionView_Loaded;
        }

        private void CompanySelectionView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Parent is Window parentWindow)
            {
                parentWindow.Width = 560;
            }
        }

        ///// <summary>
        ///// Sets or gets the <see cref="IConfirmation"/> shown by this window./>
        ///// </summary>
        //public IConfirmation Confirmation
        //{
        //    get
        //    {
        //        return this.DataContext as IConfirmation;
        //    }
        //    set
        //    {
        //        this.DataContext = value;
        //    }
        //}


    }
}
