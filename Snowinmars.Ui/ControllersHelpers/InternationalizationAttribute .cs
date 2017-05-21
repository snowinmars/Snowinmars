using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Snowinmars.Ui.Controllers
{
    /// <summary>
    /// Set language that is defined in route parameter "lang"
    /// </summary>
    public class InternationalizationAttribute : ActionFilterAttribute
    {
        private readonly string defaultLang;
        private readonly IList<string> supportedLocales;

        public InternationalizationAttribute()
        {
            this.supportedLocales = LocalizationHelper.GetSupportedLocales();
            this.defaultLang = this.supportedLocales[0];
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            const string lang = "lang";

            if (filterContext.RouteData.Values[lang] == null)
            {
                filterContext.RouteData.Values[lang] = this.defaultLang;
            }

            string language = (string)filterContext.RouteData.Values[lang];

            if (!this.supportedLocales.Contains(language))
            {
                throw new InvalidOperationException($"Can't recognize language {language}");
            }

            this.SetLanguage(language);
        }

        /// <summary>
        /// Apply locale to current thread
        /// </summary>
        /// <param name="language">locale name</param>
        private void SetLanguage(string language)
        {
                CultureInfo culture;
            try
            {
                culture = CultureInfo.GetCultureInfo(language);
            }
            catch (CultureNotFoundException)
            {
                culture = CultureInfo.CurrentCulture;
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}