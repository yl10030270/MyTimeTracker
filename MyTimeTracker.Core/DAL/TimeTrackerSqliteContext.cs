using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 8618

namespace MyTimeTracker.Core.DAL
{
	public class TimeTrackerSqliteContext : DbContext
	{
		public DbSet<TimeEntry> TimeEntries { get; set; }

		public string DbPath { get; }

		public TimeTrackerSqliteContext()
		{
#if DEBUG
			DbPath =
				$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{Path.DirectorySeparatorChar}TimeTrackerDev.db";
#else
			DbPath =
				$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{Path.DirectorySeparatorChar}TimeTracker.db";
#endif
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source={DbPath}");
		}
	}
}