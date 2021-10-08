using System;
using System.Collections.Generic;

namespace MyTimeTracker.Core
{
	public interface ITimeEntryRepository
	{
		IEnumerable<TimeEntry> GetTimeEntries(DateTime date);
		void Save(TimeEntry timeEntry);
		void Delete(TimeEntry timeEntry);
	}
}