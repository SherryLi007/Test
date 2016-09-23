using System.Web;
using System.Web.Optimization;

namespace Inventec
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Themes/Common/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Themes/Common/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Themes/Common/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Themes/Common/Scripts/bootstrap.js",
                      "~/Themes/Common/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Themes/Common/Scripts/bootstrap-datepicker/js/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                        "~/Themes/Common/Scripts/bootstrap-datepicker/css/datepicker3.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                      "~/Themes/Common/Css/bootstrap.css",
                      "~/Themes/Css/login.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Themes/Common/Css/bootstrap.css",
                      "~/Themes/Css/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                      "~/Themes/Common/Scripts/ajaxsubmit.js",
                      "~/Themes/Common/Scripts/noty/noty.js"));
        }
    }
}
