using Quartz;
using Quartz.Impl;
using Snowinmars.Ui.AppStartHelpers;
using System;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Snowinmars.Ui.QuartzCron), "Start")]

namespace Snowinmars.Ui
{
    public class QuartzCron : IDisposable
    {
        private static IScheduler scheduler;

        internal static WarningJob WarningJob { get; set; }
        internal static ShortcutJob ShortcutJob { get; set; }

        public void Dispose()
        {
            QuartzCron.scheduler.Shutdown();
        }

        private static void Start()
        {
            try
            {
                QuartzCron.scheduler = StdSchedulerFactory.GetDefaultScheduler();

                QuartzCron.scheduler.Start();

                IJobDetail shortcutJob = JobBuilder.Create<ShortcutJob>()
                    .WithIdentity("shortcutJob", "basicGroup")
                    .Build();

                IJobDetail warningJob = JobBuilder.Create<WarningJob>()
                    .WithIdentity("warningJob", "basicGroup")
                    .Build();
                
                // Trigger the job to run now, and then repeat every 60 seconds
                ITrigger shortcutTrigger = TriggerBuilder.Create()
                    .WithIdentity("shortcutTrigger", "basicGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(60)
                        .RepeatForever())
                    .Build();

                ITrigger warningTrigger = TriggerBuilder.Create()
                   .WithIdentity("warningTrigger", "basicGroup")
                   .StartNow()
                   .WithSimpleSchedule(x => x
                       .WithIntervalInHours(24)
                       .RepeatForever())
                   .Build();

                QuartzCron.scheduler.ScheduleJob(shortcutJob, shortcutTrigger);
                QuartzCron.scheduler.ScheduleJob(warningJob, warningTrigger);
            }
            catch (SchedulerException e)
            {
                throw;
            }
        }
    }
}
