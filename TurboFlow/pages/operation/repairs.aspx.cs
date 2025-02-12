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
using System.Data.SqlClient;

namespace WorkShop.pages.operation
{
    public partial class repairs : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 14;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> InitLoad(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("Stock_Filters", datos);

            dictionary["Category"] = (object)basePage.DataTableToMap(ds.Tables[0]);
            dictionary["Brand"] = (object)basePage.DataTableToMap(ds.Tables[1]);
            dictionary["Model"] = (object)basePage.DataTableToMap(ds.Tables[2]);
            dictionary["StockStatus"] = (object)basePage.DataTableToMap(ds.Tables[3]);

            return dictionary;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> StockLoad(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("Stock_Inventory", datos);



            dictionary["StockGrouped"] = (object)basePage.DataTableToMap(ds.Tables[0]);
            dictionary["StockList"] = (object)basePage.DataTableToMap(ds.Tables[1]);

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
            catch (SqlException ex)
            {
                result["Result"] = "ERROR";
                if (ex.Number == 2601)
                    result["Message"] = "Stock ID already exists";
                else
                    result["Message"] = ex.Message;
            }
            catch (Exception ex)
            {
                result["Result"] = "ERROR";
                result["Message"] = ex.Message;
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> LoadDrops(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("Stock_DropDowns", datos);

            dictionary["Category"] = (object)basePage.DataTableToMap(ds.Tables[0]);
            dictionary["Brand"] = (object)basePage.DataTableToMap(ds.Tables[1]);
            dictionary["Model"] = (object)basePage.DataTableToMap(ds.Tables[2]);
           

            return dictionary;
        }

    }
}