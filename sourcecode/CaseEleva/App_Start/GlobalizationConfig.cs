using CaseEleva.Helpers;
using CaseEleva.Models;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace CaseEleva.App_Start
{
    public class GlobalizationConfig
    {
        public static GlobalizationController Globalization;

        public static void RegisterGlobalizationController()
        {
            GlobalizationConfig.Globalization = GlobalizationController.GetInstance();
            GlobalizationConfig.Globalization.DefaultLanguage = "en-US";
            GlobalizationConfig.Globalization.AppendGlobalization("CaseEleva.Strings.en-US", "en-US", Assembly.GetExecutingAssembly());
            GlobalizationConfig.Globalization.AppendGlobalization("CaseEleva.Strings.pt-BR", "pt-BR", Assembly.GetExecutingAssembly());
            GlobalizationConfig.Globalization.OnGetCurrentLanguage += GetCurrentLanguage;

        }

        public static void SetThreadCurrentCulture()
        {
            Thread.CurrentThread.CurrentCulture = GlobalizationConfig.Globalization.GetCurrentGlobalization().CultureInfo;
        }

        public static string GetCurrentLanguage()
        {
            string language;
            var context = DBFactory.GetInstance().GetDb();
            language = context.Configuracao.SingleOrDefault().Idioma.Identificador;
            return language;
        }
    }
}