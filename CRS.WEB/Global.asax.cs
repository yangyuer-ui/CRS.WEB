using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CRS.Domain;
using System.Reflection;

namespace CRS.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //≥ı ºªØcastle activerecord≈‰÷√
            IConfigurationSource souce = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
            ActiveRecordStarter.Initialize(Assembly.Load("CRS.Domain"), souce);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
