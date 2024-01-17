using System.Web;
using System.Web.Optimization;

namespace Ext_FraudingSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();




            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/kendo").Include(
                "~/Content/kendo/2016.1.112/kendo.common.min.css",
                "~/Content/kendo/2016.1.112/kendo.mobile.all.min.css",
                "~/Content/kendo/2016.1.112/kendo.dataviz.min.css",
                "~/Content/kendo/2016.1.112/kendo.default.min.css",
                "~/Content/kendo/2016.1.112/kendo.dataviz.default.min.css",
                "~/Content/kendo/2016.1.112/kendo.rtl.min.css",
                "~/Content/bootstrap-switch/bootstrap2/bootstrap-switch.min.css",
                "~/Content/toastr.min.css"));


            bundles.Add(new ScriptBundle("~/bundle/kendoscripts").Include(
                "~/Scripts/kendo/2016.1.112/jquery.min.js",
                "~/Scripts/kendo/2016.1.112/jszip.min.js",
                "~/Scripts/kendo/2016.1.112/kendo.all.min.js",
                "~/Scripts/kendo/2016.1.112/kendo.aspnetmvc.min.js",
                "~/Scripts/bootstrap-switch.min.js",
                "~/Scripts/jquery.signalR-2.2.1.min.js",
                "~/Scripts/toastr.js"

                ));

            bundles.Add(new StyleBundle("~/bundle/Styles").Include(
                "~/Content/bootstrap/css/bootstrap.min.css",
                "~/Content/Site.css",
                "~/Content/dist/css/skins/skin-blue.min.css",
                "~/Content/dist/css/animate.css",
                "~/Content/dist/css/style.css",
                "~/Content/plugins/font-awesome/css/font-awesome.min.css",
                "~/Content/dist/css/font-awesome-animation.css",
                "~/Content/dist/css/hover-min.css",
                "~/Content/plugins/bootstrap-tour/build/css/bootstrap-tour.css"
                ));
        }
    }
}
