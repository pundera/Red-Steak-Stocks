using Autofac;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Regions;
using RedSteakStocks.Core.Views;

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