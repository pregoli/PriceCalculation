using Autofac;
using Autofac.Integration.Mvc;
using BroadbandChoices.Entities.Base;
using BroadbandChoices.Infrastructure;
using BroadbandChoices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BroadbandChoices.Web.App_Start
{
    public class IocConfig
    {
        public static IContainer RegisterDependencies()
        {
            #region Create the builder
            var builder = new ContainerBuilder();
            #endregion

            #region Register all controllers for the assembly
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            #endregion

            #region register infractusture
            builder.RegisterType<Logger>().As<ILogger>();
            #endregion

            #region register services
            builder.RegisterType<BasketService>().As<IBasketService<Product, Basket>>();
            #endregion

            //builder
            //  .RegisterType<PersonDbContext>()
            //  .AsImplementedInterfaces()
            //  .InstancePerLifetimeScope();

            //builder.Register<IRepository<Person>>(c => new PeopleRepository(new PersonDbContext(), new Logger()));

            return builder.Build();
        }
    }
}