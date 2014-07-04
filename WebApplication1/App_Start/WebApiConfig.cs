using ConfigurationFileHandler;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Tracing;
using WebApplication1.Utility;


namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IConfigService, StringDetectorConfigService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITraceWriter, CustomizedTraceWriter>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new ProductStore.Resolver.UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Services.Replace(typeof(ITraceWriter), new CustomizedTraceWriter());
       }
    }
}
