using System;
using logic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace WorkShop.pages
{
    public partial class Login : BasePageLogIn
    {
        public string ruta = string.Empty;
        public static string _Conexion;
        public static string ConexionDB
        {
            get
            {
                if (Login._Conexion == null)
                    Login._Conexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();
                return Login._Conexion;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.Theme = "";
            this.ruta = this.URL;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack || HttpContext.Current.Session["UserId"] == null)
                return;
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Session.RemoveAll();
                this.lblError.Text = "";
                BasePage basePage = new BasePage();
                logic_acces logicAcces = new logic_acces(Login.ConexionDB, false);
                Dictionary<string, string> p_datos = new Dictionary<string, string>();
                p_datos.Add("LoginName", this.txtUsuario.Value);
                p_datos.Add("Contrasenia", this.txtPassword.Value);
                DataTable table1 = logicAcces.ExecuteQuery("Usuario_Login", p_datos).Tables[0];
                if (table1.Rows.Count <= 0)
                    return;
                HttpContext.Current.Session[nameof(Login)] = (object)true;
                HttpContext.Current.Session["UserId"] = (object)table1.Rows[0]["usuarioId"].ToString();
                HttpContext.Current.Session["User"] = (object)table1.Rows[0]["nombre"].ToString();
                HttpContext.Current.Session["Apellido"] = (object)table1.Rows[0]["Apellido"].ToString();
                HttpContext.Current.Session["LoginName"] = (object)table1.Rows[0]["loginName"].ToString();
                HttpContext.Current.Session["TimeOffset"] = this.txtTimeOffset.Value;
                HttpContext.Current.Session["EsPermisoMantenimiento"] = (object)null;
                DataTable table2 = logicAcces.ExecuteQuery("UsuarioPermisoPantallaDefault_Sel", p_datos).Tables[0];
                if (table2.Rows.Count <= 0)
                    throw new Exception("Your user does not have a default page, please verify");

                this.Response.Redirect(table2.Rows[0]["ruta"].ToString().Replace("pages/", "").ToString());
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
            }
        }
    }
}