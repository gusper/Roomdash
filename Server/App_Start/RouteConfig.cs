using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Server
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          name: "BlankDefault",
          url: "",
          defaults: new { controller = "Home", action = "Index", projectId = (string)null }
      );
      routes.MapRoute(
          name: "Projects",
          url: "project/{projectId}",
          defaults: new { controller = "Home", action = "Index", projectId = "f12" }
      );
      //routes.MapRoute(
      //    name: "Default",
      //    url: "{controller}/{action}/{topic}",
      //    defaults: new { controller = "Home", action = "Index", topic = UrlParameter.Optional }
      //);
    }
  }
}