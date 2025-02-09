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

namespace WorkShop.pages.catalogos
{
    public partial class brandmodel : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 13;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> BrandLoad(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("StockBrand_Get", datos);

            dictionary["Brands"] = (object)basePage.DataTableToMap(ds.Tables[0]);

            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> ModelLoad(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("StockModel_Get", datos);

            dictionary["Models"] = (object)basePage.DataTableToMap(ds.Tables[0]);

            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> LoadInit(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("StockCategory_Sel", datos);
            dictionary["Categories"] = (object)basePage.DataTableToMap(ds.Tables[0]);

            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, string> StockSave(Dictionary<string, string> datos)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            
            try
            {
                BasePage val = new BasePage();
                logic_acces val2 = new logic_acces(BasePage.ConexionDB);
                
                ArrayList arrayList = new ArrayList();
                TransactionScope val3 = new TransactionScope();
                try
                {
                    val2.ExecuteNonQuery("Stock_UI", datos);

                    val3.Complete();

                    result["Result"] = "OK";
                    result["StockID"] = datos["StockID"];


                }
                finally
                {
                    ((IDisposable)val3)?.Dispose();
                }


            }
            catch (Exception ex)
            {
                result["Result"] = "ERROR";
                result["Message"] = ex.Message;
            }

            return result;
        }


    }
}