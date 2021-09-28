using System;

namespace MyTimeTracker.Core
{
	public record TimeEntry
	{
		public DateTime StartTime { get; set; }
		public DateTime StopTime  { get; set; }
		public Activity Activity  { get; set; }
		public string?  Comment   { get; set; }
		public bool     IsActive  => StartTime != default && StopTime == default;

		public void Save()
		{
			throw new NotImplementedException();
		}
	}
}