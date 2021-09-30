using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTimeTracker.Core;

namespace MyTimeTracker.CL
{
	public class ConsoleTimeTracker
	{

		public ConsoleTimeTracker(ITimeEntryRepository timeEntryRepository)
		{
			TimeEntry.SetTimeEntryRepository(timeEntryRepository);
			TimeCard.SetTimeEntryRepository(timeEntryRepository);
		}

		public void Run()
		{
			Console.WriteLine("Applicaiton Started");
		}
	}
}
