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
            //dictionary["RepairStatus"] = (object)basePage.DataTableToMap(ds.Tables[4]);

            Dictionary<string, string> techs = new Dictionary<string, string>();
            techs["ProfileID"] = "3";
            DataSet ds2 = logicAcces.ExecuteQuery("UserProfiles_Get", techs);
            dictionary["Technicians"] = (object)basePage.DataTableToMap(ds2.Tables[0]);

            DataSet ds3 = logicAcces.ExecuteQuery("RepairActivity_Get", techs);
            dictionary["Activities"] = (object)basePage.DataTableToMap(ds3.Tables[0]);


            return dictionary;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> RepairLoad(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataSet ds = logicAcces.ExecuteQuery("Repair_Get", datos);



            dictionary["Repairs"] = (object)basePage.DataTableToMap(ds.Tables[0]);

            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> StockSearch(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            DataSet ds = logicAcces.ExecuteQuery("Stock_Get", datos);

            if (ds.Tables[0].Rows.Count == 1)
            {
                dictionary["Result"] = "OK";
                dictionary["StockID"] = ds.Tables[0].Rows[0]["StockID"].ToString();
                dictionary["Category"] = ds.Tables[0].Rows[0]["Category"].ToString();
                dictionary["Brand"] = ds.Tables[0].Rows[0]["Brand"].ToString();
                dictionary["Model"] = ds.Tables[0].Rows[0]["Model"].ToString();
            }
            else
            {
                dictionary["Result"] = "ERROR";
                dictionary["Message"] = "Stock ID not found";
            }

            return dictionary;
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



        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, string> RepairSave(Dictionary<string, string> datos)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (datos.ContainsKey("RepairStatusID2"))
            {
                if (datos["RepairStatusID2"] != datos["RepairStatusID"])
                    datos["RepairStatusID"] = datos["RepairStatusID2"];
            }
            try
            {
                BasePage val = new BasePage();
                logic_acces val2 = new logic_acces(BasePage.ConexionDB);

                ArrayList arrayList = new ArrayList();
                TransactionScope val3 = new TransactionScope();
                try
                {
                    val2.ExecuteNonQuery("Repair_UI", datos);

                    val3.Complete();

                    result["Result"] = "OK";
                    result["RepairID"] = datos["RepairID"];


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
        public static Dictionary<string, string> StockCreate(Dictionary<string, string> datos)
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
                    val2.ExecuteNonQuery("Repair_UI", datos);

                    val3.Complete();

                    result["Result"] = "OK";
                    result["RepairID"] = datos["RepairID"];


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
    }
}