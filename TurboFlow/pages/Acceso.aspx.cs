using logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkShop.pages
{
    public partial class Acceso : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> ObtieneDatosAcceso(
  Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = logicAcces.ExecuteQuery("PermisoPantallasModulo_Sel2", datos).Tables[0];
            dictionary["Pantallas"] = (object)basePage.DataTableToMap(table);
            return dictionary;
        }
    }
}