using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device;
using System.Device.Location;

namespace NBAScheduleMaker.Models
{
	class Team
	{
		// ex: Utah Jazz
		public string FullName
		{
			get;
			private set;
		}

		// ex: Jazz
		public string TeamName
		{
			get;
			private set;
		}

		// ex: Utah
		public string LocationName
		{
			get;
			private set;
		}

		// ex: Salt Lake City
		public string LocationCity
		{
			get;
			private set;
		}

		// ex: UT
		public string LocationState
		{
			get;
			private set;
		}

		// 42.366303°N 71.062228°W
		public GeoCoordinate TeamGeoLocation
		{
			get;
			private set;
		}

		// ex: Northwest
		public Division TeamDivision
		{
			get;
			private set;
		}

		// ex: Western
		public Conference TeamConference
		{
			get;
			private set;
		}

		// ex: UTA
		public string Abbreviation
		{
			get;
			private set;
		}

		// ex: Vivint Smart Home Arena
		public string ArenaName
		{
			get;
			private set;	
		}

		public Team(string abbreviation, string fullName, string teamName, string locationName, string locationCity, string locationState, GeoCoordinate coordinates,
			Division div, Conference con, string areaName)
		{
			Abbreviation = abbreviation;
			FullName = fullName;
			TeamName = teamName;
			LocationCity = locationCity;
			LocationState = locationState;
			TeamGeoLocation = coordinates;
			TeamDivision = div;
			TeamConference = con;
			ArenaName = areaName;
			LocationName = locationName;
		}

		public TeamRelationship GetRelationship(Team otherTeam)
		{
			if (this == otherTeam)
			{
				return TeamRelationship.SameTeam;
			}
			else if (this.TeamDivision == otherTeam.TeamDivision)
			{
				return TeamRelationship.SameDivision;
			}
			else if (this.TeamConference == otherTeam.TeamConference)
			{
				return TeamRelationship.SameConference;
			}
			else
			{
				return TeamRelationship.DifferentConference;
			}
		}

		public static Tuple<Team, Team> GetTeamTuple(Team team1, Team team2)
		{
			if (string.Compare(team1.FullName, team2.FullName) < 0)
			{
				return new Tuple<Team, Team>(team1, team2);
			}
			else
			{
				return new Tuple<Team, Team>(team2, team1);
			}
		}


	}
}
