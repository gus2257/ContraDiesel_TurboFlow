using System.Data;
using System.IO;

namespace logic.Class
{
    public static class DataTableExtensionMethods
    {
        public static string ToXml(this DataTable dt)
        {
            string xml;
            using (StringWriter writer = new StringWriter())
            {
                dt.WriteXml((TextWriter)writer);
                xml = writer.ToString();
            }
            return xml;
        }
    }
}
