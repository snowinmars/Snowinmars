using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Quartz;
using Quartz.Impl;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Snowinmars.Ui.QuartzConfig), "Start")]

namespace Snowinmars.Ui
{
    public class QuartzConfig : IDisposable
    {
        private static IScheduler scheduler;

        private static void Start()
        {
            try
            {
                QuartzConfig.scheduler = StdSchedulerFactory.GetDefaultScheduler();

                QuartzConfig.scheduler.Start();

                IJobDetail job = JobBuilder.Create<ShortcutJob>()
                    .WithIdentity("shortcutJob", "basicGroup")
                    .Build();

                // Trigger the job to run now, and then repeat every 60 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("shortcutTrigger", "basicGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(60)
                        .RepeatForever())
                    .Build();

                QuartzConfig.scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException e)
            {
                throw;
            }

        }

        public void Dispose()
        {
            QuartzConfig.scheduler.Shutdown();
        }
    }

    public class ShortcutJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
        }
    }

}