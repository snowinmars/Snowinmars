using System.Collections.Generic;
using Newtonsoft.Json;
using Snowinmars.Common;
using Snowinmars.Entities;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Snowinmars.Ui.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Snowinmars.Ui.App_Start.NinjectWebCommon), "Stop")]

namespace Snowinmars.Ui.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);

	        Snowinmars.Entities.DaemonScheduler daemonScheduler = new DaemonScheduler();
	        Snowinmars.Entities.Daemon daemon1 = new Daemon("ShortcutDaemon", DaemonSettingsType.Minutes);

	        int[] a = new int[60];

	        for (int i = 0; i < 60; i++)
	        {
		        a[i] = i;
	        }

			daemon1.Settings.Minutes.AddRange(a);

	        daemonScheduler.Include(daemon1);

	        var s = Newtonsoft.Json.JsonConvert.SerializeObject(daemonScheduler);
	        var o = Newtonsoft.Json.JsonConvert.DeserializeObject<DaemonScheduler>(s);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            NinjectRegistrator.Registrator.Register(kernel);
        }
    }
}