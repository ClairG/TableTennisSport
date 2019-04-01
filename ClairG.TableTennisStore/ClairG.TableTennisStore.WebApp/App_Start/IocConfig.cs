using Autofac;
using Autofac.Integration.Mvc;
using ClairG.TableTennisStore.Domain.Abstract;
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

            builder.RegisterControllers(AppDomain.CurrentDomain.GetAssemblies()).PropertiesAutowired();

            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock
                .Setup(m => m.Products)
                .Returns(new List<Product>
                {
                    new Product { Name = "Shoes", Price = 70 },
                    new Product { Name = "Shirts", Price = 65 },
                    new Product { Name = "Table", Price = 2300 }
                });
            builder
                .RegisterInstance<IProductsRepository>(mock.Object)
                .PropertiesAutowired();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            //最后在Global里注册
        }
    }
}