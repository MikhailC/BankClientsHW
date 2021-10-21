using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Prism.Ioc;
using Prism.Unity;
using WpfApp1.Helper;
using WpfApp1.Views;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Configuration>();

      
        }

        protected override Window CreateShell()
        {
       //     throw new NotImplementedException();
       var w = Container.Resolve<MainWindow>();
       return w;
        }
    }
    

}