using Autofac;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Regions;
using RedSteakStocks.Plots.Views;

namespace RedSteakStocks.Plots
{
    public class PlotsModule : IModule
    {
        private IRegionManager _regionManager;
        private ContainerBuilder _builder;

        public PlotsModule(ContainerBuilder builder, IRegionManager regionManager)
        {
            _builder = builder;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _builder.RegisterTypeForNavigation<TabsView>();
        }
    }
}