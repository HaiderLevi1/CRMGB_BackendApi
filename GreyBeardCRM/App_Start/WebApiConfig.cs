using GreyBeardCRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace GreyBeardCRM
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*"); // Allow all origins since this is an assignment
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "CustomerApi",
            routeTemplate: "api/customers/{id}",
            defaults: new { controller = "Customer", id = RouteParameter.Optional }
            );

            var container = new UnityContainer();

            container.RegisterType<CustomerDBContext>(new HierarchicalLifetimeManager(), new InjectionFactory(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<CustomerDBContext>();
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CustomerDBCon"].ConnectionString);
                return new CustomerDBContext(optionsBuilder.Options);
            }));

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
