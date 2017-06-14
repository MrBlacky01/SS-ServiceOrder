using System.Web.Optimization;

namespace ServiceOrder.WebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/autosize.min.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo.all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/LogIn.css"));

            bundles.Add(new LessBundle("~/Content/less").Include("~/Content/*.less"));

            bundles.Add(new StyleBundle("~/Content/404css").Include(
                      "~/Content/404Style.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/kendoCommonCss").Include(
                "~/styles/kendo.common.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/kendoDefaultCss").Include(
                "~/styles/kendo.default.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/jQuery-File-Upload").Include(
                    "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
                   "~/Content/jQuery.FileUpload/css/jquery.fileupload-ui.css",
                   "~/Content/blueimp-gallery2/css/blueimp-gallery.css",
                     "~/Content/blueimp-gallery2/css/blueimp-gallery-video.css",
                       "~/Content/blueimp-gallery2/css/blueimp-gallery-indicator.css"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/jQuery-File-Upload").Include(
                    //<!-- The Templates plugin is included to render the upload/download listings -->
                    "~/Scripts/jQuery.FileUpload/vendor/jquery.ui.widget.js",
                    "~/Scripts/jQuery.FileUpload/tmpl.min.js",
                //<!-- The Load Image plugin is included for the preview images and image resizing functionality -->
                "~/Scripts/jQuery.FileUpload/load-image.all.min.js",
                //<!-- The Canvas to Blob plugin is included for image resizing functionality -->
                "~/Scripts/jQuery.FileUpload/canvas-to-blob.min.js",
                //"~/Scripts/file-upload/jquery.blueimp-gallery.min.js",
                //<!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
                "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                //<!-- The basic File Upload plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                //<!-- The File Upload processing plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-process.js",
                //<!-- The File Upload image preview & resize plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-image.js",
                //<!-- The File Upload audio preview plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-audio.js",
                //<!-- The File Upload video preview plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-video.js",
                //<!-- The File Upload validation plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-validate.js",
                //!-- The File Upload user interface plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js",
                //Blueimp Gallery 2 
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery.js",
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery-video.js",
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery-indicator.js",
                "~/Scripts/blueimp-gallery2/js/jquery.blueimp-gallery.js"));

            bundles.Add(new ScriptBundle("~/bundles/Blueimp-Gallerry2").Include(//Blueimp Gallery 2 
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery.js",
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery-video.js",
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery-indicator.js",
                "~/Scripts/blueimp-gallery2/js/jquery.blueimp-gallery.js"));

        }
    }
}
