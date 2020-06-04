using System.Web;
using System.Web.Optimization;

namespace MaxsPetCare
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/js").Include(
                      "~/Plugins/jquery/jquery.min.js",
                      "~/Plugins/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Plugins/jquery.easing/jquery.easing.min.js",
                      "~/Plugins/waypoints/jquery.waypoints.min.js",
                      "~/Plugins/counterup/counterup.min.js",
                      "~/Plugins/owl.carousel/owl.carousel.min.js",
                      "~/Plugins/isotope-layout/isotope.pkgd.min.js",
                      "~/Plugins/venobox/venobox.min.js",
                      "~/Plugins/aos/aos.js",
                      "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Plugins/bootstrap/css/bootstrap.min.css",
                      "~/Plugins/icofont/icofont.min.css",
                      "~/Plugins/remixicon/remixicon.css",
                      "~/Plugins/boxicons/css/boxicons.min.css",
                      "~/Plugins/owl.carousel/assets/owl.carousel.min.css",
                      "~/Plugins/venobox/venobox.css",
                      "~/Plugins/aos/aos.css",
                      "~/Content/style.css",
                      "~/Content/custom.css"
                      ));
        }
    }
}
