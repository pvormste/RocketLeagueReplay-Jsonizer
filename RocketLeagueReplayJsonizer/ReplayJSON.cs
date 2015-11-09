using RocketLeagueReplayParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeagueReplayJsonizer {
    class ReplayJSON {
        const string _version = "RLRJv1";

        public string ReplayJsonType { get { return _version; } }
        public string ID { get; private set; }
        public string MatchType { get; private set; }
        public string MatchDate { get; private set; }
        public long? MatchLength { get; private set; }
        public float FramesPerSecond { get; private set; }
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

            // Initialize Lists
            MembersTeamBlue = new List<Player>();
            MembersTeamOrange = new List<Player>();
            Goals = new List<Goal>();

            // Start to deserialize
            string log = null;
            Replay replay = Replay.Deserialize(path, out log);
            bool isTeamOrange = false;

            foreach(var prop in replay.Properties) {
                // Debug:
                //Console.WriteLine(prop.Name + " ("+prop.Type+")");

                switch(prop.Name) {
                    case "PrimaryPlayerTeam":
                        isTeamOrange = true;
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
                    case "RecordFPS":
                        FramesPerSecond = prop.FloatValue;
                        break;
                    case "MapName":
                        Stadium = prop.StringValue;
                        break;
                    case "TeamSize":
                        TeamSize = prop.IntValue;
                        break;
                    case "Team0Score":
                        if (isTeamOrange)
                            ScoreTeamBlue = prop.IntValue;
                        else
                            ScoreTeamOrange = prop.IntValue;
                        break;
                    case "Team1Score":
                        if (isTeamOrange)
                            ScoreTeamOrange = prop.IntValue;
                        else
                            ScoreTeamBlue = prop.IntValue;
                        break;
                    case "PlayerStats":
                        
                        foreach(var propList in prop.ArrayValue) {
                            Player player = new Player();

                            foreach(var propElement in propList) {
                                switch(propElement.Name) {
                                    case "None":
                                        player = new Player();
                                        break;
                                    case "Name":
                                        player.Name = propElement.StringValue;
                                        break;
                                    case "Platform":
                                        player.Platform = propElement.StringValue.Split(' ')[1];
                                        break;
                                    case "OnlineID":
                                        player.OnlineID = propElement.IntValue;
                                        break;
                                    case "Team":
                                        if (propElement.IntValue == 0) {
                                            MembersTeamBlue.Add(player);
                                        }   
                                        else if (propElement.IntValue == 1) {
                                            MembersTeamOrange.Add(player);
                                        }
                                            
                                        break;
                                    case "Score":
                                        player.Score = propElement.IntValue;
                                        break;
                                    case "Goals":
                                        player.Goals = propElement.IntValue;
                                        break;
                                    case "Assists":
                                        player.Assists = propElement.IntValue;
                                        break;
                                    case "Saves":
                                        player.Saves = propElement.IntValue;
                                        break;
                                    case "Shots":
                                        player.Shots = propElement.IntValue;
                                        break;
                                    case "bBot":
                                        if (propElement.IntValue == 0)
                                            player.isBot = false;
                                        else
                                            player.isBot = true;
                                        break;

                                }
                            }
                        }

                        break;
                    case "Goals":
                        foreach (var propList in prop.ArrayValue) {
                            Goal goal = new Goal();

                            foreach (var propElement in propList) {
                                switch (propElement.Name) {
                                    case "None":
                                        Goals.Add(goal);
                                        goal = new Goal();
                                        break;
                                    case "frame":
                                        goal.FrameTime = propElement.IntValue;
                                        break;
                                    case "PlayerName":
                                        goal.Scorer = propElement.StringValue;
                                        break;
                                    case "PlayerTeam":
                                        if (propElement.IntValue == 0)
                                            goal.Team = "blue";
                                        else if (propElement.IntValue == 1)
                                            goal.Team = "orange";
                                        break;
                                }
                            }
                        }
                        break;
                }
            }
        }
    }

    class Player {
        public string Name { get; set; }
        public string Platform { get; set; }
        public long? OnlineID { get; set; }
        public long? Score { get; set; }
        public long? Goals { get; set; }
        public long? Assists { get; set; }
        public long? Saves { get; set; }
        public long? Shots { get; set; }
        public bool isBot { get; set; }
    }

    class Goal {
        public long? FrameTime { get; set; }
        public string Scorer { get; set; }
        public string Team { get; set; }
    }

}
