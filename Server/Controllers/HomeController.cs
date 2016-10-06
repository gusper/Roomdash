using System.Web.Mvc;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                Response.Redirect(@"/project/vspreview");
            }

            ViewData["projectId"] = projectId;
            return View();
        }
    }
}
