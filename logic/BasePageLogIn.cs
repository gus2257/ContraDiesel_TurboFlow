
using logic.Common.Resources;
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
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace logic
{
    public class BasePageLogIn : Page
    {
        private const string SessionTime = "SessionTime";
        private const string SessionState = "SessionState";
        private const string UidPage = "UidPage";
        public const string REMOTE_HOST = "REMOTE_HOST";
        private KeyValuePlainTextResource resourceMgr;
        private KeyValuePlainTextResource commonResourceMgr;

        public string Token
        {
            get => this.GetSession("token") != null ? this.GetSession("token").ToString() : string.Empty;
            set => this.SetSession("token", (object)value, SessionStateModes.AllPages);
        }

        public string SqlLanguage
        {
            get
            {
                if (!(this.Session["SESSION_CULTURE"] is CultureInfo cultureInfo))
                    return "Spanish";
                string name = cultureInfo.Name;
                return name == "es-MX" || !(name == "en-US") ? "Spanish" : "English";
            }
        }

        public KeyValuePlainTextResource ResourceManager
        {
            get
            {
                if (this.resourceMgr == null)
                    this.resourceMgr = ResourceFactory.CreateResource(this.GetType().Name, !this.IsPostBack);
                return this.resourceMgr;
            }
        }

        public KeyValuePlainTextResource CommonResourceManager
        {
            get
            {
                if (this.commonResourceMgr == null)
                    this.commonResourceMgr = ResourceFactory.CreateResource("GlobalResources", !this.IsPostBack);
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

        public event BasePageLogIn.LanguageChanged LanguageChangedEvent;

        public event BasePageLogIn.OnPageRefresh OnPageRefreshEvent;

        protected override void OnLoad(EventArgs e)
        {
            this.RunJavascriptBeforeLoadPage("var recursosGlobal = jQuery.parseJSON('" + this.GetCommonResourcesJSON() + "');");
            this.RunJavascript("var recursos = jQuery.parseJSON('" + this.GetResourcesJSON() + "');");
            base.OnLoad(e);
        }

        protected override void OnPreInit(EventArgs e)
        {
            this.Response.AppendHeader("X-UA-Compatible", "IE=edge,chrome=1");
            this.Theme = HttpContext.Current.Session["Tema"] == null ? "default" : HttpContext.Current.Session["Tema"].ToString();
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

        public void RunJavascript(string script) => ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), script, true);

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
            string ipString = string.Empty;
            try
            {
                IPHostEntry ipHostEntry = new IPHostEntry();
                IPHostEntry hostEntry = Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
                if (!string.IsNullOrEmpty(hostEntry.HostName))
                    ipString = hostEntry.HostName;
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

        public void Mensaje(string msj, int tipo) => ScriptManager.RegisterClientScriptBlock((Page)this, this.GetType(), "Aviso", "Ex.mensajes('" + msj.Replace("'", "") + "'," + (object)tipo + ");", true);

        public static Decimal? DecimalIsNull(string numero) => numero == "" ? new Decimal?() : new Decimal?(Decimal.Parse(numero));

        public static string ToString(object value) => value is DBNull ? string.Empty : Convert.ToString(value);

        public static int ToInt32(object value) => value is DBNull ? 0 : Convert.ToInt32(value);

        public static Decimal ToDecimal(object value) => value is DBNull ? 0M : Convert.ToDecimal(value);

        public static bool ToBoolean(object value) => !(value is DBNull) && Convert.ToBoolean(value);

        public static DateTime ToDateTime(object value) => value is DBNull ? DateTime.MinValue : Convert.ToDateTime(value);

        public List<Dictionary<string, object>> DataTableToMap(DataTable p_dt)
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

        public string SerializerJson(List<Dictionary<string, object>> a) => new JavaScriptSerializer().Serialize((object)a);

        public List<Dictionary<string, string>> Deserialize(string json) => new JavaScriptSerializer().Deserialize<List<Dictionary<string, string>>>(json);

        public bool SendMail(
          string[] pMails,
          string pSubject,
          string pBody,
          bool isBodyHtml,
          string[] attachments,
          out string messageError)
        {
            return this.SendMail(pMails, new string[0], new string[0], pSubject, pBody, isBodyHtml, attachments, out messageError);
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
            messageError = string.Empty;
            if (ConfigurationManager.AppSettings["EnviaMail"] == null || ConfigurationManager.AppSettings["EnviaMail"] == "0")
            {
                messageError = "NO esta configurado el sistema para envio de correos, favor de verificar.";
                return false;
            }
            if (!(ConfigurationManager.AppSettings["EnviaMail"].ToString() == "1"))
                return false;
            try
            {
                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["ServerSMTP"]);
                smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(ConfigurationManager.AppSettings["UserMail"].ToString(), ConfigurationManager.AppSettings["PasswordMail"].ToString());
                MailMessage message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"], ConfigurationManager.AppSettings["SenderName"]);
                string str = " <style type=\"text/css\"> body, p, table, div, ul, li  {font-family:\"Verdana\", \"Arial\"; font-weight:normal; font-size:12px; } </style> ";
                message.Subject = pSubject;
                message.IsBodyHtml = isBodyHtml;
                message.Body = str + pBody;
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
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                messageError = ex.Message;
                return false;
            }
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

        protected DataTable HtmlEncodeStrings(DataTable dt)
        {
            foreach (string name in dt.Columns.OfType<DataColumn>().Select<DataColumn, string>((Func<DataColumn, string>)(col => col.ColumnName)).ToList<string>())
            {
                string message = this.GetMessage(string.Format("{0}-{1}", (object)dt.TableName, (object)dt.Columns[name].ColumnName));
                dt.Columns[name].ColumnName = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(message) ? message : dt.Columns[name].ColumnName);
            }
            dt = this.HtmlEncodeStringsBody(dt);
            return dt;
        }

        protected DataTable HtmlEncodeStringsBody(DataTable dt)
        {
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                foreach (DataColumn column in (InternalDataCollectionBase)dt.Columns)
                {
                    if (column.DataType.Name.Equals("String"))
                        row[column] = (object)HttpUtility.HtmlEncode(row[column].ToString());
                }
            }
            return dt;
        }

        protected void Export(DataGrid dgGrid, string fileName)
        {
            this.Response.ClearContent();
            this.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            this.Response.ContentType = "application/excel";
            this.Response.Write("<style>.text {mso-number-format:\\@; } </style>");
            StringWriter writer1 = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter((TextWriter)writer1);
            this.Response.Clear();
            this.Response.Buffer = true;
            this.EnableViewState = false;
            dgGrid.RenderControl(writer2);
            this.Response.Output.Write(writer1.ToString());
            this.Response.Flush();
            this.Response.End();
        }

        private void DisableClientCaching()
        {
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
            this.Response.Cache.SetNoStore();
        }

        public static void WriteLineOnTxt(string message)
        {
            string str = DateTime.Now.ToString("ddMMyyyy");
            StreamWriter streamWriter = new StreamWriter(HostingEnvironment.MapPath("~/Log/") + str + ".txt", true);
            streamWriter.WriteLine(message);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public void IsRequestFromKiosco()
        {
            if (string.IsNullOrWhiteSpace(this.Request.QueryString["IsFromKiosco"]) || this.Request.Cookies["IsFromKiosco"] != null)
                return;
            this.Response.Cookies.Add(new HttpCookie("IsFromKiosco")
            {
                Value = "true",
                Expires = DateTime.Now.AddYears(1)
            });
        }

        public bool IsRedirectToKiosco(string nuevoToken)
        {
            if (this.Request.Cookies["IsFromKiosco"] == null || !(this.Request.Cookies["IsFromKiosco"].Value.ToString().ToLower() == "true"))
                return false;
            this.Response.Redirect(this.GetAppSetting("URLPortalCentralKiosco") + "?t=" + nuevoToken);
            return true;
        }

        public delegate void LanguageChanged(CultureInfo cultureInfo);

        public delegate void OnPageRefresh(EventArgs e);
    }
}
