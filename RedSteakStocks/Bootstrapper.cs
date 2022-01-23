using Autofac;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Regions;
using RedSteakStocks.Interfaces;
using RedSteakStocks.Notifications;
using RedSteakStocks.Plots;
using RedSteakStocks.Services;
using RedSteakStocks.ViewModels;
using System;
using System.Windows;

namespace RedSteakStocks
{
    internal class Bootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            builder.RegisterType<Shell>();
            builder.RegisterType<CompanyListService>().As<ICompanyListService>();
            builder.RegisterType<SelectionCompanyListService>().As<ISelectionCompanyListService>();

            builder.RegisterType<CompanySelectionViewModel>().AsSelf();
            builder.RegisterType<CompanySelectionNotification>().AsSelf();

            //// register autofac module
            //builder.RegisterModule<LeftModuleRegistry>();
            //builder.RegisterModule<MiddleModuleRegistry>();
            //builder.RegisterModule<RightModuleRegistry>();
        }

        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();

            // View discovery
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainRegion", () => Container.Resolve<MainView>());

            regionManager.RegisterViewWithRegion("Plots", typeof(RedSteakStocks.Plots.Views.TabsView));
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog catalog = (ModuleCatalog)this.ModuleCatalog;

            AddModuleToCatalog(typeof(PlotsModule), catalog);
        }

        private void AddModuleToCatalog(Type ModuleType, ModuleCatalog Catalog)
        {
            ModuleInfo NewModuleInfo = new ModuleInfo()
            {
                ModuleName = ModuleType.AssemblyQualifiedName,
                ModuleType = ModuleType.AssemblyQualifiedName
            };
            Catalog.AddModule(NewModuleInfo);
        }
    }
}