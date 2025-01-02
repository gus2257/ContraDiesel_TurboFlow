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
    public partial class masterGuard3 : System.Web.UI.MasterPage
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
            this.host = masterGuard3.Base.Host;
            this.ruta = masterGuard3.Base.URL;
            if (!this.Page.IsPostBack)
            {
                if (HttpContext.Current.Session["User"] != null)
                    this.NombreUsuario.InnerText = HttpContext.Current.Session["User"].ToString() + " " + (HttpContext.Current.Session["Apellido"] != null ? HttpContext.Current.Session["Apellido"].ToString() : "");
                Dictionary<string, string> p_datos = new Dictionary<string, string>();
                logic_acces logicAcces = new logic_acces(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
                p_datos["UsuarioID"] = HttpContext.Current.Session["UserId"].ToString();
                DataTable table = logicAcces.ExecuteQuery("UsuarioUbicacion_Cmb", p_datos).Tables[0];
                this.ddlUbicacion.DataTextField = "NombreUbicacion";
                this.ddlUbicacion.DataValueField = "UbicacionID";
                this.ddlUbicacion.DataSource = (object)table;
                this.ddlUbicacion.DataBind();
                this.ddlUbicacion.SelectedValue = HttpContext.Current.Session["UbicacionId"].ToString();

                p_datos["PadreId"] = "1";
                this.dtConfiguracion = logicAcces.ExecuteQuery("PermisoPantallasMenu_Sel", p_datos).Tables[0];

                p_datos["PadreId"] = "22";
                this.dtCatalogos = logicAcces.ExecuteQuery("PermisoPantallasMenu_Sel", p_datos).Tables[0];
                p_datos["PadreId"] = "17";
                this.dtReportes = logicAcces.ExecuteQuery("PermisoPantallasMenu_Sel", p_datos).Tables[0];
                p_datos["PadreId"] = "10";
                this.dtOperacion = logicAcces.ExecuteQuery("PermisoPantallasMenu_Sel", p_datos).Tables[0];
                p_datos["PadreId"] = "29";
                this.dtGuard = logicAcces.ExecuteQuery("PermisoPantallasMenu_Sel", p_datos).Tables[0];
            }
            if (HttpContext.Current.Session["TipoUsuarioId"] == null || !(HttpContext.Current.Session["TipoUsuarioId"].ToString() == "4"))
                return;
            this.login = "pages/loginMechanic.aspx";
        }

        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Session["UbicacionId"] = (object)this.ddlUbicacion.Items[this.ddlUbicacion.SelectedIndex].Value;
            HttpContext.Current.Session["NombreUbicacion"] = (object)this.ddlUbicacion.Items[this.ddlUbicacion.SelectedIndex].Text;
        }
    }
}