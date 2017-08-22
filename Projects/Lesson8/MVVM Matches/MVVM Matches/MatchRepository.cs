using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Matches
{
   public class MatchRepository : IMatchRepository
    {
        List<Match> _matches;

        public List<Match> Matches
        {
            get
            {
                return _matches;
            }
        }
        public MatchRepository()
        {
            _matches = new List<Match>()
            {
                new Match(){HomeGoals=2, AwayGoals =2, HomeTeam="Man Unt", AwayTeam = "Man city"},
                new Match(){HomeGoals=2, AwayGoals =2, HomeTeam="Liverpool", AwayTeam = "Everton"}

            };
        }
    }
}
