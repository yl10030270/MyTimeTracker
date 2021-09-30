using MyTimeTracker.Core.DAL;

namespace MyTimeTracker.CL
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var timeTracker = new ConsoleTimeTracker(new SqliteTimeEntryRepository());
			timeTracker.Run();
		}
	}
}