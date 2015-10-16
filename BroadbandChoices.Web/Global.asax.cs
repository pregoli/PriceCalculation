using Autofac;
using Autofac.Integration.Mvc;
using BroadbandChoices.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BroadbandChoices.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer _container;

        /// <summary>
        /// Gets the container.
        /// </summary>
        public IContainer Container
        {
            get { return _container; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region Dependency injection Autofac
            _container = IocConfig.RegisterDependencies();

            var mvcResolver = new AutofacDependencyResolver(_container);
            DependencyResolver.SetResolver(mvcResolver);
            #endregion
        }
    }
}
