using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Prism.Ioc;
using Prism.Unity;
using WpfApp1.Views;

namespace WpfApp1
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<Configuration>();
            //      throw new NotImplementedException();
        }

        protected override Window CreateShell()
        {
       //     throw new NotImplementedException();
       var w = Container.Resolve<MainWindow>();
       return w;
        }
    }
    

}