using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace WorkShop.pages
{
    public partial class GenerarPDF : System.Web.UI.Page
    {
        public static string _Conexion;
        public static string ConexionDB
        {
            get
            {
                if (GenerarPDF._Conexion == null)
                    GenerarPDF._Conexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();
                return GenerarPDF._Conexion;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           // logic_acces logicAcces = new logic_acces(GenerarPDF.ConexionDB, false);
            Dictionary<string, string> datos = new Dictionary<string, string>();
            if (this.IsPostBack)
                return;
            if (this.Request.QueryString["TypePDF"].ToString() == "1")
            {
                string empty = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.Request.QueryString["HistoryID"]))
                    empty = this.Request.QueryString["HistoryID"].ToString();
                datos.Add("StockHistoryID", empty);
                this.RegForm(datos);
            }
            else if (this.Request.QueryString["TypePDF"].ToString() == "2")
            {
                string empty = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.Request.QueryString["OrdenServicioID"]))
                    empty = this.Request.QueryString["OrdenServicioID"].ToString();
                datos.Add("OrdenServicioID", empty);
                this.Activities(datos);
            }
        }

        public void RegForm(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(GenerarPDF.ConexionDB, false);
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            DataTable table = logicAcces.ExecuteQuery("StockHistory_Get", datos).Tables[0];
            List<string> stringList = new List<string>();
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
            {
                string input = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/RegFrom.html"));
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (DataColumn column in (InternalDataCollectionBase)table.Columns)
                    dictionary.Add("_" + column.ColumnName, BasePage.ToString(row[column.ColumnName]));
                foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                {
                    string pattern = string.Format("\\b{0}\\b", (object)keyValuePair.Key);
                    input = Regex.Replace(input, pattern, string.IsNullOrEmpty(keyValuePair.Value) ? "&nbsp;" : keyValuePair.Value);
                }
                string str1 = Regex.Replace(input, "_UrlAPP_", string.IsNullOrEmpty(basePage.GetAppSetting("URL")) ? "../" : basePage.GetAppSetting("URL"));
                stringList.Add(str1);
                Rectangle pageSize = new Rectangle(612f, 792f);
                MemoryStream os = new MemoryStream();
                Document document = new Document(pageSize);
                document.SetMargins(40f, 40f, 15f, 15f);
                PdfWriter instance1 = PdfWriter.GetInstance(document, (Stream)os);
                instance1.CloseStream = false;
                XMLWorkerHelper instance2 = XMLWorkerHelper.GetInstance();
                document.Open();
                for (int index = 0; index < stringList.Count; ++index)
                {
                    if (index == 0)
                    {
                        StringReader inp = new StringReader(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(stringList[index])));
                        instance2.ParseXHtml(instance1, document, (TextReader)inp);
                    }
                    else
                    {
                        document.NewPage();
                        StringReader inp = new StringReader(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(stringList[index])));
                        instance2.ParseXHtml(instance1, document, (TextReader)inp);
                    }
                }
                document.Close();
                instance1.Close();
                string str2 = "StockNum_" + row["StockNum"].ToString() + "_Order_" + datos["StockHistoryID"];
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.BinaryWrite(os.ToArray());
                response.ContentType = "application/pdf";
                response.AddHeader("Content-Disposition", "attachment; filename=" + str2 + ".pdf");
                response.End();
            }
        }

        public void Activities(Dictionary<string, string> datos)
        {
            BasePage basePage = new BasePage();
            logic_acces logicAcces = new logic_acces(GenerarPDF.ConexionDB, false);
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            DataTable table = logicAcces.ExecuteQuery("ReporteCierreOrden_Sel", datos).Tables[0];
            List<string> stringList = new List<string>();
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
            {
                string input = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/CierreOrdenServicio.html"));
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (DataColumn column in (InternalDataCollectionBase)table.Columns)
                    dictionary.Add("_" + column.ColumnName, BasePage.ToString(row[column.ColumnName]));
                foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                {
                    string pattern = string.Format("\\b{0}\\b", (object)keyValuePair.Key);
                    input = Regex.Replace(input, pattern, string.IsNullOrEmpty(keyValuePair.Value) ? "&nbsp;" : keyValuePair.Value);
                }
                string str1 = Regex.Replace(input, "_UrlAPP_", string.IsNullOrEmpty(basePage.GetAppSetting("URL")) ? "../" : basePage.GetAppSetting("URL"));
                stringList.Add(str1);
                Rectangle pageSize = new Rectangle(612f, 792f);
                MemoryStream os = new MemoryStream();
                Document document = new Document(pageSize);
                document.SetMargins(40f, 40f, 15f, 15f);
                PdfWriter instance1 = PdfWriter.GetInstance(document, (Stream)os);
                instance1.CloseStream = false;
                XMLWorkerHelper instance2 = XMLWorkerHelper.GetInstance();
                document.Open();
                for (int index = 0; index < stringList.Count; ++index)
                {
                    if (index == 0)
                    {
                        StringReader inp = new StringReader(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(stringList[index])));
                        instance2.ParseXHtml(instance1, document, (TextReader)inp);
                    }
                    else
                    {
                        document.NewPage();
                        StringReader inp = new StringReader(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(stringList[index])));
                        instance2.ParseXHtml(instance1, document, (TextReader)inp);
                    }
                }
                document.Close();
                instance1.Close();
                string str2 = "aaa"; // "StockNum_" + row["StockNum"].ToString() + "_Order_" 
                if (this.Request.QueryString["NO"] != null)
                    str2 = this.Request.QueryString["NO"].ToString() + "_" + DateTime.Now.Second.ToString();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.BinaryWrite(os.ToArray());
                response.ContentType = "application/pdf";
                response.AddHeader("Content-Disposition", "attachment; filename=" + str2 + ".pdf");
                response.End();
            }
        }
    }
}