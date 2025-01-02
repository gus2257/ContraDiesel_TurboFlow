using logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkShop.include
{
    public partial class master : System.Web.UI.MasterPage
    {
        private static BasePage Base = new BasePage();
        public DataTable dtOperacion;
        public DataTable dtConfiguracion;
        public DataTable dtReportes;
        public DataTable dtCatalogos;
        public DataTable dtGuard;
        public string ruta = string.Empty;
        public string host = string.Empty;
        public string login = "pages/login.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.host = master.Base.Host;
            this.ruta = master.Base.URL;
            if (!this.Page.IsPostBack)
            {
                if (HttpContext.Current.Session["User"] != null)
                    this.NombreUsuario.InnerText = HttpContext.Current.Session["User"].ToString() + " " + (HttpContext.Current.Session["Apellido"] != null ? HttpContext.Current.Session["Apellido"].ToString() : "");
                Dictionary<string, string> p_datos = new Dictionary<string, string>();
            }
              
        }

    }
}