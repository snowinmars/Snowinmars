using System.Web.Mvc;
using System.Web.Routing;
using Snowinmars.BackgroundWorkers;

namespace Snowinmars.Ui
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			if (!BackgroundDaoWorker.WasStarted)
			{
				BackgroundDaoWorker.Start();
			}
		}
	}
}