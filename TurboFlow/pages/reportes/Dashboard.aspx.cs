using logic;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkShop.pages.reportes
{
    public partial class Dashboard : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();
        public string d6, d5, d4, d3, d2, d1, d0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 35;

            d0 = DateTime.Now.AddDays(0).ToString("MM/dd");
            d1 = DateTime.Now.AddDays(-1).ToString("MM/dd");
            d2 = DateTime.Now.AddDays(-2).ToString("MM/dd");
            d3 = DateTime.Now.AddDays(-3).ToString("MM/dd");
            d4 = DateTime.Now.AddDays(-4).ToString("MM/dd");
            d5 = DateTime.Now.AddDays(-5).ToString("MM/dd");
            d6 = DateTime.Now.AddDays(-6).ToString("MM/dd");

        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> ObtieneDatos(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = new DataSet();
            ds = logicAcces.ExecuteQuery("Dashboard_Get", datos);

            DataTable table1 = ds.Tables[0];
            DataTable table2 = ds.Tables[1];
            DataTable table3 = ds.Tables[2];

            dictionary["MecanicosRep"] = (object)basePage.DataTableToMap(table1);
            dictionary["UnidadesRep"] = (object)basePage.DataTableToMap(table2);
            dictionary["InspeccionesRep"] = (object)basePage.DataTableToMap(table3);

            //int timeoff = 0;
            //if (HttpContext.Current.Session["TimeOffset"] != null)
            //    timeoff = int.Parse(HttpContext.Current.Session["TimeOffset"].ToString());

            //dictionary["LastUpdate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "hrs.";


            dictionary["MecanicosQty"] = table1.Compute("Sum(ThisMonth)", string.Empty).ToString();
            dictionary["UnidadesQty"] = table2.Rows.Count.ToString();
            dictionary["InspeccionesQty"] = table3.Compute("Sum(Total)", string.Empty).ToString();


            return dictionary;
        }

    }
}