using RedSteakStocks.ViewModels;
using System.ComponentModel.Composition;

namespace RedSteakStocks
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [Export(typeof(MainView))]
    public partial class MainView
    {
        [ImportingConstructor]
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}