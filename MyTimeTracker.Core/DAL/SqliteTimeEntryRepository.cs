using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyTimeTracker.Core.DAL
{
	public class SqliteTimeEntryRepository : ITimeEntryRepository
	{
		public IEnumerable<TimeEntry> GetTimeEntries(DateTime date)
		{
			using var dbContext = new TimeTrackerSqliteContext();
			return dbContext.TimeEntries.Where(x => x.StartTime >= date && x.StartTime < date.AddDays(1)).ToList();
		}

		public void Save(TimeEntry timeEntry)
		{
			using var dbContext = new TimeTrackerSqliteContext();
			if (timeEntry.TimeEntryId == 0)
				dbContext.TimeEntries.Add(timeEntry);
			else
				dbContext.Entry(timeEntry).State = EntityState.Modified;
			dbContext.SaveChanges();
		}

		public void Delete(TimeEntry timeEntry)
		{
			using var db = new TimeTrackerSqliteContext();
			db.TimeEntries.Remove(timeEntry);
			db.SaveChanges();
		}
	}
}