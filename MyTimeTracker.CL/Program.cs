using Microsoft.EntityFrameworkCore;
using MyTimeTracker.Core.DAL;

namespace MyTimeTracker.CL
{
	public static class Program
	{
		public static void Main(string[] args)
		{

#if DEBUG
			using (var db = new TimeTrackerSqliteContext())
			{
				db.Database.Migrate();
			}
#endif

			var timeTracker = new ConsoleTimeTracker(new SqliteTimeEntryRepository());
			timeTracker.Run();
		}
	}
}