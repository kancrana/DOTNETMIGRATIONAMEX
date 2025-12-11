using System.Web;
using System.Web.Optimization;

namespace eShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles( // TODO Script and style bundling works differently in ASP.NET Core. BundleCollection should be replaced by alternative bundling technologies. For more details see https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification.
        BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/carbondesign").Include("~/Content/Carbon/carbon-components.js", "~/Content/Carbon/carbon-components.min.js"));
            bundles.Add(new StyleBundle("~/content/carbondesign").Include("~/Content/Carbon/carbon-components.css", "~/Content/Carbon/carbon-custom.css"));
            bundles.Add(new ScriptBundle("~/bundles/customjquery").Include("~/Content/Carbon/app.js"));
        }
    }
}