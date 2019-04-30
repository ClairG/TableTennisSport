using Autofac;
using Autofac.Integration.Mvc;
using ClairG.TableTennisStore.Domain.Abstract;
using ClairG.TableTennisStore.Domain.Concrete;
using ClairG.TableTennisStore.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClairG.TableTennisStore.WebApp
{
    public class IocConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder
                .RegisterControllers(AppDomain.CurrentDomain.GetAssemblies())
                .PropertiesAutowired();
            builder
                .RegisterType<EFDbContext>()
                .PropertiesAutowired();
            builder
                .RegisterType<EFProductRepository>()
                .As<IProductsRepository>()
                .PropertiesAutowired();
            builder
                .RegisterType<EmailOrderProcessor>()
                .As<IOrderProcessor>()
                .PropertiesAutowired();
            builder
                .RegisterType<EmailSettings>()
                .PropertiesAutowired();
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //register in global.asax
        }
    }
}