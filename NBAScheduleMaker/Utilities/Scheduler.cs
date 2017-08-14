using NBAScheduleMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAScheduleMaker.Utilities
{
	class Scheduler
	{
		private List<TeamSchedule> _TeamsSchedules = new List<TeamSchedule>();

		Dictionary<int, Game> _AllGames = new Dictionary<int, Game>();

		Random rand = new Random();

		public List<TeamSchedule> CreateSchedule(List<Team> teams)
		{

			int gameNumber = 0;
			foreach (var team in teams)
			{
				_TeamsSchedules.Add(new TeamSchedule(team));
			}

			foreach (var teamschedule1 in _TeamsSchedules)
			{
				foreach (var teamschedule2 in _TeamsSchedules)
				{
					var relationship = teamschedule1.Team.GetRelationship(teamschedule2.Team);
					if (relationship == TeamRelationship.SameTeam)
					{
						// Don't scheudle games against yourself
						continue;
					}

					var teamTuple = Tuple.Create(teamschedule1.Team, teamschedule2.Team);

					// This scheduler will add 2 home games with each conference and division opponent 
					// and one home game for each non-conference opponent.
					// After adding all the games, each team will have more than 82 games and we will need to 
					// remove some games.  

					if (relationship == TeamRelationship.SameDivision || relationship == TeamRelationship.SameConference)
					{
						// Add two home games for team1 against team2 because they are in the same conference
						// we will remove excess games for non division conference opponents below
						Game homeGame1 = new Game(teamschedule1.Team, teamschedule2.Team);
						Game homeGame2 = new Game(teamschedule1.Team, teamschedule2.Team);

						_AllGames.Add(gameNumber, homeGame1);
						gameNumber++;
						_AllGames.Add(gameNumber, homeGame2);
						gameNumber++;
					}
					else if (relationship == TeamRelationship.DifferentConference)
					{
						// Add one home games against out of conference opponent
						Game homeGame1 = new Game(teamschedule1.Team, teamschedule2.Team);
						_AllGames.Add(gameNumber, homeGame1);
						gameNumber++;

					}
				}


			}

			// Add all games to team schedules
			foreach (var game in _AllGames)
			{
				var homeTeam = _TeamsSchedules.First(x => x.Team == game.Value.HomeTeam);
				homeTeam.Games.Add(game.Value);
				var awayTeam = _TeamsSchedules.First(x => x.Team == game.Value.AwayTeam);
				awayTeam.Games.Add(game.Value);
			}

			// each team wil have 86 games.  
			// We need to remove 2 home against non-division conference opponents
			foreach (var team in _TeamsSchedules)
			{
				RemoveExcessGames(team);
			}

			return _TeamsSchedules;
		}

		private void RemoveExcessGames(TeamSchedule teamSchedule)
		{
			// get potential games to remove 
			var gamesThatCanBeRemoved = new List<Game>();
			while (teamSchedule.Games.Count > 82)
			{
				gamesThatCanBeRemoved = new List<Game>();

				var homesGames = _AllGames.Where(x => x.Value.HomeTeam == teamSchedule.Team);
				foreach (var game in homesGames)
				{
					var currentAwayTeamSchedule = _TeamsSchedules.First(x => game.Value.AwayTeam == x.Team);
					bool canRemove = CanGameBeRemoved(teamSchedule, currentAwayTeamSchedule);
					if (canRemove)
					{
						gamesThatCanBeRemoved.Add(game.Value);
					}
				}

				if (gamesThatCanBeRemoved.Count == 0)
				{
					break;
				}

				var gameToRemove = gamesThatCanBeRemoved.ElementAt(rand.Next(gamesThatCanBeRemoved.Count()));
				var keyOfGamesToRemove = _AllGames.Keys.First(key => _AllGames[key] == gameToRemove);
				_AllGames.Remove(keyOfGamesToRemove);
				var homeTeamSchedule = _TeamsSchedules.First(x => x.Team == gameToRemove.HomeTeam);
				homeTeamSchedule.Games.Remove(gameToRemove);

				var awayTeamSchedule = _TeamsSchedules.First(x => x.Team == gameToRemove.AwayTeam);
				awayTeamSchedule.Games.Remove(gameToRemove);
			}


		}

		private bool CanGameBeRemoved(TeamSchedule homeTeamSchedule, TeamSchedule awayTeamSchedule)
		{
			if (homeTeamSchedule.Team.GetRelationship(awayTeamSchedule.Team) != TeamRelationship.SameConference)
			{
				return false;
			}

			if (homeTeamSchedule.Games.Count() == 82)
			{
				return false;
			}

			if (awayTeamSchedule.Games.Count() == 82)
			{
				return false;
			}

			int gamesCount = GetGamesCountAgainstEachOther(homeTeamSchedule.Team, awayTeamSchedule.Team);
			if (gamesCount < 4)
			{
				return false;
			}

			return true;
		}

		private int GetGamesCountAgainstEachOther(Team team1, Team team2)
		{
			return _AllGames.Where(x => (x.Value.HomeTeam == team1 && x.Value.AwayTeam == team2) ||
							(x.Value.HomeTeam == team2 && x.Value.AwayTeam == team1)).Count();
		}

		private DateTime _StartDate = new DateTime(2016, 10, 25);

		private DateTime _EndDate = new DateTime(2016, 4, 13);

		private List<DateTime> _InelgibleDates = new List<DateTime>
		{
			new DateTime(2017,2,17),
			new DateTime(2017,2,18),
			new DateTime(2017,2,19),
			new DateTime(2017,2,20),
			new DateTime(2017,2,21),
			new DateTime(2017,2,22)
		};
	}
}
