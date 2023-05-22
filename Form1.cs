using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Net.NetworkInformation;
using System.IO;
using System.Text.RegularExpressions;
using Downloader;
using AngleSharp.Io;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.ComponentModel;
using DownloadProgressChangedEventArgs = Downloader.DownloadProgressChangedEventArgs;
using System.Threading;

namespace Tinfoil_Resource_Downloader
{
    public partial class bannerLabel : Form
    {
        public OBJData filteredAlt { get; private set; }
        public int Initial { get; private set; }
        public EventHandler<Downloader.DownloadProgressChangedEventArgs> OnDownloadProgressChanged { get; private set; }
        public EventHandler<AsyncCompletedEventArgs> OnDownloadFileCompleted { get; private set; }

        public bannerLabel()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void gameIdInputButton_Click(object sender, EventArgs e)
        {
            var ID = gameIdInput.Text;
            bool IDCheck = Regex.IsMatch(ID, "^0100[0-9A-Z]{3}0[0-9A-Z]{5}000");
            var url = "https://tinfoil.media/Title/ApiJson/";

            if (IDCheck == false)
            {
                Output.WriteLine("Failed ID Regex");
                MessageBox.Show("Inputted ID is not valid", "Error");
            }
            else
            {
                Output.WriteLine("Passed ID Regex");
                var checkedConn = true; //CheckForInternetConnection(url);
                if (checkedConn)
                {

                    var httpClientHandler = new HttpClientHandler();
                    var httpClient = new HttpClient(httpClientHandler);
                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var response = httpClient.GetAsync(url))
                    {

                        if (response.Status == TaskStatus.Faulted)
                        {
                            MessageBox.Show("Your device was unable to connect to: " + url, "Error");
                        }
                        else
                        {

                            Output.WriteLine("url " + url);
                            //Output.WriteLine("response " + response.Result.Content.ReadAsStringAsync().Result);

                            RootObject obj = JsonConvert.DeserializeObject<RootObject>(response.Result.Content.ReadAsStringAsync().Result);
                            //Output.WriteLine("data " + obj.Data);

                            Output.WriteLine("Finder " + JsonConvert.SerializeObject(obj.Data.Where(n => n.ID == ID).FirstOrDefault()));
                            Output.WriteLine("ID " + ID);


                            var filtered = obj.Data.Where(n => n.ID == ID).FirstOrDefault();
                            this.filteredAlt = filtered;
                            Output.WriteLine("filtered " + filtered);


                            LoadData(filtered);
                            await LoadImages(filtered);
                        }

                    }
                }
                else if (!checkedConn) MessageBox.Show("Your device was unable to connect to: " + url, "Error");
            }
        }

        private void LoadData(OBJData filtered)
        {
            var GameName = filtered.Name.Substring(0, filtered.Name.LastIndexOf("</a>"));
            var GameID = filtered.ID;
            var reggex = ("<a href=" + '"' + "/Title/" + GameID + '"' + ">");
            var GameNameFormatted = GameName.Replace(reggex, String.Empty);
            var GameNameFolderFormat = CharachterCleaner(GameNameFormatted);
            var GameRelease = filtered.ReleaseDate;
            var GamePub = filtered.Publisher;
            var GameSize = filtered.Size;
            var GameIcon = "https://tinfoil.media/ti/" + filtered.ID + "/200/200/";
            var GameBanner = "https://tinfoil.media/thi/" + filtered.ID + "/0/0/";
            //Output.WriteLine(GameIcon);

            gameNameLabel.Text = GameNameFormatted;
            Output.WriteLine("Game Name Formatted: " + GameNameFormatted);
            Output.WriteLine("Game Name Folder Formatted: " + GameNameFolderFormat);
            Output.WriteLine(reggex);
            gameIdLabel.Text = GameID;
            filtered.FormattedName = GameNameFormatted;
            filtered.FolderFormatName = GameNameFolderFormat;

        }

        private Task LoadImages(OBJData filtered)
        {
            //Clear All Old Images
            filtered.Screenshots.Clear();
            filtered.NewIcon = null;
            filtered.Banner = null;

            //Icon
            var iconRequest = WebRequest.Create("https://tinfoil.media/ti/" + filtered.ID + "/200/200/");

            using (var response = iconRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                gameIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                gameIcon.Image = Image.FromStream(stream);
            }
            filtered.NewIcon = "https://tinfoil.media/ti/" + filtered.ID + "/2000/2000/";

            //Banner
            var bannerRequest = WebRequest.Create("https://tinfoil.media/thi/" + filtered.ID + "/1920/1080/");

            using (var response = bannerRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                gameBanner.SizeMode = PictureBoxSizeMode.StretchImage;
                gameBanner.Image = Image.FromStream(stream);
            }
            filtered.Banner = "https://tinfoil.media/thi/" + filtered.ID + "/1920/1080/";

            //Screenshots
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://tinfoil.io/Title/" + filtered.ID);
            HtmlNodeCollection images = doc.DocumentNode.SelectNodes("//li/img");
            foreach (var image in images)
            {
                var link = image.Attributes["src"].Value;
                filtered.Screenshots.Add(link);
            }
            filtered.Screenshots.Distinct().ToList();

            Output.WriteLine(filtered.Screenshots.First());
            var ssRequest = WebRequest.Create(filtered.Screenshots.First());

            using (var response = ssRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                gameScreenshots.SizeMode = PictureBoxSizeMode.StretchImage;
                gameScreenshots.Image = Image.FromStream(stream);
            }

            Output.WriteLine(filtered.Screenshots.ToArray().ToString());

            return Task.CompletedTask;
        }

        private void NextImage(OBJData filtered)
        {
            if (Initial == (filtered.Screenshots.Count - 1)) Initial = 0;
            var newImage = filtered.Screenshots[Initial++];

            var ssRequest = WebRequest.Create(newImage);
            Output.WriteLine("New Image: " + newImage);

            using (var response = ssRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                gameScreenshots.SizeMode = PictureBoxSizeMode.StretchImage;
                gameScreenshots.Image = Image.FromStream(stream);
            }
        }

        private void gameScreenshots_Click(object sender, EventArgs e)
        {
            NextImage(filteredAlt);
            string combinedString = string.Join("\n", filteredAlt.Screenshots.ToArray());
            Output.WriteLine(filteredAlt.Screenshots.Count.ToString());
            Output.WriteLine(combinedString);
        }
        public static bool CheckForInternetConnection(string url, int timeout = 10000)
        {
                try
                {
                    Ping myPing = new Ping();
                    String host = url;
                    byte[] buffer = new byte[32];
                    PingOptions pingOptions = new PingOptions();
                    PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            //Directory Checks
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\Tinfoil Resource Downloader\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var dir2 = (dir + filteredAlt.FolderFormatName + @"\Icon"); //Icons
            if (!Directory.Exists(dir2))
            {
                Directory.CreateDirectory(dir2);
            }

            var dir3 = (dir + filteredAlt.FolderFormatName + @"\Banner"); //Banners
            if (!Directory.Exists(dir3))
            {
                Directory.CreateDirectory(dir3);
            }

            var dir4 = (dir + filteredAlt.FolderFormatName + @"\Screenshots"); //Screenshots
            if (!Directory.Exists(dir4))
            {
                Directory.CreateDirectory(dir4);
            }

            //Check if anything is selected first
            if(downloadType.SelectedItem == null)
            {
                MessageBox.Show("You need to select a download type first!", "Error");
                return;
            }


            //Download Icon
            switch (downloadType.SelectedItem)
            {
                case "Download All":
                    Output.WriteLine("Downloading All Started");
                    MessageBox.Show("Downloading All Resources...", "Download Info");
                    await SaveImageAsync(filteredAlt.NewIcon, (dir + filteredAlt.FolderFormatName + @"\Icon\Icon"), ".png");
                    await SaveImageAsync(filteredAlt.Banner, (dir + filteredAlt.FolderFormatName + @"\Banner\Banner"), ".png");
                    foreach (var picture in filteredAlt.Screenshots.Select((value, i) => new { i, value }))
                    {
                        await SaveImageAsync(picture.value, (dir + filteredAlt.FolderFormatName + @"\Screenshots\Screenshot-" + (picture.i + 1)), ".png");
                    }
                    Output.WriteLine("Downloaded All");
                    MessageBox.Show("Download Complete!", "Download Info");
                    break;
                case "Download Icon":
                    Output.WriteLine("Downloading Icon Started");
                    MessageBox.Show("Downloading Icon...", "Download Info"); 
                    await SaveImageAsync(filteredAlt.NewIcon, (dir + filteredAlt.FolderFormatName + @"\Icon\Icon"), ".png");
                    Output.WriteLine("Downloaded Icon");
                    MessageBox.Show("Download Complete!", "Download Info");
                    break;
                case "Download Banner":
                    Output.WriteLine("Downloading Banner Started");
                    MessageBox.Show("Downloading Banner...", "Download Info"); 
                    await SaveImageAsync(filteredAlt.Banner, (dir + filteredAlt.FolderFormatName + @"\Banner\Banner"), ".png");
                    Output.WriteLine("Downloaded Banner");
                    MessageBox.Show("Download Complete!", "Download Info");
                    break;
                case "Download Screenshots":
                    Output.WriteLine("Downloading Screenshots Started");
                    MessageBox.Show("Downloading All Screenshots...", "Download Info"); 
                    foreach (var picture in filteredAlt.Screenshots.Select((value, i) => new { i, value }))
                    {
                        await SaveImageAsync(picture.value, (dir + filteredAlt.FolderFormatName + @"\Screenshots\Screenshot-" + (picture.i + 1)), ".png");
                    }
                    Output.WriteLine("Downloaded Screenshots");
                    MessageBox.Show("Download Complete!", "Download Info");
                    break;
                default:
                    Output.WriteLine("Downloading All Started");
                    MessageBox.Show("Downloading All Resources...", "Download Info"); 
                    await SaveImageAsync(filteredAlt.NewIcon, (dir + filteredAlt.FolderFormatName + @"\Icon\Icon"), ".png");
                    await SaveImageAsync(filteredAlt.Banner, (dir + filteredAlt.FolderFormatName + @"\Banner\Banner"), ".png");
                    foreach (var picture in filteredAlt.Screenshots.Select((value, i) => new { i, value }))
                    {
                        await SaveImageAsync(picture.value, (dir + filteredAlt.FolderFormatName + @"\Screenshots\Screenshot-" + (picture.i + 1)), ".png");
                    }
                    Output.WriteLine("Downloaded All");
                    MessageBox.Show("Download Complete!", "Download Info");
                    break;
            }
        }

        public async Task SaveImageAsync(string imageUrl, string filename, string format)
        {
            var downloadOpt = new DownloadConfiguration()
            {
                // usually, hosts support max to 8000 bytes, default values is 8000
                BufferBlockSize = 10240,
                // file parts to download, default value is 1
                ChunkCount = 8,
                // the maximum number of times to fail
                MaxTryAgainOnFailover = 5,
                // timeout (millisecond) per stream block reader, default values is 1000
                Timeout = 1000,
                // clear package chunks data when download completed with failure, default value is false
                ClearPackageOnCompletionWithFailure = true,

            };
            var downloader = new DownloadService(downloadOpt);
            downloader.DownloadProgressChanged += ProgressChanged;
            downloader.DownloadFileCompleted += ProgressFinished;
            await downloader.DownloadFileTaskAsync(imageUrl, (filename + format));
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadBar.Value = (int)e.ProgressPercentage;
        }
        private void ProgressFinished(object sender, AsyncCompletedEventArgs e)
        {
            downloadBar.Value = 0;
        }

        public string CharachterCleaner(string input)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            var output = r.Replace(input, "");
            return output;
        }
    }
    public class OBJData
    {
        public List<string> Screenshots = new List<string>();
        public string ID { get; set; }
        public string Name { get; set; }
        public string FormattedName { get; set; }
        public string FolderFormatName { get; set; }
        public string NewIcon { get; set; }
        public string Banner { get; set; }
        public string ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Size { get; set; }

        public static explicit operator OBJData(EventArgs v)
        {
            throw new NotImplementedException();
        }
    }

    public class RootObject
    {
        public List<OBJData> Data { get; set; }
    }

}
