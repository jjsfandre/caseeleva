using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace CaseEleva.Helpers
{
    public class Globalization
    {
        public string ResourceName;
        public CultureInfo CultureInfo;
        public Assembly Assembly;

        public Globalization(string resourceName, string cultureIndentifier, Assembly assembly = null)
        {
            this.Assembly = assembly;
            this.ResourceName = resourceName;
            this.CultureInfo = new CultureInfo(cultureIndentifier);
            if (this.CultureInfo == null)
                throw new Exception($"The {cultureIndentifier} it's not a valide culture!");
            if (this.CultureInfo.IsNeutralCulture)
                throw new Exception($"The {cultureIndentifier} it's as neutral culture!");
        }

        public string GetString(string id)
        {
            string error = $"[Undefined string '{id}' on language {this.CultureInfo.Name}]";
            ResourceManager manager = getResourceManager();
            if (manager != null)
            {
                try
                {
                    string str = manager.GetString(id);
                    return str != null ? str : error;
                }
                catch (Exception e)
                {
                    return error;
                }
            }
            else
                return error;
        }

        public Dictionary<string, string> GetAllStrings()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            ResourceManager manager = getResourceManager();
            foreach (DictionaryEntry entry in manager.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentCulture, true, true))
            {
                result.Add(entry.Key.ToString(), entry.Value.ToString());
            }
            return result;
        }

        #region Private
        private ResourceManager getResourceManager()
        {
            return new ResourceManager(
                ResourceName,
                (Assembly != null ? Assembly : Assembly.GetEntryAssembly())
            );
        }
        #endregion
    }
}