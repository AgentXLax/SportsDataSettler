using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SportsDataSettler.Models
{

    public class GameData
    {
        public int totalGames;
        public string gamePk;//game id
        public string gameDate;
        public string abstractGameState;//found in status. need to check if the game is over
        public TeamData home;
        public TeamData away;

        override public string ToString()
        {
            string aStr = String.Format("{2} - {0} vs. {1}",
                home.name,
                away.name,
                abstractGameState);

            return aStr;
        }//ToString

    }//GameData

    public class TeamData {
        public int score;//team score
        public string name;//team name
    }//TeamData

}//namespace
