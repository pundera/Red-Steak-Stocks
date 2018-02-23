using Fluent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RedSteakStocks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);

            //// add custom accent and theme resource dictionaries to the ThemeManager
            //// you should replace FluentRibbonThemesSample with your application name
            //// and correct place where your custom accent lives
            //ThemeManager.AddAccent("Cobalt", new Uri("pack://application:,,,/Fluent;component/Themes/Accents/Cobalt.xaml"));

            //// get the current app style (theme and accent) from the application
            //var theme = ThemeManager.GetAppTheme("Cobalt");

            //// now change app style to the custom accent and current theme
            //ThemeManager.ChangeAppStyle(Application.Current,
            //                            ThemeManager.GetAccent("Cobalt"),
            //                            theme);


            var bootstrapper = new Bootstrapper();
            bootstrapper.Run(true);
        }
    }
}
