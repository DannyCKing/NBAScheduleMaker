using NBAScheduleMaker.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAScheduleMaker.Utilities
{
	class ImportCSVFile
	{
		public static List<Team> ReadInTeamFile()
		{
			bool isFirstLine = true;
			List<Team> teams = new List<Team>();

			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string fullPath = "NBA_Teams.csv";
			using (var reader = new StreamReader(fullPath))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					if (isFirstLine)
					{
						isFirstLine = false;
						continue;
					}
					else
					{
						var values = line.Split(',');
						Team team = GetTeam(values);
						teams.Add(team);
					}
				}
			}
			return teams;
		}

		private static Team GetTeam(string[] values)
		{
			var fullTeamName = values[0];
			var city = values[1];
			var state = values[2];
			var arena = values[3];
			var coordinatesLatStr= values[4];
			var coordinatesLongStr = values[5];
			GeoCoordinate coordinates = new GeoCoordinate(double.Parse(coordinatesLatStr), -1 * double.Parse(coordinatesLongStr));
			var divisionStr = values[6];
			Division division = (Division)Enum.Parse(typeof(Division), divisionStr);
			var conferenceStr = values[7];
			Conference conference = (Conference)Enum.Parse(typeof(Conference), conferenceStr);
			var geoname = values[8];
			var teamname = values[9];
			var abbreviation = values[10];

			var team = new Team(abbreviation, fullTeamName, teamname, geoname, city, state, coordinates, division, conference, arena);
			return team;
		}
	}
}
