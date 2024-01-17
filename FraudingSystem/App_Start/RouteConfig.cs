using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ext_FraudingSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = new NotEqual("AdminSettings") }
          );
            routes.MapRoute(
    name: "AdminSettingsRoute",
    url: "en/WhistleBlowingSystem/DomainRouting/251112/{action}",
    defaults: new { controller = "AdminSettings", action = "Index", id = UrlParameter.Optional }
);

        }
    }
}
