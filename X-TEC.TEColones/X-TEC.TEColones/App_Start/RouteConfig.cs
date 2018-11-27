using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace X_TEC.TEColones
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "LogIn", action = "LogIn", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ChangePassword", 
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "LogIn", action = "ChangePassword", id = UrlParameter.Optional });
        }
    }
}
