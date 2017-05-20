using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Snowinmars.Entities;

namespace Snowinmars.Ui.Controllers
{
    /// <summary>
    /// Module to append lang parameter to the requested url if it's absent or unsupported
    /// </summary>
    public class LangQueryAppenderModule : IHttpModule
    {
        private readonly IList<string> supportedLocales;

        /// <summary>
        /// We need to have controllers list to correctly handle situations
        /// when target method name is missed
        /// </summary>
        private readonly IList<string> controllersNamesList;

        public LangQueryAppenderModule()
        {
            this.supportedLocales = LocalizationHelper.GetSupportedLocales();
            this.controllersNamesList = new List<string>();
            this.FillControllersList();
        }

        private void FillControllersList()
        {
            string asmPath = HttpContext.Current.Server.MapPath("~/bin/Snowinmars.Ui.dll");
            Assembly asm = Assembly.LoadFile(asmPath);

            IEnumerable<Type> controllerTypes = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type));

            foreach (var controllerType in controllerTypes)
            {
                var fullName = controllerType.Name;

                // We need only name part of Controller class that is used in route
                this.controllersNamesList.Add(fullName.Substring(0, fullName.Length - "Controller".Length));
            }
        }

        // In the Init function, register for HttpApplication 
        // events by adding your handlers.
        public void Init(HttpApplication application)
        {
            application.BeginRequest += this.Application_BeginRequest;
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication) source;
            HttpContext ctx = app.Context;

            var a = HttpContext.Current.Request.Cookies["lang"];

            if (!ctx.Request.Url.AbsoluteUri.Contains($"/{a.Value.ToLowerInvariant()}/"))
            {

                // We will redirect to url with defined locale only in case for HTTP GET verb
                // cause we assume that all requests with other verbs will be called from site directly
                // where all the urls created with URLHelper, so it complies with routing rules and will contain "lang" parameter
                if (string.Equals(ctx.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    string localisedUri = LocalizationHelper.GetLocalisedUrl(ctx.Request.Url, this.controllersNamesList,
                        new[] {a.Value});

                    if (!string.IsNullOrEmpty(localisedUri))
                    {
                        ctx.Response.Redirect(localisedUri);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}