using RedSteakStocks.Core.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Autofac;
using Prism.Autofac;

namespace RedSteakStocks.Core
{
    public class CoreModule : IModule
    {
        private IRegionManager _regionManager;
        private ContainerBuilder _builder;

        public CoreModule(ContainerBuilder builder, IRegionManager regionManager)
        {
            _builder = builder;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _builder.RegisterTypeForNavigation<ViewA>();
        }
    }
}