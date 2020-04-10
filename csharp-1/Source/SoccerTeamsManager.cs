using System;
using System.Collections.Generic;
using Codenation.Challenge.Exceptions;
using Source;
using System.Linq;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private List<Team> _teams = new List<Team>();
        private List<Player> _players = new List<Player>();
        
        public SoccerTeamsManager()
        {
        }
        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            CheckIdTeamDeplicate(_teams, id);

            var time = new Team(id, name, createDate, mainShirtColor, secondaryShirtColor);
            _teams.Add(time);
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            CheckIdPlayerDeplicate(_players, id);
            CheckIdTeamNotFound(_teams, teamId);
            
            var player = new Player(id, teamId, name, birthDate, skillLevel, salary);
            _players.Add(player);
        }

        public void SetCaptain(long playerId)
        {
            CheckIdPlayerNotFound(_players, playerId);

            _players.Where(x => x.IsCaptain == true)
                    .ToList()
                    .ForEach(x => x.IsCaptain = false);

            _players.FirstOrDefault(x => x.Id == playerId).IsCaptain = true;
        }

        public long GetTeamCaptain(long teamId)
        {
            CheckIdTeamNotFound(_teams, teamId);
            var captain = _players.Where(x => x.TeamId == teamId)
                                  .FirstOrDefault(x => x.IsCaptain == true);

            if (captain == null)
                throw new CaptainNotFoundException();
            else
                return captain.TeamId;
        }

        public string GetPlayerName(long playerId)
        {
            CheckIdPlayerNotFound(_players, playerId);
            return _players.FirstOrDefault(x => x.Id == playerId).Name;
        }

        public string GetTeamName(long teamId)
        {
            CheckIdTeamNotFound(_teams, teamId);
            return _teams.FirstOrDefault(x => x.Id == teamId).Name;
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            CheckIdTeamNotFound(_teams, teamId);

            return _players.Where(x => x.TeamId == teamId)
                           .OrderBy(x => x.Id)
                           .Select(x => x.Id)
                           .ToList();
            
        }

        public long GetBestTeamPlayer(long teamId)
        {
            CheckIdTeamNotFound(_teams, teamId);
            
            return _players.Where(x => x.TeamId == teamId)
                           .OrderByDescending(x => x.SkillLevel)
                           .ThenBy(x => x.Id)
                           .Select(x => x.Id)
                           .FirstOrDefault();
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            CheckIdTeamNotFound(_teams, teamId);
            return _players.Where(x => x.TeamId == teamId)
                           .OrderBy(x => x.BirthDate)
                           .ThenBy(x => x.Id)
                           .Select(x => x.Id)
                           .FirstOrDefault();
        }

        public List<long> GetTeams()
        {
            return _teams.OrderBy(x => x.Id).Select(x => x.Id).ToList();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            CheckIdTeamNotFound(_teams, teamId);
            return _players.Where(x => x.TeamId == teamId)
                           .OrderByDescending(x => x.Salary)
                           .ThenBy(x => x.Id)
                           .Select(x => x.Id)
                           .FirstOrDefault();
        }

        public decimal GetPlayerSalary(long playerId)
        {
            CheckIdPlayerNotFound(_players, playerId);
            return _players.FirstOrDefault(x => x.Id == playerId).Salary;
        }

        public List<long> GetTopPlayers(int top)
        {
            return _players.OrderByDescending(x => x.SkillLevel)
                           .Select(x => x.Id)
                           .Take(top)
                           .ToList();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            CheckIdTeamNotFound(_teams, teamId);
            CheckIdTeamNotFound(_teams, visitorTeamId);

            string team = _teams.FirstOrDefault(x => x.Id == teamId).MainShirtColor;
            
            bool checkShirtColor = _teams.Where(x => x.Id == visitorTeamId)
                                         .Any(x => x.MainShirtColor == team);

            if (checkShirtColor)
                return _teams.FirstOrDefault(x => x.Id == visitorTeamId)
                             .SecondaryShirtColor;
            else
                return _teams.FirstOrDefault(x => x.Id == visitorTeamId)
                             .MainShirtColor;

        }

        private void CheckIdTeamDeplicate(List<Team> teams, long id)
        {
            bool checkId = teams.Any(x => x.Id == id);
            if (checkId)
                throw new UniqueIdentifierException();
        }
        private void CheckIdPlayerDeplicate(List<Player> players, long id)
        {
            bool checkId = players.Any(x => x.Id == id);
            if (checkId)
                throw new UniqueIdentifierException();
        }
        private void CheckIdTeamNotFound(List<Team> teams, long id)
        {
            bool checkId = teams.Any(x => x.Id == id);
            if (!checkId)
                throw new TeamNotFoundException();
        }
        private void CheckIdPlayerNotFound(List<Player> players, long id)
        {
            bool checkId = players.Any(x => x.Id == id);
            if (!checkId)
                throw new PlayerNotFoundException();
        }
    }
}
