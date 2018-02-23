using RedSteakStocks.Plots.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Autofac;
using Prism.Autofac;

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