using logic;
using System;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkShop.pages.catalogos
{
    public partial class Actividad : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 26;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> ObtieneDatos(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //DataTable table1 = logicAcces.ExecuteQuery("Marca_Cmb", datos).Tables[0];
            //DataTable table2 = logicAcces.ExecuteQuery("Modelo_Cmb", datos).Tables[0];
            //dictionary["Marcas"] = (object)basePage.DataTableToMap(table1);
            //dictionary["Modelos"] = (object)basePage.DataTableToMap(table2);
            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> ObtieneActividades(
          Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = logicAcces.ExecuteQuery("Actividad_Sel", datos).Tables[0];
            dictionary["Actividades"] = (object)basePage.DataTableToMap(table);
            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> ObtieneActividadDetalle(
          Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
            DataTable table1 = logicAcces.ExecuteQuery("Actividad_Sel", datos).Tables[0];
            dictionary1[nameof(Actividad)] = (object)basePage.DataTableToMap(table1);
            /*
            DataTable table2 = logicAcces.ExecuteQuery("ActividadMarca_Sel", datos).Tables[0];
            DataTable table3 = logicAcces.ExecuteQuery("ActividadMarcaModelo_Sel", datos).Tables[0];
            DataTable table4 = logicAcces.ExecuteQuery("ActividadMarcaRepuesto_Sel", datos).Tables[0];
            
            List<Dictionary<string, object>> map1 = basePage.DataTableToMap(table2);
            datos.Add("ActividadMarcaID", "");
            foreach (Dictionary<string, object> dictionary2 in map1)
            {
                datos["ActividadMarcaID"] = dictionary2["ActividadMarcaID"].ToString();
                DataSet dataSet = logicAcces.ExecuteQuery("ActividadMarcaMarcas_Sel", datos);
                dictionary2.Add("MarcaSelectedID", (object)basePage.DataTableToMap(dataSet.Tables[0]));
                dictionary2.Add("Descripcion", dataSet.Tables[1].Rows[0]["Descripcion"]);
            }
            dictionary1["ActividadMarcas"] = (object)map1;
            List<Dictionary<string, object>> map2 = basePage.DataTableToMap(table4);
            List<Dictionary<string, object>> map3 = basePage.DataTableToMap(table3);
            DataTable p_dt1 = new DataTable();
            p_dt1.Columns.Add("RepuestoID", typeof(int));
            p_dt1.Columns.Add("Codigo", typeof(string));
            foreach (Dictionary<string, object> dictionary3 in map2)
            {
                p_dt1.Rows.Add(dictionary3["RepuestoID"], dictionary3["Codigo"]);
                dictionary3.Add("RepuestoSel", (object)basePage.DataTableToDiccionary(p_dt1));
                p_dt1.Clear();
            }
            DataTable p_dt2 = new DataTable();
            DataSet dataSet1 = new DataSet();
            p_dt2.Columns.Add("StartDate", typeof(string));
            p_dt2.Columns.Add("EndDate", typeof(string));
            datos.Add("AnioInicio", "");
            datos.Add("AnioFin", "");
            foreach (Dictionary<string, object> dictionary4 in map3)
            {
                p_dt2.Rows.Add(dictionary4["AnioInicio"], dictionary4["AnioFin"]);
                dictionary4.Add("AnioModelos", (object)basePage.DataTableToDiccionary(p_dt2));
                p_dt2.Clear();
                datos["ActividadMarcaID"] = dictionary4["ActividadMarcaID"].ToString();
                datos["AnioInicio"] = dictionary4["AnioInicio"].ToString();
                datos["AnioFin"] = dictionary4["AnioFin"].ToString();
                DataSet dataSet2 = logicAcces.ExecuteQuery("ModeloPorAnios_Sel", datos);
                dictionary4.Add("ModSelectedID", (object)basePage.DataTableToMap(dataSet2.Tables[0]));
                dictionary4.Add("Descripcion", dataSet2.Tables[1].Rows[0]["Descripcion"]);
            }
            dictionary1["MarcaRepuesto"] = (object)map2;
            dictionary1["MarcaModelos"] = (object)map3;
            */
            return dictionary1;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> BuscarRepuestos(
          Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = logicAcces.ExecuteQuery("Repuesto_Cmb", datos).Tables[0];
            dictionary["Repuestos"] = (object)basePage.DataTableToMap(table);
            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string GuardarActividad(Dictionary<string, string> datos)
        {
            string str;
            try
            {
                BasePage basePage = new BasePage();
                logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //List<Dictionary<string, string>> dictionaryList1 = new List<Dictionary<string, string>>();
                    //List<Dictionary<string, string>> dictionaryList2 = new List<Dictionary<string, string>>();
                    //List<Dictionary<string, string>> dictionaryList3 = new List<Dictionary<string, string>>();
                    //List<Dictionary<string, string>> registrosActuales1 = basePage.Deserialize(datos["ActividadMarcas"]);
                    //List<Dictionary<string, string>> registrosActuales2 = basePage.Deserialize(datos["MarcaRepuesto"]);
                    //List<Dictionary<string, string>> registrosActuales3 = basePage.Deserialize(datos["MarcaModelos"]);
                    logicAcces.ExecuteNonQuery("Actividad_UI", datos);

                    //datos.Add("actividadmarcasXML", BasePage.ObtieneXML(registrosActuales1));
                    //datos.Add("marcarepuestoXML", BasePage.ObtieneXML(registrosActuales2));
                    //datos.Add("marcamodelosXML", BasePage.ObtieneXML(registrosActuales3));
                    //datos.Remove("ActividadMarcas");
                    //datos.Remove("MarcaRepuesto");
                    //datos.Remove("MarcaModelos");
                    //logicAcces.ExecuteNonQuery("ActividadMarcaModeloRepuesto_UI", datos);
                    transactionScope.Complete();
                }
                str = "OK";
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string EliminarActividad(Dictionary<string, string> datos)
        {
            string str = "";
            try
            {
                BasePage basePage = new BasePage();
                logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    datos.Add("NombreCampo", "ActividadID");
                    datos.Add("IDEliminar", datos["ActividadID"]);
                    datos.Add("ExcluirTablas", "ActividadMarcaModelo,ActividadMarcaRepuesto,ActividadMarca");
                    datos.Add("EsEnUso", "false");
                    logicAcces.ExecuteNonQuery("ValidarLlavesForaneas_Get", datos);
                    if (!bool.Parse(datos["EsEnUso"].ToString()))
                    {
                        logicAcces.ExecuteNonQuery("Actividad_Del", datos);
                        str = "OK";
                    }
                    else
                        str = "You cannot delete this item, because is in use. Please verify.";
                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string EliminarActividadMarca(Dictionary<string, string> datos)
        {
            string str;
            try
            {
                BasePage basePage = new BasePage();
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    new logic_acces(BasePage.ConexionDB).ExecuteNonQuery("ActividadMarca_Del", datos);
                    transactionScope.Complete();
                }
                str = "OK";
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string EliminarMarcaModelo(Dictionary<string, string> datos)
        {
            string str;
            try
            {
                BasePage basePage = new BasePage();
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    new logic_acces(BasePage.ConexionDB).ExecuteNonQuery("ActividadMarcaModelo_Del", datos);
                    transactionScope.Complete();
                }
                str = "OK";
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string EliminarMarcaRepuesto(Dictionary<string, string> datos)
        {
            string str;
            try
            {
                BasePage basePage = new BasePage();
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    new logic_acces(BasePage.ConexionDB).ExecuteNonQuery("ActividadMarcaRepuesto_Del", datos);
                    transactionScope.Complete();
                }
                str = "OK";
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }
    }
}