using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Asteroids
{
    public partial class MainPage : ContentPage
    {
        List<AsteroidInfo> asteroids;
        HttpClient htc = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
            Button_Clicked(null, null);
        }
        protected void GetAsteroidsList()
        {
           /* AsteroidInfo aa = new AsteroidInfo
            {
                Name = "asteroid",
                Magnitude = 10.2,
                IsDangerous = false
            };
            AsteroidInfo bb = new AsteroidInfo
            {
                Name = "sdfdfd",
                Magnitude = 7.12,
                IsDangerous = true
            };
            asteroids = new List<AsteroidInfo> { aa, bb };
            asteroidList.ItemsSource = asteroids; */
        } //metoda pro vyzkoušení funkčnosti buttonu, checkboxu a serializace dat json
        public async void GetData() //metoda pro braní dat z API
        {
            try
            {
                HttpClient client = new HttpClient();
                var result = await client.GetAsync("https://api.nasa.gov/neo/rest/v1/feed?start_date=2015-09-07&end_date=2015-09-08&api_key=DEMO_KEY"); //nevím proč, ale hlásí mi to chybný handshake
                result.EnsureSuccessStatusCode();
                string json = await result.Content.ReadAsStringAsync();
                asteroids = JsonConvert.DeserializeObject<List<AsteroidInfo>>(json);
                asteroidList.ItemsSource = asteroids;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            GetData();
            syncLabel.Text = "Last synced: " + DateTime.Now.ToString();
        }

        private void chbDangerous_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            List<AsteroidInfo> dangerousAsteroids = new List<AsteroidInfo>();
            if ((sender as CheckBox).IsChecked == true)
            {
                foreach (var item in asteroids)
                {
                    if (item.IsDangerous)
                        dangerousAsteroids.Add(item);
                }
                asteroidList.ItemsSource = dangerousAsteroids;
            }
            else
            {
                GetAsteroidsList();
            }
        }
    }
}
