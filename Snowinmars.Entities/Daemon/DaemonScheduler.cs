using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snowinmars.Entities
{
	public class DaemonScheduler : IDaemon
	{
		private const int measurmentError = 5;
		private List<Daemon> daemons;
		private bool wasSentInThisHour;

		public DaemonScheduler()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; }

		public string Name { get; } = nameof(DaemonSettings);

		public DaemonSettings Settings { get; }

		public bool Exclude(string name) => Exclude(this.daemons.First(d => d.Name == name));

		public bool Exclude(Guid id) => Exclude(this.daemons.First(d => d.Id == id));

		public void Finit()
		{
		}

		public bool Include(Daemon daemon)
		{
			daemon.Init();

			this.daemons.Add(daemon);

			return true;
		}

		public void Init()
		{
		}

		/// <summary>
		/// Starts every 5 seconds
		/// </summary>
		public void Work()
		{
			var utcNow = this.FloorSeconds(DateTime.UtcNow);

			bool isCloseToMinuteStart = this.CheckIsCloseToMinuteStart(utcNow);

			if (isCloseToMinuteStart)
			{
				foreach (var daemon in this.daemons.Where(d => d.Settings.Type == DaemonSettingsType.Minutes))
				{
					ThreadPool.QueueUserWorkItem(_ => daemon.Work());
				}
			}

			bool isCloseToHourStart = this.CheckIsCloseToHourStart(utcNow);

			if (isCloseToHourStart && !this.wasSentInThisHour)
			{
				foreach (var daemon in this.daemons.Where(d => d.Settings.Type == DaemonSettingsType.Hours))
				{
					ThreadPool.QueueUserWorkItem(_ => daemon.Work());
				}

				this.wasSentInThisHour = true;
			}

			if (utcNow.Minute > DaemonScheduler.measurmentError + 1)
			{
				this.wasSentInThisHour = false;
			}
		}

		private bool CheckIsCloseToMinuteStart(DateTime utcNow)
		{
			return utcNow.Second < DaemonScheduler.measurmentError;
		}

		private bool CheckIsCloseToHourStart(DateTime utcNow)
		{
			return utcNow.Minute < DaemonScheduler.measurmentError;
		}

		private DateTime FloorSeconds(DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
		}

		private bool Exclude(Daemon daemon)
		{
			daemon.Finit();

			return this.daemons.Remove(daemon);
		}
	}
}