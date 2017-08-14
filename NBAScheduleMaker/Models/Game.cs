using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAScheduleMaker.Models
{
	class Game
	{
		public DateTime GameTime
		{
			get;
			private set;
		}

		public Team HomeTeam
		{
			get;
			private set;
		}

		public Team AwayTeam
		{
			get;
			private set;
		}

		public Game(Team homeTeam, Team awayteam)
		{
			HomeTeam = homeTeam;
			AwayTeam = awayteam;
		}
	}
}
