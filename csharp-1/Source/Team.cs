using System;
using System.Collections.Generic;
using System.Text;

namespace Source
{
    class Team
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string MainShirtColor { get; set; }
        public string SecondaryShirtColor { get; set; }

        private List<Player> _players;
        private List<Team> _teams;
        
        public Team(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            Id = id;
            Name = name;
            CreateDate = createDate;
            MainShirtColor = mainShirtColor;
            SecondaryShirtColor = secondaryShirtColor;
        }

        public void TeamAndPlayer(List<Team> teams, List<Player> players)
        {
            _teams = teams;
            _players = players;
        }
    }
}
