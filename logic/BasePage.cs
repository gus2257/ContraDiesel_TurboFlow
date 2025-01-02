using logic.Common.Resources;
using logic.SeguridadPortalRH;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Serialization;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Drawing.Charts;


namespace logic
{
    public class BasePage : Page
    {
        public static string _Conexion;
        private const string SessionTime = "SessionTime";
        private const string SessionState = "SessionState";
        private const string UidPage = "UidPage";
        public const string REMOTE_HOST = "REMOTE_HOST";
        private const string sessionCurrentModuloID = "sessionCurrentModuloID";
        private const string sessionDTpermisos = "DTpermisos";
        private const string sessionMenuPermisosItems = "sessionMenuPermisosItems";
        private const string sessionPermisosPantalla = "PermisosPantalla";
        private KeyValuePlainTextResource resourceMgr;
        private KeyValuePlainTextResource commonResourceMgr;
        private int permisoid;

        public static string ConexionDB
        {
            get
            {
                if (BasePage._Conexion == null)
                    BasePage._Conexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();
                return BasePage._Conexion;
            }
        }

        public System.Data.DataTable PermisosPantalla
        {
            get => this.GetSession(nameof(PermisosPantalla)) != null ? this.GetSession(nameof(PermisosPantalla)) as System.Data.DataTable : new System.Data.DataTable();
            set => this.SetSession(nameof(PermisosPantalla), (object)value, SessionStateModes.AllPages);
        }

        public string Token
        {
            get => this.GetSession("token") != null ? this.GetSession("token").ToString() : string.Empty;
            set => this.SetSession("token", (object)value, SessionStateModes.AllPages);
        }

        public int CurrentModuloID
        {
            get => HttpContext.Current.Session["sessionCurrentModuloID"] != null ? Convert.ToInt32(HttpContext.Current.Session["sessionCurrentModuloID"]) : 0;
            internal set => HttpContext.Current.Session["sessionCurrentModuloID"] = (object)value;
        }

        public string SqlLanguage => this.Session["SESSION_CULTURE"] is CultureInfo cultureInfo ? cultureInfo.TwoLetterISOLanguageName : "es";

        public string NombrePcMod
        {
            get
            {
                if (!(this.Session[nameof(NombrePcMod)] is string nombrePc))
                {
                    nombrePc = this.GetNombrePC();
                    this.Session[nameof(NombrePcMod)] = (object)nombrePc;
                }
                return nombrePc;
            }
        }

        protected string TypeName
        {
            get
            {
                string name = this.GetType().Name;
                return !name.StartsWith("pages_") ? string.Format("pages_{0}_aspx", (object)name) : name;
            }
        }

        public KeyValuePlainTextResource ResourceManager
        {
            get
            {
                if (this.resourceMgr == null)
                {
                    if (!(this.Session["SESSION_CULTURE"] is CultureInfo cultureInfo))
                        cultureInfo = CultureManager.ResolveCulture();
                    this.resourceMgr = ResourceFactory.CreateResource(this.Context.Handler.GetType().Name, !this.IsPostBack, cultureInfo);
                }
                return this.resourceMgr;
            }
        }

        public KeyValuePlainTextResource CommonResourceManager
        {
            get
            {
                if (this.commonResourceMgr == null)
                {
                    if (!(this.Session["SESSION_CULTURE"] is CultureInfo cultureInfo))
                        cultureInfo = CultureManager.ResolveCulture();
                    this.commonResourceMgr = ResourceFactory.CreateResource("GlobalResources", !this.IsPostBack, cultureInfo);
                }
                return this.commonResourceMgr;
            }
        }

        public string UIDPage
        {
            get
            {
                string uidPage = this.ViewState["UidPage"] as string;
                if (string.IsNullOrWhiteSpace(uidPage))
                {
                    uidPage = Guid.NewGuid().ToString();
                    this.ViewState.Add("UidPage", (object)uidPage);
                }
                return uidPage;
            }
        }

        public string URL => this.GetAppSetting(nameof(URL));

        public string Host => this.GetAppSetting(nameof(Host));

        public int PermisoID
        {
            get => this.permisoid;
            set => this.permisoid = value;
        }

        public event BasePage.LanguageChanged LanguageChangedEvent;

        public event BasePage.OnPageRefresh OnPageRefreshEvent;

        public bool ValidaSessionActiva()
        {
            if (HttpContext.Current.Session["UserId"] == null)
                throw new Exception(this.CommonResourceManager.GetMessage("msgSinSesion") == null ? " -999.- The session has expired requires re-login." : this.CommonResourceManager.GetMessage("msgSinSesion"));
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            if (!this.Page.IsPostBack)
            {
                this.RunJavascriptBeforeLoadPage("var recursosGlobal = jQuery.parseJSON('" + this.GetCommonResourcesJSON() + "');");
                string resourcesJson = this.GetResourcesJSON();
                this.RunJavascriptBeforeLoadPage("var fechaHoy = '" + DateTime.Now.ToShortDateString() + "';");
                this.RunJavascript("var recursos = jQuery.parseJSON('" + resourcesJson + "');");
                if (this.PermisosPantalla.Rows.Count > 0)
                    this.RunJavascriptBeforeLoadPage("var permisosPantalla = jQuery.parseJSON('" + scriptSerializer.Serialize((object)this.DataTableToMap(this.PermisosPantalla)) + "');");
            }
            base.OnLoad(e);
            System.Data.DataTable table = new logic_acces(ConfigurationManager.ConnectionStrings["Conexion"].ToString()).ExecuteQuery("UsuarioPermisoPantalla_Sel", new Dictionary<string, string>()
      {
        {
          "PermisoID",
          this.PermisoID.ToString()
        }
      }).Tables[0];
            if (table.Rows.Count <= 0)
                return;
            if (int.Parse(table.Rows[0]["PermisoID"].ToString()) != 13 && !bool.Parse(table.Rows[0]["AccesoPantalla"].ToString()))
                this.Response.Redirect(this.URL + "pages/Login.aspx?ReturnUrl=" + this.Request.Url.PathAndQuery, true);
            this.RunJavascriptBeforeLoadPage("var accesoPantalla = jQuery.parseJSON('" + scriptSerializer.Serialize((object)this.DataTableToMap(table)) + "');");
        }

        protected override void OnPreInit(EventArgs e)
        {
            this.Response.AppendHeader("X-UA-Compatible", "IE=edge,chrome=1");
            this.Theme = HttpContext.Current.Session["Tema"] == null ? "default" : HttpContext.Current.Session["Tema"].ToString();
            if (this.Session["UserId"] == null)
                this.Response.Redirect(this.URL + "pages/Login.aspx?" + string.Empty + "ReturnUrl=" + this.Request.Url.PathAndQuery, true);
            base.OnPreInit(e);
        }

        protected override void OnInit(EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                CtrlsTranslator.Translate((Page)this, this.ResourceManager);
                this.CleanSession();
            }
            base.OnInit(e);
            this.DisableClientCaching();
        }

        public void RaiseLanguageChanged(CultureInfo cultureInfo)
        {
            if (this.LanguageChangedEvent == null)
                return;
            CultureManager.StoreCulture(cultureInfo);
            CtrlsTranslator.Translate((Page)this, this.ResourceManager);
            this.LanguageChangedEvent(cultureInfo);
            this.Response.Redirect(this.Request.RawUrl);
        }

        public void RaiseOnPageRefresh(EventArgs e)
        {
            if (this.OnPageRefreshEvent == null)
                return;
            this.OnPageRefreshEvent(e);
        }

        protected override void InitializeCulture()
        {
            base.InitializeCulture();
            CultureManager.Initialize();
        }

        public string GetMessage(string resourceID) => this.ResourceManager.GetMessage(resourceID);

        public string GetCommonMessage(string resourceID) => this.CommonResourceManager.GetMessage(resourceID);

        public void RunJavascript(string script)
        {
            Page currentHandler = (Page)HttpContext.Current.CurrentHandler;
            ScriptManager.RegisterStartupScript(currentHandler, currentHandler.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        public void RunJavascriptBeforeLoadPage(string script) => ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), script, true);

        public string GetAppSetting(string key) => WebConfigurationManager.AppSettings[key];

        public object GetSession(string name) => this.Session[name];

        public void SetSession(string name, object value, SessionStateModes sessionMode)
        {
            if (sessionMode == SessionStateModes.SinglePage)
            {
                Hashtable hashtable1;
                Hashtable hashtable2;
                if (this.Session["SessionState"] == null)
                {
                    hashtable1 = new Hashtable();
                    this.Session["SessionState"] = (object)hashtable1;
                    hashtable2 = new Hashtable();
                    this.Session["SessionTime"] = (object)hashtable2;
                }
                else
                {
                    hashtable1 = (Hashtable)this.Session["SessionState"];
                    hashtable2 = (Hashtable)this.Session["SessionTime"];
                }
                if (!hashtable1.ContainsKey((object)name))
                {
                    hashtable1.Add((object)name, (object)HttpContext.Current.Request.CurrentExecutionFilePath);
                    hashtable2.Add((object)name, (object)DateTime.Now.Ticks);
                }
                else
                {
                    hashtable1[(object)name] = (object)HttpContext.Current.Request.CurrentExecutionFilePath;
                    hashtable2[(object)name] = (object)DateTime.Now.Ticks;
                }
            }
            this.Session[name] = value;
        }

        public void CleanSession()
        {
            if (this.Session["SessionTime"] == null)
                return;
            IList<string> stringList = (IList<string>)new List<string>();
            Hashtable hashtable1 = (Hashtable)this.Session["SessionTime"];
            Hashtable hashtable2 = (Hashtable)this.Session["SessionState"];
            foreach (DictionaryEntry dictionaryEntry in hashtable1)
            {
                if (new TimeSpan(DateTime.Now.Ticks - long.Parse(dictionaryEntry.Value.ToString())).TotalMilliseconds > 300000.0)
                    stringList.Add(dictionaryEntry.Key.ToString());
            }
            foreach (string str in (IEnumerable<string>)stringList)
            {
                hashtable2.Remove((object)str);
                hashtable1.Remove((object)str);
                this.Session.Remove(str);
            }
        }

        public string GetNombrePC()
        {
            string empty = string.Empty;
            string ipString;
            try
            {
                ipString = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
            }
            catch
            {
                ipString = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
            }
            if (string.IsNullOrWhiteSpace(ipString))
            {
                ipString = Environment.MachineName;
            }
            else
            {
                IPAddress address = (IPAddress)null;
                if (!IPAddress.TryParse(ipString, out address))
                    ipString = ipString.Split('.')[0];
            }
            return ipString;
        }

        public string GetResourcesJSON() => new JavaScriptSerializer().Serialize((object)this.DataTableToMap(this.ResourceManager.GetResourcesValues()));

        public string GetCommonResourcesJSON() => new JavaScriptSerializer().Serialize((object)this.DataTableToMap(this.CommonResourceManager.GetResourcesValues()));

        public void Mensaje(string msj, int tipo) => this.RunJavascript("Ex.mensajes('" + msj.Replace("'", "") + "'," + (object)tipo + ");");

        public static Decimal? DecimalIsNull(string numero) => numero == "" ? new Decimal?() : new Decimal?(Decimal.Parse(numero));

        public static string ToString(object value) => value is DBNull ? string.Empty : Convert.ToString(value);

        public static int ToInt32(object value) => value is DBNull ? 0 : Convert.ToInt32(value);

        public static Decimal ToDecimal(object value) => value is DBNull ? 0M : Convert.ToDecimal(value);

        public static bool ToBoolean(object value) => !(value is DBNull) && Convert.ToBoolean(value);

        public static DateTime ToDateTime(object value) => value is DBNull ? DateTime.MinValue : Convert.ToDateTime(value);

        public List<Dictionary<string, object>> DataTableToMap(System.Data.DataTable p_dt)
        {
            List<Dictionary<string, object>> map = new List<Dictionary<string, object>>();
            foreach (DataRow row in (InternalDataCollectionBase)p_dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (DataColumn column in (InternalDataCollectionBase)p_dt.Columns)
                    dictionary.Add(column.ColumnName, row[column]);
                map.Add(dictionary);
            }
            return map;
        }

        public Dictionary<string, object> DataTableToDiccionary(System.Data.DataTable p_dt)
        {
            Dictionary<string, object> diccionary = (Dictionary<string, object>)null;
            foreach (DataRow row in (InternalDataCollectionBase)p_dt.Rows)
            {
                diccionary = new Dictionary<string, object>();
                foreach (DataColumn column in (InternalDataCollectionBase)p_dt.Columns)
                    diccionary.Add(column.ColumnName, row[column]);
            }
            return diccionary;
        }

        public static System.Data.DataTable GetDataTableFromDictionaries<T>(
          List<Dictionary<string, T>> list)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            if (list == null || !list.Any<Dictionary<string, T>>())
                return dataTable;
            foreach (DataColumn column in list.First<Dictionary<string, T>>().Select<KeyValuePair<string, T>, DataColumn>((Func<KeyValuePair<string, T>, DataColumn>)(c => new DataColumn(c.Key, typeof(T)))))
                dataTable.Columns.Add(column);
            foreach (DataRow row in list.Select<Dictionary<string, T>, DataRow>((Func<Dictionary<string, T>, DataRow>)(r =>
            {
                DataRow dataRow = dataTable.NewRow();
                r.ToList<KeyValuePair<string, T>>().ForEach((Action<KeyValuePair<string, T>>)(c => dataRow.SetField<T>(c.Key, c.Value)));
                return dataRow;
            })))
                dataTable.Rows.Add(row);
            return dataTable;
        }

        public Dictionary<string, object> Info(System.Data.DataTable dtInfo) => new Dictionary<string, object>();

        public string Serialize(Dictionary<string, object> a) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Serialize((object)a);

        public string SerializerJson(List<Dictionary<string, object>> a) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Serialize((object)a);

        public string SerializerJsonString(Dictionary<string, string> a) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Serialize((object)a);

        public string SerializerJsonStringList(List<Dictionary<string, string>> a) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Serialize((object)a);

        public string SerializerJson(string a) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Serialize((object)a);

        public List<Dictionary<string, string>> Deserialize(string json) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Deserialize<List<Dictionary<string, string>>>(json);

        public Dictionary<string, string> DeserializeData(string json) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Deserialize<Dictionary<string, string>>(json);

        public Dictionary<string, object> DeserializeObj(string json) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Deserialize<Dictionary<string, object>>(json);

        public List<Dictionary<string, object>> DeserializeDataObj(string json) => new JavaScriptSerializer()
        {
            MaxJsonLength = int.MaxValue
        }.Deserialize<List<Dictionary<string, object>>>(json);

        public void InsertDetail(
          logic_acces a,
          string llave,
          string deserializeJson,
          string store,
          string valorLlave)
        {
            List<Dictionary<string, string>> dictionaryList1 = new List<Dictionary<string, string>>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<Dictionary<string, string>> dictionaryList2 = this.Deserialize(deserializeJson);
            for (int index = 0; index < dictionaryList2.Count; ++index)
            {
                Dictionary<string, string> parameters = dictionaryList2[index];
                parameters[llave] = valorLlave;
                a.ExecuteNonQuery(store, parameters);
            }
        }

        public void InsertDetailSimple(
          logic_acces a,
          string llave,
          string deserializeJson,
          string store,
          string valorLlave)
        {
            List<Dictionary<string, string>> dictionaryList1 = new List<Dictionary<string, string>>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<Dictionary<string, string>> dictionaryList2 = this.Deserialize(deserializeJson);
            for (int index = 0; index < dictionaryList2.Count; ++index)
            {
                Dictionary<string, string> parameters = dictionaryList2[index];
                parameters[llave] = valorLlave;
                a.ExecuteNonQuerySimple(store, parameters);
            }
        }

        public void LogError(
            string Module,
            string FunctionName,
            string  LoggedUser,
            string InnerMessage,
            string ShortMessage 
            )
        {
            Dictionary<string, string> datos = new Dictionary<string, string>();

            datos["Module"]  = Module;
            datos["FunctionName"] = FunctionName;
            datos["LoggedUser"] = LoggedUser;
            datos["InnerMessage"] = InnerMessage;
            datos["ShortMessage"] = ShortMessage;

            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB, false);

            DataSet ds = logicAcces.ExecuteQuery("LogError_Ins", datos);


        }

        public bool SendMail(
          string[] pMails,
          string pSubject,
          string pBody,
          bool isBodyHtml,
          string[] attachments,
          out string messageError)
        {
            return this.SendMail(pMails, new string[0], new string[0], pSubject, pBody, isBodyHtml, attachments, out messageError, (Dictionary<string, Stream>)null);
        }

        public bool SendMail(
          string[] pMails,
          string[] pBccMails,
          string[] pCCMails,
          string pSubject,
          string pBody,
          bool isBodyHtml,
          string[] attachments,
          out string messageError)
        {
            return this.SendMail(pMails, pBccMails, pCCMails, pSubject, pBody, isBodyHtml, attachments, out messageError, (Dictionary<string, Stream>)null);
        }

        public bool SendMail(
          string[] pMails,
          string[] pBccMails,
          string[] pCCMails,
          string pSubject,
          string pBody,
          bool isBodyHtml,
          string[] attachments,
          out string messageError,
          Dictionary<string, Stream> stearmAttachments)
        {
            messageError = string.Empty;
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            string str1 = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "1"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            string host = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "2"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            string str2 = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "3"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            string password = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "4"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            string displayName = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "5"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            if (str1 == "0")
            {
                messageError = "No esta configurado el sistema para envio de correos, favor de verificar.";
                return false;
            }
            if (!(str1 == "1"))
                return false;
            try
            {
                SmtpClient smtpClient = new SmtpClient(host);
                smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(str2, password);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(str2, displayName);
                string str3 = " <style type=\"text/css\"> body, p, table, div, ul, li  {font-family:\"Verdana\", \"Arial\"; font-weight:normal; font-size:12px; } </style> ";
                message.Subject = pSubject;
                message.IsBodyHtml = isBodyHtml;
                message.Body = str3 + pBody;
                foreach (string pMail in pMails)
                {
                    if (pMail != "")
                        message.To.Add(pMail.Trim());
                }
                foreach (string pBccMail in pBccMails)
                {
                    if (pBccMail != "")
                        message.Bcc.Add(pBccMail.Trim());
                }
                foreach (string pCcMail in pCCMails)
                {
                    if (pCcMail != "")
                        message.CC.Add(pCcMail.Trim());
                }
                foreach (string attachment1 in attachments)
                {
                    if (attachment1 != "" && System.IO.File.Exists(attachment1))
                    {
                        Attachment attachment2 = new Attachment(attachment1);
                        message.Attachments.Add(attachment2);
                    }
                }
                if (stearmAttachments != null)
                {
                    foreach (KeyValuePair<string, Stream> stearmAttachment in stearmAttachments)
                    {
                        Attachment attachment = new Attachment(stearmAttachment.Value, stearmAttachment.Key);
                        message.Attachments.Add(attachment);
                    }
                }
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                messageError = ex.Message;
                return false;
            }
        }

        public bool SendMail(
          string[] pMails,
          string pSubject,
          string pBody,
          bool isBodyHtml,
          string[] attachments,
          out string messageError,
          Dictionary<string, Stream> stearmAttachments,
          string conexion)
        {
            messageError = string.Empty;
            logic_acces logicAcces = new logic_acces(conexion, false);
            string str1 = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "1"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            string host = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "2"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            string str2 = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "3"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            string password = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "4"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            string displayName = logicAcces.ExecuteQuery("get_Parametro", new Dictionary<string, string>()
      {
        {
          "parametroId",
          "5"
        }
      }).Tables[0].Rows[0]["valor"].ToString();
            if (str1 == "0")
            {
                messageError = "No esta configurado el sistema para envio de correos, favor de verificar.";
                return false;
            }
            if (!(str1 == "1"))
                return false;
            try
            {
                SmtpClient smtpClient = new SmtpClient(host);
                smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(str2, password);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(str2, displayName);
                string str3 = " <style type=\"text/css\"> body, p, table, div, ul, li  {font-family:\"Verdana\", \"Arial\"; font-weight:normal; font-size:12px; } </style> ";
                message.Subject = pSubject;
                message.IsBodyHtml = isBodyHtml;
                message.Body = str3 + pBody;
                foreach (string pMail in pMails)
                {
                    if (pMail != "")
                        message.To.Add(pMail.Trim());
                }
                foreach (string attachment1 in attachments)
                {
                    if (attachment1 != "" && System.IO.File.Exists(attachment1))
                    {
                        Attachment attachment2 = new Attachment(attachment1);
                        message.Attachments.Add(attachment2);
                    }
                }
                if (stearmAttachments != null)
                {
                    foreach (KeyValuePair<string, Stream> stearmAttachment in stearmAttachments)
                    {
                        Attachment attachment = new Attachment(stearmAttachment.Value, stearmAttachment.Key);
                        message.Attachments.Add(attachment);
                    }
                }
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                messageError = ex.Message;
                return false;
            }
        }

        public bool EnviarMail(string conexion)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 25);
                smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential("", "");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage message = new MailMessage();
                message.From = new MailAddress("j", "NameToDisplay");
                message.To.Add(new MailAddress("j"));
                message.Body = "This is a test email. Please ignore or delete.";
                message.Subject = "Sent from C#";
                smtpClient.Send(message);
                return true;
            }
            catch (SmtpException ex)
            {
            }
            return false;
        }

        public static string SerializeOne(Dictionary<string, string> a) => new JavaScriptSerializer().Serialize((object)a);

        public static Dictionary<string, string> DeserializeOne(string json) => new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);

        public static string[] DeserializeArray(string json) => new JavaScriptSerializer().Deserialize<string[]>(json);

        public static void WriteLineOnTxt(string message)
        {
            string str = DateTime.Now.ToString("ddMMyyyy");
            StreamWriter streamWriter = new StreamWriter(HostingEnvironment.MapPath("~/Log/") + str + ".txt", true);
            streamWriter.WriteLine(message);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            IEnumerable<Control> controls = control.Controls.Cast<Control>();
            return controls.SelectMany<Control, Control>((Func<Control, IEnumerable<Control>>)(ctrl => this.GetAll(ctrl, type))).Concat<Control>(controls).Where<Control>((Func<Control, bool>)(c => c.GetType() == type));
        }

        public static string Encripta(string Password)
        {
            string str1 = "";
            try
            {
                SHA1 shA1 = SHA1.Create();
                byte[] bytes = new ASCIIEncoding().GetBytes(Password);
                shA1.ComputeHash(bytes);
                str1 = Convert.ToBase64String(shA1.Hash);
            }
            catch (Exception ex)
            {
                string str2 = "Error in HashCode : " + ex.Message;
            }
            return str1;
        }

        protected System.Data.DataTable SetColumnsName(System.Data.DataTable dt)
        {
            int count = dt.Columns.Count;
            for (int index = 0; index < count; ++index)
            {
                if (!dt.Columns[index].ColumnName.Contains("Export"))
                {
                    dt.Columns.Remove(dt.Columns[index].ColumnName);
                    --count;
                }
            }
            return dt;
        }

        public void ExportDataSet(DataSet dsInfo, string fileName)
        {
            try
            {
                ExcelPackage excelPackage = new ExcelPackage(new FileInfo(this.Server.MapPath("~/Templates/" + fileName + ".xlsx")), true);
                using (new ExcelPackage())
                {
                    if (dsInfo != null)
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[dsInfo.Tables[0].TableName];
                        string Name = "";
                        int rows = 0;
                        int num1 = 0;
                        for (int index1 = 0; index1 < dsInfo.Tables.Count; ++index1)
                        {
                            System.Data.DataTable table = dsInfo.Tables[index1];
                            ExcelWorksheet excelWorksheet;
                            if (index1 == 0)
                            {
                                excelWorksheet = excelPackage.Workbook.Worksheets["Sheet1"];
                                if (excelWorksheet == null)
                                    throw new Exception("Error al exportar la información, error en el formato layout. Falta Worksheet Sheet1");
                                excelWorksheet.Name = table.TableName;
                                Name = excelWorksheet.Name;
                                rows = table.Rows.Count;
                                num1 = table.Columns.Count;
                            }
                            else
                            {
                                excelWorksheet = excelPackage.Workbook.Worksheets.Copy(Name, table.TableName);

                               

                                //if (excelWorksheet._rows.Count > 0)
                                //{
                                    for (int index2 = 0; index2 < num1; ++index2)
                                        excelWorksheet.Cells[1, index2 + 1].Value = (object)"";
                                    excelWorksheet.DeleteRow(2, rows, true);
                                //}
                                Name = excelWorksheet.Name;
                                rows = table.Rows.Count;
                                num1 = table.Columns.Count;
                            }
                            int num2 = 0;
                            for (int index3 = 0; index3 < table.Columns.Count; ++index3)
                            {
                                if (table.Columns[index3].ColumnName.Contains("Export"))
                                {
                                    if (table.Rows.Count == 0)
                                    {
                                        excelWorksheet.Cells[1, num2 + 1].Value = (object)table.Columns[index3].ColumnName.Replace("Export", "");
                                        ++num2;
                                    }
                                    else
                                    {
                                        for (int index4 = 0; index4 < table.Rows.Count; ++index4)
                                        {
                                            if (index4 == 0)
                                                excelWorksheet.Cells[index4 + 1, num2 + 1].Value = (object)table.Columns[index3].ColumnName.Replace("Export", "");
                                            excelWorksheet.Cells[index4 + 2, num2 + 1].Value = table.Rows[index4][index3] != null ? (object)table.Rows[index4][index3].ToString() : (object)string.Empty;
                                        }
                                        ++num2;
                                    }
                                }
                            }
                        }
                    }
                    this.Response.Clear();
                    this.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                    this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    this.Response.BinaryWrite(excelPackage.GetAsByteArray());
                    this.Response.End();
                }
            }
            catch (Exception ex)
            {
                this.Response.End();
                Thread.ResetAbort();
                this.Context.Session.Add("Error", (object)ex.Message);
                this.Context.Response.Redirect(this.URL + "pages/Error.aspx", false);
            }
        }

        public void Export(GridView dgGrid, string fileName)
        {
            byte[] excel = this.ToExcel(dgGrid);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.BinaryWrite(excel);
            response.ContentType = "application/excel";
            response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("MMddyyyyHHmm") + "_" + fileName + ".xls");
            response.End();
        }

        public byte[] ToExcel(GridView objects)
        {
            System.Data.DataTable dataSource = (System.Data.DataTable)objects.DataSource;
            objects.AllowPaging = false;
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet(dataSource.TableName);
            sheet.DisplayGridlines = false;
            IRow row1 = sheet.CreateRow(0);
            int column1 = 0;
            foreach (string name in dataSource.Columns.OfType<DataColumn>().Select<DataColumn, string>((Func<DataColumn, string>)(col => col.ColumnName)).ToList<string>())
            {
                if (name.Contains("Export"))
                {
                    string message = this.GetMessage(string.Format("{0}-{1}", (object)dataSource.TableName, (object)dataSource.Columns[name].ColumnName));
                    string str = !string.IsNullOrEmpty(message) ? message : dataSource.Columns[name].ColumnName.Replace("Export", "");
                    ICell cell = row1.CreateCell(column1);
                    ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();
                    IFont font = hssfWorkbook.CreateFont();
                    font.Color = (short)9;
                    font.Boldweight = (short)700;
                    cellStyle.FillForegroundColor = (short)17;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.FillBackgroundColor = (short)17;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(font);
                    cell.CellStyle = cellStyle;
                    cell.SetCellValue(str);
                    sheet.AutoSizeColumn(column1);
                    ++column1;
                }
                else
                    dataSource.Columns.Remove(name);
            }
            int rownum = 1;
            foreach (DataRow row2 in (InternalDataCollectionBase)dataSource.Rows)
            {
                int count = dataSource.Rows.Count;
                int column2 = 0;
                IRow row3 = sheet.CreateRow(rownum);
                for (int columnIndex = 0; columnIndex < dataSource.Columns.Count; ++columnIndex)
                {
                    ICell cell = row3.CreateCell(column2);
                    BasePage.ToString(row2[columnIndex]).Replace("&nbsp;", "");
                    cell.SetCellValue(BasePage.ToString(row2[columnIndex]).Replace("&nbsp;", ""));
                    ++column2;
                }
                ++rownum;
            }
            MemoryStream out1 = new MemoryStream();
            hssfWorkbook.Write((Stream)out1);
            return out1.ToArray();
        }

        protected void Export(GridView dgGrid, GridView dgGrid2, string fileName)
        {
            byte[] excelDoubleSheet = this.ToExcel_Double_Sheet(dgGrid, dgGrid2);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.BinaryWrite(excelDoubleSheet);
            response.ContentType = "application/excel";
            response.AddHeader("Content-Disposition", "attachment; filename=" + this.UIDPage.Substring(0, 8) + "_" + fileName + ".xls");
            response.End();
        }

        public static bool EsTienePermisoObjeto(int ServicioId, int SocioId, int ModuloId)
        {
            DataSet dataSet = new logic_acces(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString).ExecuteQuery("dbo.mainObtienePermisoModulo", new Dictionary<string, string>()
            {
                ["ServicioID"] = ServicioId.ToString(),
                ["SocioID"] = SocioId.ToString(),
                [nameof(ModuloId)] = ModuloId.ToString()
            });
            return dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0;
        }

        public static T XmlDeserializeFromString<T>(string objectData) => (T)BasePage.XmlDeserializeFromString(objectData, typeof(T));

        public static object XmlDeserializeFromString(string objectData, Type type)
        {
            object obj;
            using (TextReader textReader = (TextReader)new StringReader(objectData))
                obj = new XmlSerializer(type).Deserialize(textReader);
            return obj;
        }

        public static string ObtieneMenuAplicacion(int ServicioId, int SocioId)
        {
            BasePage basePage = new BasePage();
            string str = "{}";
            logic_acces logicAcces = new logic_acces(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
           // var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                ["ServicioID"] = ServicioId.ToString(),
                ["SocioID"] = SocioId.ToString()
            };
            if (basePage.GetSession("sessionMenuPermisosItems") == null)
            {
                System.Data.DataTable dataTable = new SeguridadPortalRHSoapClient().ObtieneMenuServicio(ServicioId, SocioId);
                if (dataTable.Rows.Count > 0)
                {
                    logic.Class.Menu menu = BasePage.XmlDeserializeFromString<logic.Class.Menu>(dataTable.Rows[0][0] as string);
                    str = new JavaScriptSerializer().Serialize((object)menu);
                    basePage.SetSession("sessionMenuPermisosItems", (object)menu, SessionStateModes.AllPages);
                }
            }
            else
                str = new JavaScriptSerializer().Serialize((object)(basePage.GetSession("sessionMenuPermisosItems") as logic.Class.Menu));
            if (basePage.GetSession("DTpermisos") == null)
            {
                System.Data.DataTable dataTable = new SeguridadPortalRHSoapClient().ObtieneMenuServicio(ServicioId, SocioId);
                if (dataTable.Rows.Count > 0)
                {
                    logic.Class.Menu menu = BasePage.XmlDeserializeFromString<logic.Class.Menu>(dataTable.Rows[0][0] as string);
                    basePage.SetSession("DTpermisos", (object)menu, SessionStateModes.AllPages);
                }
            }
            return str;
        }

        public static logic.Class.Menu ObtieneMenuXML(int ServicioId, int SocioId)
        {
            logic.Class.Menu menu = new logic.Class.Menu();
            logic_acces logicAcces = new logic_acces(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                ["ServicioID"] = ServicioId.ToString(),
                ["SocioID"] = SocioId.ToString()
            };
            System.Data.DataTable dataTable = new SeguridadPortalRHSoapClient().ObtieneMenuServicio(ServicioId, SocioId);
            if (dataTable.Rows.Count > 0)
                menu = BasePage.XmlDeserializeFromString<logic.Class.Menu>(dataTable.Rows[0][0] as string);
            return menu;
        }

        private void DisableClientCaching()
        {
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
            this.Response.Cache.SetNoStore();
        }

        public byte[] ToExcel_Double_Sheet(GridView objects, GridView objects2)
        {
            System.Data.DataTable dataSource1 = (System.Data.DataTable)objects.DataSource;
            objects.AllowPaging = false;
            System.Data.DataTable dataSource2 = (System.Data.DataTable)objects2.DataSource;
            objects2.AllowPaging = false;
            if (dataSource1.TableName == dataSource2.TableName)
                dataSource2.TableName = dataSource1.TableName + "-1";
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet1 = hssfWorkbook.CreateSheet(dataSource1.TableName);
            sheet1.DisplayGridlines = false;
            IRow row1 = sheet1.CreateRow(0);
            ISheet sheet2 = hssfWorkbook.CreateSheet(dataSource2.TableName);
            sheet2.DisplayGridlines = false;
            IRow row2 = sheet2.CreateRow(0);
            int column1 = 0;
            foreach (string name in dataSource1.Columns.OfType<DataColumn>().Select<DataColumn, string>((Func<DataColumn, string>)(col => col.ColumnName)).ToList<string>())
            {
                string message = this.GetMessage(string.Format("{0}-{1}", (object)dataSource1.TableName, (object)dataSource1.Columns[name].ColumnName));
                string str = !string.IsNullOrEmpty(message) ? message : dataSource1.Columns[name].ColumnName;
                ICell cell = row1.CreateCell(column1);
                ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();
                IFont font = hssfWorkbook.CreateFont();
                font.Color = (short)9;
                font.Boldweight = (short)700;
                cellStyle.FillForegroundColor = (short)17;
                cellStyle.FillPattern = FillPattern.SolidForeground;
                cellStyle.FillBackgroundColor = (short)17;
                cellStyle.Alignment = HorizontalAlignment.Center;
                cellStyle.SetFont(font);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(str);
                sheet1.AutoSizeColumn(column1);
                ++column1;
            }
            int rownum1 = 1;
            foreach (DataRow row3 in (InternalDataCollectionBase)dataSource1.Rows)
            {
                int count = dataSource1.Rows.Count;
                int column2 = 0;
                IRow row4 = sheet1.CreateRow(rownum1);
                for (int columnIndex = 0; columnIndex < dataSource1.Columns.Count; ++columnIndex)
                {
                    ICell cell = row4.CreateCell(column2);
                    BasePage.ToString(row3[columnIndex]).Replace("&nbsp;", "");
                    cell.SetCellValue(BasePage.ToString(row3[columnIndex]).Replace("&nbsp;", ""));
                    if (count == rownum1)
                        sheet1.AutoSizeColumn(column2);
                    ++column2;
                }
                ++rownum1;
            }
            int column3 = 0;
            foreach (string name in dataSource2.Columns.OfType<DataColumn>().Select<DataColumn, string>((Func<DataColumn, string>)(col => col.ColumnName)).ToList<string>())
            {
                string message = this.GetMessage(string.Format("{0}-{1}", (object)dataSource2.TableName, (object)dataSource2.Columns[name].ColumnName));
                string str = !string.IsNullOrEmpty(message) ? message : dataSource2.Columns[name].ColumnName;
                ICell cell = row2.CreateCell(column3);
                ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();
                IFont font = hssfWorkbook.CreateFont();
                font.Color = (short)9;
                font.Boldweight = (short)700;
                cellStyle.FillForegroundColor = (short)17;
                cellStyle.FillPattern = FillPattern.SolidForeground;
                cellStyle.FillBackgroundColor = (short)17;
                cellStyle.Alignment = HorizontalAlignment.Center;
                cellStyle.SetFont(font);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(str);
                sheet2.AutoSizeColumn(column3);
                ++column3;
            }
            int rownum2 = 1;
            foreach (DataRow row5 in (InternalDataCollectionBase)dataSource2.Rows)
            {
                int count = dataSource2.Rows.Count;
                int column4 = 0;
                IRow row6 = sheet2.CreateRow(rownum2);
                for (int columnIndex = 0; columnIndex < dataSource2.Columns.Count; ++columnIndex)
                {
                    ICell cell = row6.CreateCell(column4);
                    BasePage.ToString(row5[columnIndex]).Replace("&nbsp;", "");
                    cell.SetCellValue(BasePage.ToString(row5[columnIndex]).Replace("&nbsp;", ""));
                    if (count == rownum2)
                        sheet2.AutoSizeColumn(column4);
                    ++column4;
                }
                ++rownum2;
            }
            MemoryStream out1 = new MemoryStream();
            hssfWorkbook.Write((Stream)out1);
            return out1.ToArray();
        }

        public void FillCmb(string sp, string jsvar)
        {
            logic_acces logicAcces = new logic_acces(BasePage.ConexionDB);
            Dictionary<string, string> p_datos = new Dictionary<string, string>();
            string str = this.SerializerJson(this.DataTableToMap(logicAcces.ExecuteQuery(sp, p_datos).Tables[0]));
            this.RunJavascriptBeforeLoadPage("var " + jsvar + " = jQuery.parseJSON('" + HttpUtility.JavaScriptStringEncode(str) + "');");
        }
        public string ExportToExcel2(System.Data.DataTable dt, string FileName)

        {
            string fileN = FileName + "_" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".xlsx";
            string ruta = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings.Get("PathFiles") + fileN );

            FileInfo fileInfo = new FileInfo(ruta);

            ExcelPackage pck = new ExcelPackage(fileInfo);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
            ws.Cells["A1"].LoadFromDataTable(dt, true);
            pck.Save();

            //ExcelPackage pck = new ExcelPackage();
            //ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
            //ws.Cells["A1"].LoadFromDataTable(dt, true);
            //var ms = new System.IO.MemoryStream();
            //var ms = new System.IO.Me
            //pck.SaveAs(ms);


            //return ms.ToArray();

            return fileN;
        }
        public byte[] ExportToExcel(System.Data.DataTable dt)
        {
            ExcelPackage excelPackage = new ExcelPackage(new FileInfo(HttpContext.Current.Server.MapPath("~/Templates/Default_Layout.xlsx")), true);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
            List<string> stringList = new List<string>();
            int Row1 = 2;
            int Col1 = 2;
            foreach (DataColumn column in (InternalDataCollectionBase)dt.Columns)
            {
                string message = this.GetMessage(string.Format("{0}-{1}", (object)dt.TableName, (object)column.ColumnName));
                if (!string.IsNullOrEmpty(message))
                {
                    worksheet.Cells[Row1, Col1].Value = (object)message;
                    stringList.Add(column.ColumnName);
                    ++Col1;
                }
            }
            int Row2 = 3;
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                int Col2 = 2;
                foreach (string columnName in stringList)
                {
                    worksheet.Cells[Row2, Col2].Value = row[columnName];
                    ++Col2;
                }
                ++Row2;
            }
            return excelPackage.GetAsByteArray();
        }

        public static string ObtieneXML(List<Dictionary<string, string>> registrosActuales)
        {
            string str = "<root>";
            foreach (Dictionary<string, string> registrosActuale in registrosActuales)
            {
                Dictionary<string, string> dictionary = registrosActuale;
                if (dictionary.ContainsKey("$$hashKey"))
                    dictionary.Remove("$$hashKey");
                if (dictionary.ContainsKey("EsEditar"))
                    dictionary.Remove("EsEditar");
                XElement xelement = new XElement((XName)"item", (object)registrosActuale.Select<KeyValuePair<string, string>, XElement>((Func<KeyValuePair<string, string>, XElement>)(kv => new XElement((XName)kv.Key, (object)kv.Value))));
                str += xelement.ToString();
            }
            return str + "</root>";
        }

        public static string ObtieneXML(List<Dictionary<string, object>> registrosActuales)
        {
            string str = "<root>";
            foreach (Dictionary<string, object> registrosActuale in registrosActuales)
            {
                Dictionary<string, object> dictionary = registrosActuale;
                if (dictionary.ContainsKey("$$hashKey"))
                    dictionary.Remove("$$hashKey");
                if (dictionary.ContainsKey("EsEditar"))
                    dictionary.Remove("EsEditar");
                XElement xelement = new XElement((XName)"item", (object)registrosActuale.Select<KeyValuePair<string, object>, XElement>((Func<KeyValuePair<string, object>, XElement>)(kv => new XElement((XName)kv.Key, kv.Value))));
                str += xelement.ToString();
            }
            return str + "</root>";
        }

        public static string ReplaceSpecialCharacter(string html)
        {
            if (html != null)
            {
                html = html.Replace("á", "a");
                html = html.Replace("é", "e");
                html = html.Replace("í", "i");
                html = html.Replace("ó", "o");
                html = html.Replace("ú", "u");
            }
            return html;
        }

        public delegate void LanguageChanged(CultureInfo cultureInfo);

        public delegate void OnPageRefresh(EventArgs e);
    }
}
