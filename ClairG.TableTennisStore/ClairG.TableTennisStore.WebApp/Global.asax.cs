using ClairG.TableTennisStore.Domain.Entities;
using ClairG.TableTennisStore.WebApp.Infrastructure.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClairG.TableTennisStore.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            IocConfig.Register();
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
