using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTimeTracker.Core
{
	public class TimeCard
	{
		private static ITimeEntryRepository? _repository;

		public TimeCard(DateTime date)
		{
			Date = date;
		}

		public DateTime         Date               { get; }
		public IList<TimeEntry> TimeEntries        { get; private set; } = new List<TimeEntry>();
		public bool             HasActiveTimeEntry => TimeEntries.Any() && TimeEntries[^1].IsActive;

		public TimeEntry ActiveTimeEntry => HasActiveTimeEntry
			? TimeEntries[^1]
			: throw new InvalidOperationException("Time Card doesn't have active time entry.");

		public static void SetTimeEntryRepository(ITimeEntryRepository repository)
		{
			_repository = repository;
		}

		public void AddTimeEntry(TimeEntry timeEntry)
		{
			TimeEntries.Add(timeEntry);
		}

		public void RemoveTimeEntry(long timeEntryId)
		{
			var timeEntryToRemove = TimeEntries.FirstOrDefault(x => x.TimeEntryId == timeEntryId);
			if (timeEntryToRemove != null)
			{
				timeEntryToRemove.Delete();
				TimeEntries.Remove(timeEntryToRemove);
			}

		}
		public void Load()
		{
			TimeEntries = _repository!.GetTimeEntries(Date).ToList();
		}

		public string ToPrettyString()
		{
			var result        = new StringBuilder();
			var totalDuration = TimeSpan.Zero;
			result.AppendLine($"Date: {Date.ToShortDateString()}");
			foreach (var timeEntry in TimeEntries.OrderByDescending(x => x.StartTime))
			{
				totalDuration += timeEntry.Duration;
				result.AppendLine(timeEntry.ToPrettyString());
			}
			result.AppendLine($"Total Duration: {new DateTime(totalDuration.Ticks):HH:mm:ss}");

			return result.ToString();
		}
	}
}