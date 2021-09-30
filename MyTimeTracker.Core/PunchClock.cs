using System;
using System.Diagnostics.CodeAnalysis;

namespace MyTimeTracker.Core
{
	public static class PunchClock
	{
		public const string COMMENT_NULL_ERROR_MESSAGE = "Comment is null.";

		public static TimeCard TimeCard { get; private set; } = null!;

		public static TimeEntry CurrentTimeEntry { get; private set; } = new();

		public static void LoadTimeCard(DateTime date)
		{
			TimeCard = new TimeCard(date);
			TimeCard.Load();
			if (TimeCard.HasActiveTimeEntry)
				CurrentTimeEntry = TimeCard.ActiveTimeEntry;
		}

		public static void Punch()
		{
			if (TimeCard is null)
				throw new InvalidOperationException("TimeCard Not Loaded.");

			if (CurrentTimeEntry.IsActive)
			{
				CurrentTimeEntry.StopTime = DateTime.Now;
				CurrentTimeEntry.Save();
				CurrentTimeEntry = new TimeEntry();
			}
			else
			{
				if (CurrentTimeEntry.Comment is null)
					throw new InvalidOperationException(COMMENT_NULL_ERROR_MESSAGE);
				CurrentTimeEntry.StartTime = DateTime.Now;
				CurrentTimeEntry.Save();
				TimeCard.AddTimeEntry(CurrentTimeEntry);
			}
		}
	}
}