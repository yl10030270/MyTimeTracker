using System;
using System.Linq;

namespace MyTimeTracker.Core
{
	public record TimeEntry
	{
		private static ITimeEntryRepository? _repository;

		public int      TimeEntryId { get; set; }
		public DateTime StartTime   { get; set; }
		public DateTime StopTime    { get; set; }
		public Activity Activity    { get; set; }
		public string?  Comment     { get; set; }
		public bool     IsActive    => StartTime != default && StopTime == default;
		public TimeSpan Duration    => new[] {(StopTime - StartTime), TimeSpan.Zero }.Max();

		public static void SetTimeEntryRepository(ITimeEntryRepository repository)
		{
			_repository = repository;
		}

		public void Save()
		{
			_repository!.Save(this);
		}

		public string ToPrettyString()
		{
			return
				$"{(StartTime == default ? "null" : StartTime.ToShortTimeString())} -> {(StopTime == default ? "null" : StopTime.ToShortTimeString())} | {Duration} | {Activity} | {Comment}";
		}
	}
}