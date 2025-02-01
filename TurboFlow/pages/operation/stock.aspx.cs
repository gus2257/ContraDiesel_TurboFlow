using logic;
using logic.Class;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace WorkShop.pages.operation
{
    public partial class stock : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 12;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> InitLoad(
          Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("Stock_Inventory", datos);



            dictionary["StockGrouped"] = (object)basePage.DataTableToMap(ds.Tables[0]);
            dictionary["StockList"] = (object)basePage.DataTableToMap(ds.Tables[1]);

            return dictionary;
        }

    }
}