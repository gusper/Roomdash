using System.Web.Mvc;

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
