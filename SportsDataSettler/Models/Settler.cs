using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsDataSettler.Models
{
    public class GameSettler
    {
        GameData game;
        public bool isComplete;
        public int homeAway;
        public int awayHome;
        public int totalGoals;
        public int hcapHome;
        public int hcapAway;
        public int mpHome;
        public int mpAway;
        /// <summary>
        /// Constructor for the game settler
        /// </summary>
        /// <param name="gameInstance">Must be the instance of GameData</param>
        public GameSettler(GameData gameInstance)
        {
            isComplete = gameInstance.abstractGameState == "Final";

            game = gameInstance;
            homeAway = game.home.score - game.away.score;
            awayHome = -homeAway;
            totalGoals = game.home.score + game.away.score;
            hcapHome = hcapCalc(homeAway);
            hcapAway = hcapCalc(awayHome);
            mpHome = miniPerformanceCalc(homeAway, game.home.score);
            mpAway = miniPerformanceCalc(awayHome, game.away.score);
        }//constructor

        private int hcapCalc(int goalDif)
        {
            if (goalDif > 0)
            {
                if (goalDif > 1) return 25;
                else return 10;
            }
            else return 0;
        }//hcapCalc

        private int miniPerformanceCalc(int goalDif, int teamScore)
        {
            int performance = 0;
            if (goalDif > 0)
                performance += 25;
            return teamScore * 5 + performance;
        }//miniPerformanceCalc

        public override string ToString() {
            string aStr;
            string formattedStr = String.Format(
                "Game Settlements:\r\n\r\n" +
                "{0} ({1}) (home)\r\n" +
                "{2} ({3}) (away)\r\n" +
                "Home/Away: {4}\r\n" +
                "Away/Home: {5}\r\n" +
                "Total Goals: {6}\r\n" +
                "-1.5 Handicap Home: {7}\r\n" +
                "-1.5 Handicap Away: {8}\r\n" +
                "Mini-performance Home: {9}\r\n" +
                "Mini-performance Away: {10}\r\n", 
                game.home.name,game.home.score,game.away.name,game.away.score,homeAway,awayHome,totalGoals,hcapHome,hcapAway,mpHome,mpAway);

            if (isComplete)
            {
                aStr = formattedStr;
            } else
            {
                aStr = "Game has not officially finished yet. \r\n" +
                       "Please confirm the match state before settling the match.\r\n"
                     + formattedStr;
            }

            return aStr;
        }
    }//GameSettler
    
    public class DailySettler
    {
        GameData[] games;

        public int homes;
        public int aways;
        public int homesAways;
        public int awaysHomes;
        public int totalGoals;
        public DateTime date;
        public bool isComplete;

        /// <summary>
        /// Constructor for the Daily Settler
        /// </summary>
        /// <param name="inDate">takes the date as a parameter to calculate which day is being settled</param>
        public DailySettler(DateTime inDate)
        {
            date = inDate;
            Matchup nhl = new Matchup(date);
            games = nhl.games;

            isComplete = true;
            foreach (GameData game in games)
            {
                if (game.abstractGameState != "Final")
                {
                    isComplete = false;
                    break;
                }
            }//for

            homes = homeTotal();
            aways = awayTotal();
            homesAways = homes - aways;
            awaysHomes = -homesAways;
            totalGoals = homes + aways;
        }//constructor

        private int homeTotal()
        {
            int sum = 0;
            foreach (GameData game in games)
            {
                sum += game.home.score;
            }
            return sum;
        }

        private int awayTotal()
        {
            int sum = 0;
            foreach (GameData game in games)
            {
                sum += game.away.score;
            }
            return sum;
        }

        public override string ToString()
        {
            string aStr;
            string formattedStr;
            formattedStr = String.Format(
                    "Daily Settlements:\r\n\r\n" +
                    "Date: {0:dddd, MMMM dd, yyyy}\r\n" +
                    "Homes/Aways: {1}\r\n" +
                    "Aways/Homes: {2}\r\n" +
                    "Total Goals: {3}\r\n",
                    date, homesAways, awaysHomes, totalGoals);
            if (isComplete)
            {
                aStr = formattedStr;
            } else
            {
                aStr = "Not all of the games are finished yet.\r\n" +
                        "Check to make sure that the final scores are correct\r\n" +
                        "before settling the dailies.\r\n"
                        +formattedStr;
            }
            return aStr;
        }

    }//DailySettler
}
