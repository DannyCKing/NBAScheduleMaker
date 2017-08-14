using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAScheduleMaker.Models
{
	class TeamSchedule
	{
		public List<Game> Games
		{
			get;
			private set;
		}

		public bool IsScheduleFull
		{
			get
			{
				return Games.Count >= 82;
			}
		}

		public Team Team
		{
			get;
			private set;
		}

		public TeamSchedule(Team team)
		{
			Games = new List<Game>();
			Team = team;
		}

		public void OutputSchedule()
		{
			Console.WriteLine(string.Format("{0} Total Games", Games.Count));
			var gamesAgainstOtherConference = Games.Where(game => game.HomeTeam.GetRelationship(game.AwayTeam) == TeamRelationship.DifferentConference);
			Console.WriteLine(string.Format("VS Opposing Conference - {0} games", gamesAgainstOtherConference.Count()));
			foreach (var game in gamesAgainstOtherConference)
			{
				Console.WriteLine(string.Format("{0} AT {1}", game.AwayTeam.Abbreviation, game.HomeTeam.Abbreviation));
			}

			Console.WriteLine();

			var gamesAgainstOtherdivision = Games.Where(game => game.HomeTeam.GetRelationship(game.AwayTeam) == TeamRelationship.SameConference);
			Console.WriteLine(string.Format("VS Other Division In Conference - {0} games", gamesAgainstOtherdivision.Count()));
			foreach (var game in gamesAgainstOtherdivision)
			{
				Console.WriteLine(string.Format("{0} AT {1}", game.AwayTeam.Abbreviation, game.HomeTeam.Abbreviation));
			}

			Console.WriteLine();

			var gamesAgainstSameDivision = Games.Where(game => game.HomeTeam.GetRelationship(game.AwayTeam) == TeamRelationship.SameDivision);
			Console.WriteLine(string.Format("VS Division Opponenets - {0} games", gamesAgainstSameDivision.Count()));
			foreach (var game in gamesAgainstSameDivision)
			{
				Console.WriteLine(string.Format("{0} AT {1}", game.AwayTeam.Abbreviation, game.HomeTeam.Abbreviation));
			}
		}
	}
}
