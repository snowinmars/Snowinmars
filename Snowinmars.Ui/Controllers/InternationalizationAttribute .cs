using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Snowinmars.Entities;

namespace Snowinmars.Ui.Controllers
{
    /// <summary>
    /// Set language that is defined in route parameter "lang"
    /// </summary>
    public class InternationalizationAttribute : ActionFilterAttribute
    {
        private readonly IList<string> supportedLocales;
        private readonly string defaultLang;

        public InternationalizationAttribute()
        {
            this.supportedLocales = LocalizationHelper.GetSupportedLocales();
            this.defaultLang = this.supportedLocales[0];
        }

        /// <summary>
        /// Apply locale to current thread
        /// </summary>
        /// <param name="lang">locale name</param>
        private void SetLang(string lang)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values["lang"] == null)
            {
                filterContext.RouteData.Values["lang"] = this.defaultLang;
            }

            string lang = (string) filterContext.RouteData.Values["lang"];

            if (!this.supportedLocales.Contains(lang))
            {
                throw new InvalidOperationException($"Can't recognize language {lang}");
            }

            SetLang(lang);
        }
    }
}