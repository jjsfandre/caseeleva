using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CaseEleva
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyles(bundles);
            RegisterScripts(bundles);
        }

        public static void RegisterStyles(BundleCollection bundles)
        {
            CssRewriteUrlTransform cssRewriter = new CssRewriteUrlTransform();

            bundles.Add(new StyleBundle("~/Bundle/Styles/Resources")
                   //Resources
                   .Include("~/Content/Resources/bootstrap.min.css", cssRewriter)
                   .Include("~/Content/Resources/animate.css", cssRewriter)
                   .Include("~/Content/Resources/inspinia.css", cssRewriter)
                   //Plugins
                   .Include("~/Content/Resources/ui.jqgrid.css", cssRewriter)
                   .Include("~/Content/Resources/ui.jqgrid-bootstrap-ui.css", cssRewriter)
                   .Include("~/Content/Resources/ui.jqgrid-bootstrap.css", cssRewriter)
                   .Include("~/Content/Resources/chosen/chosen.css", cssRewriter)
                   .Include("~/Content/Resources/sweetalert2.min.css", cssRewriter)
                   .Include("~/Content/Resources/jquery-ui.css", cssRewriter)
                   .Include("~/Content/Resources/bootstrap-tour.min.css", cssRewriter)
                   .Include("~/Content/Resources/c3.min.css", cssRewriter)
                   .Include("~/Content/Resources/datepicker3.css", cssRewriter)
                   .Include("~/Content/Resources/bootstrap-iconpicker.min.css", cssRewriter)
                   //Fonts
                   .Include("~/Content/Resources/font-awesome.min.css", cssRewriter)
                   .Include("~/Content/Resources/pe-icon-set-food.min.css", cssRewriter)
                   .Include("~/Content/Resources/pe-icon-set-social-people.min.css", cssRewriter)
                   .Include("~/Content/Resources/pe-icon-set-media.min.css", cssRewriter)
                   .Include("~/Content/Resources/pe-icon-7-stroke.min.css", cssRewriter)
            );

            bundles.Add(new StyleBundle("~/Bundle/Styles/CaseEleva").IncludeDirectory("~/Content", "*.css"));
        }

        private static StyleBundle IncludeDirectoryWithCssRewriter(StyleBundle bundles, string virtualPath, string pattern, bool includeSubDir = false)
        {
            var vpath = virtualPath.TrimEnd('/');
            var ppath = HttpContext.Current.Server.MapPath(vpath);
            var files = Directory.GetFiles(ppath, pattern, includeSubDir ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                var vfp = virtualPath + "/" + file.Substring(ppath.Length + 1).Replace("\\", "/");
                bundles.Include(vfp, new CssRewriteUrlTransform());
            }
            return bundles;
        }

        public static void RegisterScripts(BundleCollection bundles)
        {
            //

            bundles.Add(new ScriptBundle("~/Bundle/Scripts/Resources").Include(
                      //Resources
                      "~/Scripts/Resources/jquery-2.1.1.min.js",
                      "~/Scripts/Resources/jquery.validate.min.js",
                      "~/Scripts/Resources/jquery.mask.js",
                      "~/Scripts/Resources/jquery.slimscroll.min.js",
                      "~/Scripts/Resources/jquery.nestable.js",
                      "~/Scripts/Resources/dateFormat.js",
                      "~/Scripts/Resources/bootstrap.min.js",
                      "~/Scripts/Resources/inspinia.js",
                      "~/Scripts/Resources/metisMenu.min.js",
                      "~/Scripts/Resources/grid.locale-pt-br.js",
                      "~/Scripts/Resources/jquery.jqGrid.min.js",
                      "~/Scripts/Resources/sweetalert2.min.js",
                      "~/Scripts/Resources/chosen.jquery.js",
                      "~/Scripts/Resources/bootstrap-tour.min.js",
                      "~/Scripts/Resources/jquery.maskedinput.js",
                      "~/Scripts/Resources/c3.min.js",
                      "~/Scripts/Resources/cropper.min.js",
                      "~/Scripts/Resources/d3.min.js",
                      "~/Scripts/Resources/bootstrap-datepicker.js",
                      "~/Scripts/Resources/moment-with-locales.js",
                      "~/Scripts/Resources/bootstrap-iconpicker.min.js"
             ));

            //CaseEleva 
            bundles.Add(new ScriptBundle("~/Bundle/Scripts/CaseEleva").IncludeDirectory("~/Scripts", "*.js"));
            bundles.Add(new ScriptBundle("~/Bundle/Scripts/CaseEleva/Helpers").IncludeDirectory("~/Scripts/Helpers", "*.js"));
        }
    }
}