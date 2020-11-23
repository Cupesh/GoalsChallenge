using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GoalsChallenge
{
    public partial class MainPage : ContentPage
    {
        public const string Url = "https://raw.githubusercontent.com/openfootball/football.json/master/2017-18/en.1.json";
        private List<Match> _matches = new List<Match>();
        public MainPage()
        {
            InitializeComponent();
            GetData();
        }

        async public void GetData()
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(Url);

            JObject data = JObject.Parse(content);

            titleLabel.Text = data.SelectToken($"$.name").ToString();

            for (int i = 0; i < 38; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    string teamOne = data.SelectToken($"$..rounds[{i}].matches[{j}].team1").ToString();
                    string teamTwo = data.SelectToken($"$..rounds[{i}].matches[{j}].team2").ToString();
                    string score1 = data.SelectToken($"$..rounds[{i}].matches[{j}].score.ft[0]").ToString();
                    string score2 = data.SelectToken($"$..rounds[{i}].matches[{j}].score.ft[1]").ToString();
                    string score = $"{score1} : {score2}";

                    _matches.Add(new Match { TeamOne = teamOne, TeamTwo = teamTwo, Score = score });
                } 
            }

            listView.ItemsSource = _matches;
        }
    }
}
