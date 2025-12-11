using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace eShop.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Dependency Injection Using Unity
            UnityConfig.RegisterComponents();
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}