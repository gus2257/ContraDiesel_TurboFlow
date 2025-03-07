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

namespace WorkShop.pages.catalogos
{
    public partial class activity : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 10;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> InitLoad(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("RepairActivity_Get", datos);

            dictionary["Activities"] = (object)basePage.DataTableToMap(ds.Tables[0]);
         

            return dictionary;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, string> Save(Dictionary<string, string> datos)
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
                    val2.ExecuteNonQuery("RepairActivity_UI", datos);

                    val3.Complete();

                    result["Result"] = "OK";
                   
                }
                finally
                {
                    ((IDisposable)val3)?.Dispose();
                }


            }
            //catch (SqlException ex)
            //{
            //    result["Result"] = "ERROR";
            //    if (ex.Number == 2601)
            //        result["Message"] = "Stock ID already exists";
            //    else
            //        result["Message"] = ex.Message;
            //}
            catch (Exception ex)
            {
                result["Result"] = "ERROR";
                result["Message"] = ex.Message;
            }

            return result;
        }

    }
}