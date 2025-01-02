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
    public partial class Usuario : BasePage
    {
        public string ruta = string.Empty;
        private static BasePage Base = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = this.URL;
            this.PermisoID = 9;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> ObtieneUsuarios(
          Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table1 = logicAcces.ExecuteQuery("Usuario_Sel", datos).Tables[0];
            DataTable table3 = logicAcces.ExecuteQuery("UsuarioPermiso_Sel", datos).Tables[0];

            dictionary["Usuarios"] = (object)basePage.DataTableToMap(table1);
            dictionary["Permisos"] = (object)basePage.DataTableToMap(table3);

            return dictionary;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string GuardarUsuarios(Dictionary<string, string> datos)
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
                    val2.ExecuteNonQuery("Usuario_UI", datos);
                    val2.ExecuteNonQuery("UsuarioPermiso_DEL", datos);
                    dynamic val4 = JsonConvert.DeserializeObject<object>(datos["listaPermisos"]);
                    for (int i = 0; i < val4.Count; i++)
                    {
                        if (Convert.ToString(val4[i].PadreId.Value) != "0")
                        {
                            datos["PermisoID"] = Convert.ToString(val4[i].PermisoID.Value);
                            datos["Autorizar"] = ((val4[i].Autorizar == null) ? "" : Convert.ToString(val4[i].Autorizar.Value));
                            datos["SoloLectura"] = ((val4[i].SoloLectura == null) ? "" : Convert.ToString(val4[i].SoloLectura.Value));
                            datos["Editar"] = ((val4[i].Editar == null) ? "" : Convert.ToString(val4[i].Editar.Value));
                            datos["EsPredeterminado"] = ((val4[i].EsPredeterminado == null) ? "" : Convert.ToString(val4[i].EsPredeterminado.Value));
                            val2.ExecuteNonQuery("UsuarioPermiso_UI", datos);
                        }
                    }

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

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string EliminarUsuarios(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            string str = "";
            using (TransactionScope transactionScope = new TransactionScope())
            {
                datos.Add("NombreCampo", "UsuarioID");
                datos.Add("IDEliminar", datos["UsuarioID"]);
                datos.Add("ExcluirTablas", "UsuarioPermiso,UsuarioUbicacion");
                datos.Add("EsEnUso", "false");
                logicAcces.ExecuteNonQuery("ValidarLlavesForaneas_Get", datos);
                if (!bool.Parse(datos["EsEnUso"].ToString()))
                {
                    logicAcces.ExecuteNonQuery("UsuarioPermiso_Del", datos);
                    logicAcces.ExecuteNonQuery("UsuarioUbicacion_Del", datos);
                    logicAcces.ExecuteNonQuery("Usuario_Del", datos);
                    transactionScope.Complete();
                    str = "OK";
                }
                else
                    str = "You cannot delete this user,because is in use. Please verify.";
            }
            return str;
        }
    }
}