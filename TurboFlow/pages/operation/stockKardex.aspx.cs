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
    public partial class stockKardex : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();
        public string StockID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 12;

            StockID = Request.QueryString["StockID"].ToString();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> LoadDropdowns(Dictionary<string, string> datos)
        {

            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("StockKardex_Dropdowns", datos);

            dictionary["StockActivity"] = (object)basePage.DataTableToMap(ds.Tables[0]);
            

            return dictionary;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> StockLoad(Dictionary<string, string> datos)
        {

            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("StockKardex_Get", datos);

            DataTable dt = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {

                dictionary["Category"] = dt.Rows[0]["Category"].ToString();
                dictionary["StockNum"] = dt.Rows[0]["StockNum"].ToString();
                dictionary["Brand"] = dt.Rows[0]["Brand"].ToString();
                dictionary["Model"] = dt.Rows[0]["ModelName"].ToString();
                dictionary["StockStatus"] = dt.Rows[0]["StockStatus"].ToString();
                dictionary["LastUpdate"] = dt.Rows[0]["LastUpdate"].ToString();

                dictionary["History"] = (object)basePage.DataTableToMap(ds.Tables[1]);
            }

            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string StockHistorySave(Dictionary<string, string> datos)
        {
            string result = "";
            try
            {
                BasePage val = new BasePage();
                logic_acces val2 = new logic_acces(BasePage.ConexionDB);
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                ArrayList arrayList = new ArrayList();
                TransactionScope val3 = new TransactionScope();
                try
                {
                    val2.ExecuteNonQuery("StockHistory_UI", datos);

                    val3.Complete();
                    result = "OK";
                }
                finally
                {
                    ((IDisposable)val3)?.Dispose();
                }


            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }


    }
}