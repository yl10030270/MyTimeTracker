using System;

namespace MyTimeTracker.Core
{
	public static class PunchClock
	{
		private static TimeCard? _timeCard;

		public static void LoadTimeCard(DateTime date)
		{
			_timeCard = new TimeCard(date);
			_timeCard.Load();
			if (_timeCard.HasActiveTimeEntry)
				CurrentTimeEntry = _timeCard.ActiveTimeEntry;
		}

		public static TimeEntry CurrentTimeEntry { get; private set; } = new();

		public static void Punch()
		{
			if (_timeCard is null)
				throw new InvalidOperationException("TimeCard Not Loaded.");

			if (CurrentTimeEntry.IsActive)
			{
				CurrentTimeEntry.StopTime = DateTime.Now;
				CurrentTimeEntry.Save();
				CurrentTimeEntry = new TimeEntry();
			}
			else
			{
				CurrentTimeEntry.StartTime = DateTime.Now;
				CurrentTimeEntry.Save();
				_timeCard.AddTimeEntry(CurrentTimeEntry);
			}
		}
	}
}