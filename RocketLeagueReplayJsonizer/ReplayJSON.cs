using RocketLeagueReplayParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeagueReplayJsonizer {
    class ReplayJSON {
        const string _version = "RLRJv1";

        public string JSONizer { get { return _version; } }
        public string ID { get; private set; }
        public string MatchType { get; private set; }
        public string MatchDate { get; private set; }
        public long? MatchLength { get; private set; }
        public string Stadium { get; private set; }
        public long? TeamSize { get; private set; }
        public List<Player> MembersTeamBlue { get; private set; }
        public List<Player> MembersTeamOrange { get; private set; }
        public long? ScoreTeamBlue { get; private set; }
        public long? ScoreTeamOrange { get; private set; }
        public List<Goal> Goals { get; private set; }

        public ReplayJSON(string path) {
            // Set Goals to 0
            ScoreTeamBlue = 0;
            ScoreTeamOrange = 0;

            // Start to deserialize
            string log = null;
            Replay replay = Replay.Deserialize(path, out log);
            bool isTeamBlue = true;

            foreach(var prop in replay.Properties) {
                Console.WriteLine(prop.Name + " ("+prop.Type+")");

                switch(prop.Name) {
                    case "PrimaryPlayerTeam":
                        isTeamBlue = false;
                        break;
                    case "Id":
                        ID = prop.StringValue;
                        break;
                    case "MatchType":
                        MatchType = prop.StringValue;
                        break;
                    case "Date":
                        MatchDate = prop.StringValue;
                        break;
                    case "NumFrames":
                        MatchLength = prop.IntValue;
                        break;
                    case "MapName":
                        Stadium = prop.StringValue;
                        break;
                    case "TeamSize":
                        TeamSize = prop.IntValue;
                        break;
                    case "Team0Score":
                        if (isTeamBlue)
                            ScoreTeamBlue = prop.IntValue;
                        else
                            ScoreTeamOrange = prop.IntValue;
                        break;
                    case "Team1Score":
                        if (isTeamBlue)
                            ScoreTeamOrange = prop.IntValue;
                        else
                            ScoreTeamBlue = prop.IntValue;
                        break;
                }
            }
        }
    }

    class Player {
        public string Name { get; private set; }
        public string Platform { get; private set; }
        public string OnlineID { get; private set; }
        public long? Score { get; private set; }
        public long? Goals { get; private set; }
        public long? Assists { get; private set; }
        public long? Saves { get; private set; }
        public long? Shots { get; private set; }
    }

    class Goal {
        public long? Time { get; private set; }
        public string Scorer { get; private set; }
    }
}
