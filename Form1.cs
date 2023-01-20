using AngleSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace Tinfoil_Resource_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void gameIdInputButton_Click(object sender, EventArgs e)
        {
            var ID = gameIdInput.Text;
            var url = "https://tinfoil.media/Title/ApiJson/";


            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.BaseAddress = new Uri(url);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = httpClient.GetAsync(url))
            {
                Console.WriteLine("url " + url);
                //Console.WriteLine("response " + response.Result.Content.ReadAsStringAsync().Result);

                RootObject obj = JsonConvert.DeserializeObject<RootObject>(response.Result.Content.ReadAsStringAsync().Result);
                //Console.WriteLine("data " + obj.Data);

                Console.WriteLine("Finder " + JsonConvert.SerializeObject(obj.Data.Where(n => n.ID == ID).FirstOrDefault()));
                Console.WriteLine("ID " + ID);


                var filtered = obj.Data.Where(n => n.ID == ID).FirstOrDefault();
                Console.WriteLine("filtered " + filtered);


                await LoadData(filtered);
                await LoadImages(filtered);


            }
        }

        private async Task LoadData(OBJData filtered)
        {
            var GameName = filtered.Name.Substring(0, filtered.Name.LastIndexOf("</a>"));
            var GameID = filtered.ID;
            var GameRelease = filtered.ReleaseDate;
            var GamePub = filtered.Publisher;
            var GameSize = filtered.Size;
            var GameIcon = "https://tinfoil.media/ti/" + filtered.ID + "/200/200/";
            var GameBanner = "https://tinfoil.media/thi/" + filtered.ID + "/0/0/";
            //Console.WriteLine(GameIcon);

            var reggex = ("<a href=" + '"' + "/Title/" + GameID + '"' + ">");
            gameNameLabel.Text = GameName.Replace(reggex, String.Empty);
            Console.WriteLine(GameName.Replace(reggex, String.Empty));
            Console.WriteLine(reggex);
            gameIdLabel.Text = GameID;

        }

        private Task LoadImages(OBJData filtered)
        {
            var iconRequest = WebRequest.Create("https://tinfoil.media/ti/" + filtered.ID + "/200/200/");

            using (var response = iconRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                gameIcon.Image = Image.FromStream(stream);
            }

            return Task.CompletedTask;
        }

    }
    public class OBJData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Size { get; set; }
    }

    public class RootObject
    {
        public List<OBJData> Data { get; set; }
    }

}
