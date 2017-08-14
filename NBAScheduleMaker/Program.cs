using NBAScheduleMaker.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAScheduleMaker
{
	class Program
	{
		static void Main(string[] args)
		{
			var teams = ImportCSVFile.ReadInTeamFile();

			var schedule = new Scheduler().CreateSchedule(teams);

			//foreach (var game in schedule.First(x => x.Team.FullName == "Utah Jazz").Games)
			//{
			//	Console.WriteLine("{0} at {1}", game.AwayTeam.Abbreviation, game.HomeTeam.Abbreviation);
			//}

			var team = schedule.First(x => x.Team.FullName == "Utah Jazz");
			team.OutputSchedule();

			Console.ReadKey();
		}
	}
}
