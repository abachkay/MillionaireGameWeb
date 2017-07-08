using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MillionaireGameWeb.BLL;

namespace MillionaireGameWeb.UI.Code
{
    public class DependencyInjectionBindings : NinjectModule
    {       
        public override void Load()
        {
            Bind<IMillionaireGameManager>().To<SimpleMillionaireGameManager>().WithConstructorArgument("xmlUrl", AppDomain.CurrentDomain.BaseDirectory + "App_Data/Data.xml");

        }
    }
}