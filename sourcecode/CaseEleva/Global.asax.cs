using CaseEleva.App_Start;
using System;
using System.Web.Optimization;
using System.Web.Routing;

namespace CaseEleva
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalizationConfig.RegisterGlobalizationController();
        }
    }
}
