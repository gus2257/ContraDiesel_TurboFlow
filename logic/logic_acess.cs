using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace logic
{
    public class logic_acces
    {
        private static BasePage Base = new BasePage();
        private SqlConnection sqlConection;
        private static string conexionString;

        public logic_acces(string strConn, bool esValidarSesion)
        {
            if (esValidarSesion && HttpContext.Current.Session["UserId"] == null)
                throw new Exception(logic_acces.Base.CommonResourceManager.GetMessage("msgSinSesion") == null ? " -999.- The session has expired requires re-login." : logic_acces.Base.CommonResourceManager.GetMessage("msgSinSesion"));
            this.sqlConection = new SqlConnection(strConn);
            logic_acces.conexionString = strConn;
        }

        public logic_acces(string strConn)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] == null)
                    throw new Exception(logic_acces.Base.CommonResourceManager.GetMessage("msgSinSesion") == null ? " -999.- The session has expired requires re-login." : logic_acces.Base.CommonResourceManager.GetMessage("msgSinSesion"));
                this.sqlConection = new SqlConnection(strConn);
                logic_acces.conexionString = strConn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public logic_acces(string strConn, string str)
        {
            if (HttpContext.Current.Session["UserId"] == null)
                throw new Exception("-999.- The session has expired requires re-login.");
            this.sqlConection = new SqlConnection(strConn);
            logic_acces.conexionString = strConn;
        }

        public DataSet ExecuteQuery(string p_procedure, Dictionary<string, string> p_datos)
        {
            string strTimeOffset = "0";
            if (HttpContext.Current.Session["TimeOffset"] != null)
                strTimeOffset = HttpContext.Current.Session["TimeOffset"].ToString();
            p_datos["TimeZoneOffSet"] = strTimeOffset;

            p_datos["UserId"] = HttpContext.Current.Session["UserId"] == null ? "1" : HttpContext.Current.Session["UserId"].ToString();

            //try {
            //    SqlConnection conn = new SqlConnection(logic_acces.conexionString);
            //    conn.Open();
            //}
            //catch (Exception ex)
            //{
            //    string ms = ex.Message;
            //}
            DataSet dataSet = new DataSet();
            BasePage basePage = new BasePage();
            using (SqlConnection connection = new SqlConnection(logic_acces.conexionString))
            {
                string empty = string.Empty;
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(p_procedure, connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(sqlCommand);
                    foreach (SqlParameter parameter in (DbParameterCollection)sqlCommand.Parameters)
                    {
                        if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
                        {
                            string str = parameter.ParameterName.Substring(3, parameter.ParameterName.Length - 3);
                            foreach (KeyValuePair<string, string> pDato in p_datos)
                            {
                                if (pDato.Key.ToString().ToLower() == str.ToLower())
                                {
                                    parameter.Value = (object)pDato.Value;
                                    break;
                                }
                            }
                            if (BasePage.ToString(parameter.Value) == "" && (parameter.SqlDbType == SqlDbType.Int || parameter.SqlDbType == SqlDbType.Float || parameter.SqlDbType == SqlDbType.Decimal))
                                parameter.Value = (object)null;
                            if (parameter.Value != null && parameter.SqlDbType.ToString().ToUpper() == "VARCHAR")
                                parameter.Value = (object)parameter.Value.ToString().ToUpper();
                            if (parameter.Value == null)
                            {
                                if (str.ToLower() == "nombrepcmod")
                                    parameter.Value = (object)basePage.NombrePcMod;
                                if (str.ToLower() == "idioma")
                                {
                                    parameter.Value = HttpContext.Current.Session["Idioma"] == null ? (object)"Spanish" : HttpContext.Current.Session["Idioma"];
                                    parameter.Value = (object)Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                                }
                                if (str.ToLower() == "ubicacionid")
                                    parameter.Value = HttpContext.Current.Session["ubicacionId"] == null ? (object)"0" : HttpContext.Current.Session["ubicacionId"];
                                if (str.ToLower() == "auditusuarioid")
                                    parameter.Value = HttpContext.Current.Session["UserId"] == null ? (object)0 : HttpContext.Current.Session["UserId"];
                            }
                        }
                    }
                    new SqlDataAdapter(sqlCommand).Fill(dataSet);
                    return dataSet;
                }

            }

        }

        public void ExecuteNonQuery(string nombreSP, Dictionary<string, object> parameters)
        {
            string strTimeOffset = "0";
            if (HttpContext.Current.Session["TimeOffset"] != null)
                strTimeOffset = HttpContext.Current.Session["TimeOffset"].ToString();
            parameters["TimeZoneOffSet"] = strTimeOffset;

            parameters["UserId"] = HttpContext.Current.Session["UserId"] == null ? "1" : HttpContext.Current.Session["UserId"].ToString();

            BasePage basePage = new BasePage();
            using (SqlConnection connection = new SqlConnection(logic_acces.conexionString))
            {
                string empty1 = string.Empty;
                connection.Open();
                using (SqlCommand command = new SqlCommand(nombreSP, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(command);
                    foreach (SqlParameter parameter1 in (DbParameterCollection)command.Parameters)
                    {
                        if (parameter1.Direction == ParameterDirection.Input || parameter1.Direction == ParameterDirection.InputOutput)
                        {
                            string str = parameter1.ParameterName.Substring(3, parameter1.ParameterName.Length - 3);
                            foreach (KeyValuePair<string, object> parameter2 in parameters)
                            {
                                if (parameter2.Key.ToString().ToLower() == str.ToLower())
                                {
                                    parameter1.Value = parameter2.Value;
                                    break;
                                }
                            }
                            if (BasePage.ToString(parameter1.Value) == "" && (parameter1.SqlDbType == SqlDbType.Int || parameter1.SqlDbType == SqlDbType.Float || parameter1.SqlDbType == SqlDbType.Decimal))
                                parameter1.Value = (object)null;
                            if (parameter1.Value != null && parameter1.SqlDbType.ToString().ToUpper() == "VARCHAR")
                                parameter1.Value = (object)parameter1.Value.ToString().ToUpper();
                            if (parameter1.Value == null)
                            {
                                if (str.ToLower() == "nombrepcmod")
                                    parameter1.Value = (object)basePage.NombrePcMod;
                                if (str.ToLower() == "idioma")
                                    parameter1.Value = HttpContext.Current.Session["Idioma"] == null ? (object)"Spanish" : HttpContext.Current.Session["Idioma"];
                                if (str.ToLower() == "ubicacionId")
                                    parameter1.Value = HttpContext.Current.Session["ubicacionId"] == null ? (object)"0" : HttpContext.Current.Session["ubicacionId"];
                                if (str.ToLower() == "auditusuarioid")
                                    parameter1.Value = HttpContext.Current.Session["UserId"] == null ? (object)0 : HttpContext.Current.Session["UserId"];
                            }
                        }
                    }
                    command.ExecuteNonQuery();
                    string empty2 = string.Empty;
                    foreach (SqlParameter parameter3 in (DbParameterCollection)command.Parameters)
                    {
                        if (parameter3.Direction == ParameterDirection.Output || parameter3.Direction == ParameterDirection.InputOutput)
                        {
                            string str = parameter3.ParameterName.Substring(3, parameter3.ParameterName.Length - 3);
                            foreach (KeyValuePair<string, object> parameter4 in parameters)
                            {
                                if (parameter4.Key.ToString().ToLower() == str.ToLower())
                                {
                                    empty2 = parameter4.Key.ToString();
                                    break;
                                }
                            }
                            parameters[empty2] = (object)parameter3.Value.ToString();
                        }
                    }
                }
            }
        }

        public void ExecuteNonQuerySimple(string nombreSP, Dictionary<string, string> parameters)
        {
            string strTimeOffset = "0";
            if (HttpContext.Current.Session["TimeOffset"] != null)
                strTimeOffset = HttpContext.Current.Session["TimeOffset"].ToString();
            parameters["TimeZoneOffSet"] = strTimeOffset;

            parameters["UserId"] = HttpContext.Current.Session["UserId"] == null ? "1" : HttpContext.Current.Session["UserId"].ToString();

            BasePage basePage = new BasePage();
            using (SqlConnection connection = new SqlConnection(logic_acces.conexionString))
            {
                string empty1 = string.Empty;
                connection.Open();
                using (SqlCommand command = new SqlCommand(nombreSP, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(command);
                    foreach (SqlParameter parameter1 in (DbParameterCollection)command.Parameters)
                    {
                        if (parameter1.Direction == ParameterDirection.Input || parameter1.Direction == ParameterDirection.InputOutput)
                        {
                            string str = parameter1.ParameterName.Substring(3, parameter1.ParameterName.Length - 3);
                            foreach (KeyValuePair<string, string> parameter2 in parameters)
                            {
                                if (parameter2.Key.ToString().ToLower() == str.ToLower())
                                {
                                    parameter1.Value = (object)parameter2.Value;
                                    break;
                                }
                            }
                            if (BasePage.ToString(parameter1.Value) == "" && (parameter1.SqlDbType == SqlDbType.Int || parameter1.SqlDbType == SqlDbType.Float || parameter1.SqlDbType == SqlDbType.Decimal))
                                parameter1.Value = (object)null;
                            if (parameter1.Value == null)
                            {
                                if (str.ToLower() == "nombrepcmod")
                                    parameter1.Value = (object)basePage.NombrePcMod;
                                if (str.ToLower() == "idioma")
                                    parameter1.Value = HttpContext.Current.Session["Idioma"] == null ? (object)"Spanish" : HttpContext.Current.Session["Idioma"];
                            }
                        }
                    }
                    command.ExecuteNonQuery();
                    string empty2 = string.Empty;
                    foreach (SqlParameter parameter3 in (DbParameterCollection)command.Parameters)
                    {
                        if (parameter3.Direction == ParameterDirection.Output || parameter3.Direction == ParameterDirection.InputOutput)
                        {
                            string str = parameter3.ParameterName.Substring(3, parameter3.ParameterName.Length - 3);
                            foreach (KeyValuePair<string, string> parameter4 in parameters)
                            {
                                if (parameter4.Key.ToString().ToLower() == str.ToLower())
                                {
                                    empty2 = parameter4.Key.ToString();
                                    break;
                                }
                            }
                            parameters[empty2] = parameter3.Value.ToString();
                        }
                    }
                }
            }
        }

        public void ExecuteNonQuery(string nombreSP, Dictionary<string, string> parameters)
        {
            string strTimeOffset = "0";
            if (HttpContext.Current.Session["TimeOffset"] != null)
                strTimeOffset = HttpContext.Current.Session["TimeOffset"].ToString();
            parameters["TimeZoneOffSet"] = strTimeOffset;


            parameters["UserId"] = HttpContext.Current.Session["UserId"] == null ? "1" : HttpContext.Current.Session["UserId"].ToString();

            BasePage basePage = new BasePage();
            using (SqlConnection connection = new SqlConnection(logic_acces.conexionString))
            {
                string empty1 = string.Empty;
                connection.Open();
                using (SqlCommand command = new SqlCommand(nombreSP, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(command);
                    foreach (SqlParameter parameter1 in (DbParameterCollection)command.Parameters)
                    {
                        if (parameter1.Direction == ParameterDirection.Input || parameter1.Direction == ParameterDirection.InputOutput)
                        {
                            string str = parameter1.ParameterName.Substring(3, parameter1.ParameterName.Length - 3);
                            foreach (KeyValuePair<string, string> parameter2 in parameters)
                            {
                                if (parameter2.Key.ToString().ToLower() == str.ToLower())
                                {
                                    parameter1.Value = (object)parameter2.Value;
                                    break;
                                }
                            }
                            if (BasePage.ToString(parameter1.Value) == "" && (parameter1.SqlDbType == SqlDbType.Int || parameter1.SqlDbType == SqlDbType.Float || parameter1.SqlDbType == SqlDbType.Decimal))
                                parameter1.Value = (object)null;
                            if (parameter1.Value != null && parameter1.SqlDbType.ToString().ToUpper() == "VARCHAR")
                                parameter1.Value = (object)parameter1.Value.ToString().ToUpper();
                            if (parameter1.Value == null)
                            {
                                if (str.ToLower() == "nombrepcmod")
                                    parameter1.Value = (object)basePage.NombrePcMod;
                                if (str.ToLower() == "idioma")
                                    parameter1.Value = HttpContext.Current.Session["Idioma"] == null ? (object)"Spanish" : HttpContext.Current.Session["Idioma"];
                           }
                        }
                    }
                    command.ExecuteNonQuery();
                    string empty2 = string.Empty;
                    foreach (SqlParameter parameter3 in (DbParameterCollection)command.Parameters)
                    {
                        if (parameter3.Direction == ParameterDirection.Output || parameter3.Direction == ParameterDirection.InputOutput)
                        {
                            string str = parameter3.ParameterName.Substring(3, parameter3.ParameterName.Length - 3);
                            foreach (KeyValuePair<string, string> parameter4 in parameters)
                            {
                                if (parameter4.Key.ToString().ToLower() == str.ToLower())
                                {
                                    empty2 = parameter4.Key.ToString();
                                    break;
                                }
                            }
                            parameters[empty2] = parameter3.Value.ToString();
                        }
                    }
                }
            }
        }

        public void ExecuteComandTextQuery(string comando)
        {
            BasePage basePage = new BasePage();
            using (SqlConnection sqlConnection = new SqlConnection(logic_acces.conexionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = comando;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public DataSet ExecuteComandTextNonQuery(string comando)
        {
            DataSet dataSet = new DataSet();
            BasePage basePage = new BasePage();
            using (SqlConnection sqlConnection = new SqlConnection(logic_acces.conexionString))
            {
                sqlConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand())
                {
                    selectCommand.CommandType = CommandType.Text;
                    selectCommand.CommandText = comando;
                    selectCommand.Connection = sqlConnection;
                    new SqlDataAdapter(selectCommand).Fill(dataSet);
                    return dataSet;
                }
            }
        }
    }
}
