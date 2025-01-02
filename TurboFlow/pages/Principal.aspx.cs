using logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkShop.pages
{
    public partial class Principal : BasePage
    {
        public string ruta = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "WorkShop";
            this.ruta = this.URL;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static List<Dictionary<string, object>> ObtieneGeografia(
          Dictionary<string, string> datos)
        {
            List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            dictionaryList.Add(dictionary);
            return dictionaryList;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> ObtieneDatosMaster(
          Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            HttpContext.Current.Session["UbicacionId"] = (object)datos["UbicacionID"].ToString();
            HttpContext.Current.Session["NombreUbicacion"] = (object)datos["Ubicacion"].ToString();
            dictionary["Data"] = (object)true;
            dictionary["TipoUsuarioId"] = (object)HttpContext.Current.Session["TipoUsuarioId"].ToString();
            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> Salir(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            HttpContext.Current.Session.Abandon();
            dictionary["Data"] = (object)true;
            return dictionary;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static Dictionary<string, object> Date(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
            return new Dictionary<string, object>()
            {
                ["Data"] = (object)true,
                [nameof(Date)] = (object)DateTime.Now.ToString("MMddyyyyHHmm")
            };
        }
    }
}