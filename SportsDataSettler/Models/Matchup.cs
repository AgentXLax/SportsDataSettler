using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace SportsDataSettler.Models
{
    public class Matchup
    {
        public GameData[] games;
        public JObject jFile;
        public int totalGames;
        

        public Matchup(DateTime date)
        {
            jFile = GetNHLJson(date);
            games = GetNHLGames(date);
        }//constructor


        //eventually add date as a parameter
        public static JObject GetNHLJson(DateTime date)
        {
            string url;
            url = String.Format("https://statsapi.web.nhl.com/api/v1/schedule?date={0}",date.ToString("yyyy-MM-dd"));
            
            WebClient client = new WebClient();
            string jString = client.DownloadString(url);

            JObject jFile = JObject.Parse(jString);

            return jFile;
        }//nhlJsonInit


        private GameData[] GetNHLGames(DateTime date)//add date time eventually
        {   //create a new list for each game played on the day
            IList<GameData> games = new List<GameData>();
            GameData[] result;
            try
            {
                JObject jFile = GetNHLJson(date);
                IList<JToken> nhlGamesToday = jFile["dates"][0]["games"].Children().ToList();
                foreach (JToken game in nhlGamesToday)
                {
                    GameData gameOverview = game.ToObject<GameData>();
                    gameOverview.abstractGameState = game["status"]["abstractGameState"].ToString();
                    gameOverview.totalGames = jFile["totalGames"].ToObject<int>();

                    TeamData awayData = game["teams"]["away"].ToObject<TeamData>();
                    awayData.name = game["teams"]["away"]["team"]["name"].ToString();
                    gameOverview.away = awayData;

                    TeamData homeData = game["teams"]["home"].ToObject<TeamData>();
                    homeData.name = game["teams"]["home"]["team"]["name"].ToString();
                    gameOverview.home = homeData;

                    games.Add(gameOverview);

                }
                result = games.ToArray<GameData>();
            } catch { result = null; }
            return result;
        }//GetNHLGames


    }//class
    }//namespace

