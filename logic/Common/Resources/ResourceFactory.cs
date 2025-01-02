using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace logic.Common.Resources
{
    public class ResourceFactory
    {
        private static Hashtable tablaResources = new Hashtable();

        private ResourceFactory()
        {
        }

        public static KeyValuePlainTextResource CreateResource(
          string resourceName,
          CultureInfo culture,
          bool isReinitilize)
        {
            string key = resourceName + "." + culture.TwoLetterISOLanguageName;
            if (ResourceFactory.tablaResources.ContainsKey((object)key) && !isReinitilize)
                return (KeyValuePlainTextResource)ResourceFactory.tablaResources[(object)key];
            ArrayList arrayList = new ArrayList();
            string str = key;
            if (!File.Exists(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, KeyValuePlainTextResource.PathHelper("Resources\\" + key + ".res.txt")))))
                str = resourceName;
            KeyValuePlainTextResource resource = new KeyValuePlainTextResource("Resources\\" + str + ".res.txt");
            ResourceFactory.tablaResources[(object)key] = (object)resource;
            return resource;
        }

        public static KeyValuePlainTextResource CreateResource(
          string resourceName,
          bool isReinitialize)
        {
            return ResourceFactory.CreateResource(resourceName, CultureInfo.CurrentCulture, isReinitialize);
        }

        public static KeyValuePlainTextResource CreateResource(
          string resourceName,
          bool isReinitialize,
          CultureInfo cultureInfo)
        {
            return ResourceFactory.CreateResource(resourceName, cultureInfo, isReinitialize);
        }
    }
}
