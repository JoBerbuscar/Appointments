using System.Web;
using System.Web.Optimization;

namespace GAP.Appointments.Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterCssBundles(bundles);
            RegisterJsBundles(bundles);
        }
        /// <summary>
        /// Permite registrar los bundles CSS.
        /// </summary>
        /// <param name="bundles">Objeto en el que se almacena la lista de bundles de la
        /// aplicación.</param>
        private static void RegisterCssBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/lib/css/bootstrap/bootstrap.css",
                     "~/Content/lib/css/ace/ace.css",
                     "~/Content/lib/css/ace/ace-fonts.css",
                     "~/Content/lib/css/ace/font-awesome.css",
                     "~/Content/css/site.css"));
        }
        /// <summary>
        /// Permite registrar los bundles JavaScript.
        /// </summary>
        /// <param name="bundles">Objeto en el que se almacena la lista de bundles de la
        /// aplicación.</param>
        private static void RegisterJsBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/site-bundle.js")
                .Include(
                    // Ace
                    "~/Scripts/ace/ace.js",
                    "~/Scripts/ace/ace.ajax-content.js",
                    "~/Scripts/ace/ace.touch-drag.js",
                    "~/Scripts/ace/ace.sidebar.js",
                    "~/Scripts/ace/ace.sidebar-scroll-1.js",
                    "~/Scripts/ace/ace.submenu-hover.js",
                    "~/Scripts/ace/ace.widget-box.js",
                    "~/Scripts/ace/ace.settings.js",
                    "~/Scripts/ace/ace.settings-rtl.js",
                    "~/Scripts/ace/ace.settings-skin.js",
                    "~/Scripts/ace/ace.widget-on-reload.js",
                    "~/Scripts/ace/ace.searchbox-autocomplete.js",
                     "~/Content/js/Common/layout.js"
                ));

            //Capturas citas
            bundles.Add(new ScriptBundle("~/Content/js/Appointments/registro-citas")
                .Include(
                    "~/Content/js/Appointments/Pacient-info.js",
                    "~/Content/js/Appointments/appointment.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                          "~/Scripts/dataTable/jquery.dataTables.min.js",
                          "~/Scripts/dataTable/moment.min.js",
                          "~/Scripts/dataTable/datetime-moment.js",
                           "~/Scripts/dataTable/json2.js",
                          "~/Scripts/dataTable/dataTables.bootstrap4.min.js"));

        }
    }
}
