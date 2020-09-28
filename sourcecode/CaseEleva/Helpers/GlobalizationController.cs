using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CaseEleva.Helpers
{
    public class GlobalizationController
    {
        private static GlobalizationController _instance;

        public delegate string GlobalizationControllerGetCurrentLanguageHandle();

        public List<Globalization> Globalizations = new List<Globalization>();
        public string DefaultLanguage;
        public GlobalizationControllerGetCurrentLanguageHandle OnGetCurrentLanguage;

        private GlobalizationController()
        {
            this.DefaultLanguage = "en-US";
        }
        public static GlobalizationController GetInstance()
        {
            if (_instance == null)
                _instance = new GlobalizationController();
            return _instance;
        }

        public void AppendGlobalization(string resourceName, string language, Assembly assembly = null)
        {
            Globalizations.Add(new Globalization(resourceName, language, assembly));
        }

        public string GetString(string identifier)
        {
            Globalization globalization = getGlobalizationByLanguage(getLanguage());
            return globalization.GetString(identifier);
        }

        public Dictionary<string, string> GetAllStrings()
        {
            Globalization globalization = getGlobalizationByLanguage(getLanguage());
            return globalization.GetAllStrings();
        }

        public Globalization GetCurrentGlobalization()
        {
            return getGlobalizationByLanguage(getLanguage());
        }

        public string GetJavascriptConfig()
        {
            Globalization currentGlobalization = this.GetCurrentGlobalization();
            RegionInfo regionInfo = new RegionInfo(currentGlobalization.CultureInfo.LCID);

            return JsonConvert.SerializeObject(new
            {
                Strings = this.GetAllStrings(),
                Culture = new
                {
                    Name = currentGlobalization.CultureInfo.Name
                },
                Currency = new
                {
                    Symbol = GetCurrencySymbol(),
                    ISO = regionInfo.ISOCurrencySymbol
                },
                Number = new
                {
                    DecimalSeparator = currentGlobalization.CultureInfo.NumberFormat.NumberDecimalSeparator,
                    ThousandsSeparator = currentGlobalization.CultureInfo.NumberFormat.NumberGroupSeparator
                },
                DateTime = new
                {
                    DateFormat = "mm/dd/yyyy",
                    TimeFormat = "HH:mm:ss"
                }
            });
        }

        public string GetCurrencySymbol()
        {
            Globalization currentGlobalization = this.GetCurrentGlobalization();
            RegionInfo regionInfo = new RegionInfo(currentGlobalization.CultureInfo.LCID);
            return regionInfo.CurrencySymbol;
        }

        #region Private
        private string getLanguage()
        {
            string currentLanguage = OnGetCurrentLanguage?.Invoke();
            var language = this.DefaultLanguage;
            if (currentLanguage != null)
                language = currentLanguage;
            return language;
        }

        private Globalization getGlobalizationByLanguage(string language)
        {
            return Globalizations.Where(g => g.CultureInfo.Name.Equals(language)).FirstOrDefault();
        }
        #endregion
    }
}