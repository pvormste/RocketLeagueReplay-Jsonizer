using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeagueReplayJsonizer {
    class ReplayJSON {
        const string _version = "RLRJv1";

        public string ID { get; private set; }
        public string MatchType { get; private set; }
        public string MatchDate { get; private set; }
        public string MatchLength { get; private set; }
        public string Stadium { get; private set; }
        public int TeamSize { get; private set; }
        public List<Player> MembersTeamBlue { get; private set; }
        public List<Player> MembersTeamOrange { get; private set; }
        public int ScoreTeamBlue { get; private set; }
        public int ScoreTeamOrange { get; private set; }
        public List<Goal> Goals { get; private set; }
    }

    class Player {
        public string Name { get; private set; }
        public string Platform { get; private set; }
        public string OnlineID { get; private set; }
        public int Score { get; private set; }
        public int Goals { get; private set; }
        public int Assists { get; private set; }
        public int Saves { get; private set; }
        public int Shots { get; private set; }
    }

    class Goal {
        public string Time { get; private set; }
        public string Scorer { get; private set; }
    }
}
