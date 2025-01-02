
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace logic.Common.Resources
{
    public class CultureManager
    {
        private const string SESSION_CULTURE = "SESSION_CULTURE";
        public const string CULTURE_PARAM = "ealang";

        public static void Initialize() => CultureManager.StoreCulture(CultureManager.ResolveCulture());

        public static void StoreCulture(string culture)
        {
            if (!CultureManager.CultureExists(culture))
                throw new ArgumentException("Invalid data, unrecognized culture identifier " + culture);
            CultureManager.StoreCulture(CultureInfo.CreateSpecificCulture(culture));
        }

        public static void StoreCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture.Name);
            HttpSessionState session = HttpContext.Current.Session;
            if (session == null)
                return;
            session["SESSION_CULTURE"] = (object)culture;
            session["Idioma"] = (object)culture.TwoLetterISOLanguageName;
        }

        public static CultureInfo ResolveCulture()
        {
            string[] userLanguages = HttpContext.Current.Request.UserLanguages;
            if (userLanguages == null || userLanguages.Length == 0)
                return new CultureInfo(CultureManager.DefaultCulture());
            try
            {
                return CultureInfo.CreateSpecificCulture(userLanguages[0].ToLowerInvariant().Trim());
            }
            catch (ArgumentException ex)
            {
                return new CultureInfo(CultureManager.DefaultCulture());
            }
        }

        private static string DefaultCulture()
        {
            string str = "es-MX";
            return ConfigurationManager.AppSettings["CultureDefault"] == null ? str : ConfigurationManager.AppSettings["CultureDefault"];
        }

        public static bool CultureExists(string name) => !string.IsNullOrEmpty(name) && ((IEnumerable<CultureInfo>)CultureInfo.GetCultures(CultureTypes.AllCultures)).FirstOrDefault<CultureInfo>((Func<CultureInfo, bool>)(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))) != null;

        public static CultureInfo CreateSpecificCulture(string language)
        {
            try
            {
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException ex)
            {
                return new CultureInfo(CultureManager.DefaultCulture());
            }
        }
    }
}
