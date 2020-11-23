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

namespace GoalsChallenge
{
    public partial class MainPage : ContentPage
    {
        public const string Url = "https://api.football-data.org/v2/competitions/PL/scorers";
        private List<Player> _players = new List<Player>();
        public MainPage()
        {
            
            InitializeComponent();
            GetData();

            listView.ItemsSource = _players;

        }

        async public void GetData()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Auth-Token", "54d5ac43b2e44aed8c081f8ba4b7d2ad");

            var content = await httpClient.GetStringAsync(Url);
            var data = JObject.Parse(content);
            var data2 = data["scorers"].ToList();
            foreach (JObject child in data2)
            {
                var name = child["player"]["name"].ToString();
                string fullName = Convert.ToString(name);

                _players.Add(new Player { Name = fullName });
            }

            listView.ItemsSource = _players;
        }
    }
}
