using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using logic;

namespace TurboFlow.pages.menus
{
    public partial class operations : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 23;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> LoadAccess(
        Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            //DataTable table1 = logicAcces.ExecuteQuery("Usuario_Sel", datos).Tables[0];
            //DataTable table3 = logicAcces.ExecuteQuery("UsuarioPermiso_Sel", datos).Tables[0];

            //dictionary["Usuarios"] = (object)basePage.DataTableToMap(table1);
            //dictionary["Permisos"] = (object)basePage.DataTableToMap(table3);

            return dictionary;
        }

    }
}