using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace logic.Common.Resources
{
    public class KeyValuePlainTextResource
    {
        private IList loadedResources = (IList)new ArrayList();
        private readonly Hashtable tablaResources = new Hashtable();
        private string resourceFile;

        public KeyValuePlainTextResource(string resourceFile)
        {
            this.resourceFile = resourceFile;
            this.LoadResource(this.DefaultLanguage);
        }

        public string ResourceFile
        {
            set => this.resourceFile = value;
        }

        public string DefaultLanguage => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        public string GetMessage(string key) => this.GetMessage(key, (CultureInfo)null, (string[])null);

        public string GetMessage(string key, CultureInfo cultureInfo) => this.GetMessage(key, cultureInfo, (string[])null);

        public string GetMessage(string key, string[] substitutions) => this.GetMessage(key, (CultureInfo)null, substitutions);

        public string GetMessage(string key, CultureInfo cultureInfo, string[] substitutions)
        {
            string str = cultureInfo != null ? cultureInfo.TwoLetterISOLanguageName : this.DefaultLanguage;
            if (!this.loadedResources.Contains((object)str))
                this.LoadResource(str);
            if (this.tablaResources[(object)str] == null)
                return (string)null;
            Hashtable tablaResource = (Hashtable)this.tablaResources[(object)str];
            if (!tablaResource.ContainsKey((object)key))
                return (string)null;
            string format = (string)tablaResource[(object)key];
            return substitutions == null || substitutions.Length == 0 ? format : string.Format(format, (object[])substitutions);
        }

        public DataTable GetResourcesValues()
        {
            DataTable resourcesValues = new DataTable();
            resourcesValues.Columns.Add("Key", typeof(string));
            resourcesValues.Columns.Add("Value", typeof(string));
            string empty = string.Empty;
            string fileName = this.GetFileName();
            if (string.IsNullOrEmpty(fileName))
                return resourcesValues;
            string key = string.IsNullOrEmpty(empty) ? this.BuildCultureCodeString(fileName) : empty;
            Hashtable hashtable;
            if (this.tablaResources.ContainsKey((object)key))
            {
                hashtable = (Hashtable)this.tablaResources[(object)key];
            }
            else
            {
                hashtable = new Hashtable();
                this.tablaResources.Add((object)key, (object)hashtable);
            }
            foreach (DictionaryEntry dictionaryEntry in hashtable)
            {
                DataRow row = resourcesValues.NewRow();
                row["Key"] = (object)dictionaryEntry.Key.ToString();
                row["Value"] = (object)dictionaryEntry.Value.ToString();
                resourcesValues.Rows.Add(row);
            }
            return resourcesValues;
        }

        private void LoadResource(string culture)
        {
            string fileName = this.GetFileName();
            if (string.IsNullOrEmpty(fileName))
                return;
            string key1 = string.IsNullOrEmpty(culture) ? this.BuildCultureCodeString(fileName) : culture;
            Hashtable hashtable;
            if (this.tablaResources.ContainsKey((object)key1))
            {
                hashtable = (Hashtable)this.tablaResources[(object)key1];
            }
            else
            {
                hashtable = new Hashtable();
                this.tablaResources.Add((object)key1, (object)hashtable);
            }
            using (StreamReader streamReader = new StreamReader(fileName, Encoding.Default))
            {
                string str1;
                while ((str1 = streamReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(str1))
                    {
                        string[] strArray = str1.Split('=');
                        string key2 = strArray[0].Trim();
                        string str2 = strArray[1].Trim();
                        hashtable.Add((object)key2, (object)str2);
                    }
                }
            }
            this.loadedResources.Add((object)culture);
        }

        private string BuildCultureCodeString(string filename)
        {
            string str = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(filename.ToLower(), "(.*)[/]", ""), "(.*)[\\\\]", ""), ".res.txt$", ""), "(.*)[(.)]", "");
            if (str.Length != 2)
                str = this.DefaultLanguage;
            return str;
        }

        private string GetFileName()
        {
            string fileName = (string)null;
            string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, KeyValuePlainTextResource.PathHelper(this.resourceFile)));
            if (File.Exists(fullPath))
                fileName = fullPath;
            return fileName;
        }

        internal static string PathHelper(string pathDirectory)
        {
            pathDirectory = pathDirectory.Replace("/\\", "/");
            pathDirectory = pathDirectory.Replace("\\/", "\\");
            pathDirectory = pathDirectory.Replace("//", "/");
            pathDirectory = pathDirectory.Replace("\\\\", "\\");
            pathDirectory = pathDirectory.Replace("/\\/", "/");
            pathDirectory = pathDirectory.Replace("\\/\\", "\\");
            return pathDirectory;
        }
    }
}
