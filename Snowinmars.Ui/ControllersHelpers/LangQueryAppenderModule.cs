using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Snowinmars.Ui.Controllers
{
    /// <summary>
    /// Module to append lang parameter to the requested url if it's absent or unsupported
    /// </summary>
    public class LangQueryAppenderModule : IHttpModule
    {
        /// <summary>
        /// We need to have controllers list to correctly handle situations
        /// when target method name is missed
        /// </summary>
        private readonly IList<string> controllersNamesList;

        public LangQueryAppenderModule()
        {
            this.controllersNamesList = new List<string>();
            this.FillControllersList();
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += this.Application_BeginRequest;
        }

        private void Application_BeginRequest(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            string language = HttpContext.Current.Request.Cookies["lang"]?.Value?.ToLowerInvariant() ?? "en";
            Uri url = context.Request.Url;

            // if the url already contains language, that user setuped, system doesn't do anything.
            if (url.AbsoluteUri.Contains($"/{language}/"))
            {
                return;
            }

            // Otherwise we will redirect to url with defined locale only in case for HTTP GET verb
            // cause we assume that all requests with other verbs will be called from site directly
            // where all the urls created with URLHelper, so it complies with routing rules and will contain "lang" parameter
            if (string.Equals(context.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                string localisedUri = LocalizationHelper.GetLocalisedUrl(url, this.controllersNamesList, language);

                if (!string.IsNullOrEmpty(localisedUri))
                {
                    context.Response.Redirect(localisedUri);
                }
            }
        }

        private void FillControllersList()
        {
            string asmPath = HttpContext.Current.Server.MapPath("~/bin/Snowinmars.Ui.dll");
            Assembly assembly = Assembly.LoadFile(asmPath);

            IEnumerable<Type> controllerTypes = assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type));

            foreach (var controllerType in controllerTypes)
            {
                var fullName = controllerType.Name;

                // We need only name part of Controller class that is used in route
                this.controllersNamesList.Add(fullName.Substring(0, fullName.Length - "Controller".Length));
            }
        }
    }
}