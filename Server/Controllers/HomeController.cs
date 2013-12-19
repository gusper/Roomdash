using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Engine;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                Response.Redirect(@"/project/f12");
            }

            ViewData["projectId"] = projectId;
            return View();
        }
    }
}
