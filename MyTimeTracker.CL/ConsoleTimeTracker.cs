using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyTimeTracker.Core;
using MyTimeTracker.Core.Utility;

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
			PunchClock.LoadTimeCard(DateTime.Today);
			Console.WriteLine(PunchClock.TimeCard.ToPrettyString());
			while (true)
			{
				Console.Write(">");
				var input = Console.ReadLine()?.Split();
				try
				{
					if (input == null) continue;
					switch (input[0])
					{
						case "c":
						case "C":
							PunchClock.CurrentTimeEntry.Comment = input.Skip(1).JoinToString(null, " ");
							Console.WriteLine();
							Console.WriteLine(PunchClock.CurrentTimeEntry.ToPrettyString());
							Console.WriteLine();
							break;
						case "a":
						case "A":
							PunchClock.CurrentTimeEntry.Activity = Enum.Parse<Activity>(input[1]);
							Console.WriteLine();
							Console.WriteLine(PunchClock.CurrentTimeEntry.ToPrettyString());
							Console.WriteLine();
							break;
						case "p":
						case "P":
							PunchClock.Punch();
							Console.WriteLine();
							Console.WriteLine(PunchClock.CurrentTimeEntry.IsActive
								? PunchClock.CurrentTimeEntry.ToPrettyString()
								: PunchClock.TimeCard.ToPrettyString());
							Console.WriteLine();
							break;
						case "s":
						case "S":
							Console.WriteLine();
							if (input.Length > 1)
							{
								var viewDate = Convert.ToDateTime(input[1]);
								var timeCard = new TimeCard(viewDate);
								timeCard.Load();
								Console.WriteLine(timeCard.ToPrettyString());
							}
							else
							{
								Console.WriteLine(PunchClock.TimeCard.ToPrettyString());
							}
							Console.WriteLine();
							break;
						case "q":
						case "Q":
							return;
						default:
							ShowHelp();
							break;
					}
				}
				catch (InvalidOperationException e)
				{
					if (e.Message == PunchClock.COMMENT_NULL_ERROR_MESSAGE)
					{
						Console.WriteLine();
						Console.WriteLine(PunchClock.COMMENT_NULL_ERROR_MESSAGE);
						Console.WriteLine();
					}
					else
						ShowHelp();
				}
				catch
				{
					ShowHelp();
				}
				
			}
			
		}

		private void ShowHelp()
		{
			Console.WriteLine();
			Console.WriteLine("c/C comment: Set comment.");
			Console.WriteLine($"a/A activity: Set Activity. {ActivityEnumToString()}");
			Console.WriteLine("p/P : Punch.");
			Console.WriteLine("s/S : Show Time Card.");
			Console.WriteLine("q/Q : Quit.");
			Console.WriteLine();

			string ActivityEnumToString()
			{
				return Enum.GetValues(typeof(Activity)).Cast<Activity>().JoinToString(activity => $"{(int)activity} {activity}");
			}
		}

		
	}
}
