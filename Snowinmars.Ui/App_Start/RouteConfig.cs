using System.Web.Mvc;
using System.Web.Routing;

namespace Snowinmars.Ui
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var internationalizationRoute = routes.MapRoute(
				name: "DefaultInternationalization",
				url: "{lang}/{controller}/{action}/{id}",
				defaults: new
				{
				    lang = "en",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                }
			);
        }
	}
}