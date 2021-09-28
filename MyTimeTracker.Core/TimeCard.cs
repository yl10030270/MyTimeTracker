using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTimeTracker.Core
{
	public class TimeCard
	{
		public TimeCard(DateTime date)
		{
			Date = date;
		}

		public DateTime         Date               { get; }
		public IList<TimeEntry> TimeEntries        { get; } = new List<TimeEntry>();
		public bool             HasActiveTimeEntry => TimeEntries.Any() && TimeEntries[^1].IsActive;

		public TimeEntry ActiveTimeEntry => HasActiveTimeEntry
			? TimeEntries[^1]
			: throw new InvalidOperationException("Time Card doesn't have active time entry.");

		public void AddTimeEntry(TimeEntry timeEntry)
		{
			TimeEntries.Add(timeEntry);
		}

		public void Load()
		{
		}
	}
}