using System.Web;
using System.Web.Optimization;

namespace Server
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            var mainBundle = new ScriptBundle("~/bundles/main").Include(
                "~/frontend/dateutils.js", "~/frontend/main.js");
            mainBundle.Transforms.Add(new JsMinify());
            bundles.Add(mainBundle);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-tmpl").Include(
                "~/Scripts/jquery.tmpl*"));

            bundles.Add(new ScriptBundle("~/bundles/masonry").Include(
                "~/Scripts/masonry.pkgd*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/frontend/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}