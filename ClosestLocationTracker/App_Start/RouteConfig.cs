using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClosestLocationTracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          //  routes.MapRoute(
          //    name: "Default",
          //    url: "DistanceCalculator/{controller}/{action}/{id}",
          //    defaults: new { controller = "search", action = "Index", id = UrlParameter.Optional }
          //).DataTokens.Add("area", "Admin"); ;
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
            ).DataTokens.Add("area", "DistanceCalculator"); ;

            
        }
    }
}
