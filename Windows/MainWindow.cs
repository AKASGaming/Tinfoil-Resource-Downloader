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
using System.IO;
using System.Text.RegularExpressions;
using Downloader;
using System.ComponentModel;
using DownloadProgressChangedEventArgs = Downloader.DownloadProgressChangedEventArgs;

namespace Tinfoil_Resource_Downloader
{
    public partial class Window2 : Form
    {
        public OBJData filteredAlt { get; private set; }
        public int Initial { get; private set; }
        public EventHandler<Downloader.DownloadProgressChangedEventArgs> OnDownloadProgressChanged { get; private set; }
        public EventHandler<AsyncCompletedEventArgs> OnDownloadFileCompleted { get; private set; }
        public DownloadStatus UserProgress { get; set; }
        public Form DownloadWindow { get; set; }
        public bool running { get; set; }


        public Window2()
        {
            InitializeComponent();
            label3.Text = label3.Text + "   v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private async void GameIdInputButton_Click(object sender, EventArgs e)
        {
            var ID = gameIdInput.Text;
            bool IDCheck = Regex.IsMatch(ID, "^0100[0-9A-Z]{3}0[0-9A-Z]{5}000");
            var apiURL = "https://tinfoil.media/Title/ApiJson/";

            if (await ConnectionsCheck(ID))
            {
                if (IDCheck == false)
                {
                    Output.WriteLine("Failed ID Regex");
                    MessageBox.Show("Inputted ID is not valid", "Error");
                }
                else
                {
                    Output.WriteLine("Passed ID Regex");
                    var httpClientHandler = new HttpClientHandler();
                    var httpClient = new HttpClient(httpClientHandler);
                    httpClient.BaseAddress = new Uri(apiURL);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var response = httpClient.GetAsync(apiURL))
                    {
                        Output.WriteLine("url " + apiURL);
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
            else return;
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
            var baseURL = "https://tinfoil.io/Title/";
            //Output.WriteLine(GameIcon);

            gameNameLabel.Text = GameNameFormatted;
            Output.WriteLine("Game Name Formatted: " + GameNameFormatted);
            Output.WriteLine("Game Name Folder Formatted: " + GameNameFolderFormat);
            Output.WriteLine(reggex);
            gameIdLabel.Text = GameID;
            filtered.FormattedName = GameNameFormatted;
            filtered.FolderFormatName = GameNameFolderFormat;
            filtered.TinfoilSite = baseURL + GameID;

        }

        private Task LoadImages(OBJData filtered)
        {
            var imageURL = "https://tinfoil.media/ti/";
            var bannerURL = "https://tinfoil.media/thi/";

            //Clear All Old Images
            filtered.Screenshots.Clear();
            filtered.NewIcon = null;
            filtered.Banner = null;

            //Icon
            var iconRequest = WebRequest.Create(imageURL + filtered.ID + "/200/200/");

            using (var response = iconRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                gameIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                gameIcon.Image = Image.FromStream(stream);
            }
            filtered.NewIcon = imageURL + filtered.ID + "/2000/2000/";

            //Banner
            var bannerRequest = WebRequest.Create(bannerURL + filtered.ID + "/1920/1080/");

            using (var response = bannerRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                gameBanner.SizeMode = PictureBoxSizeMode.StretchImage;
                gameBanner.Image = Image.FromStream(stream);
            }
            filtered.Banner = bannerURL + filtered.ID + "/1920/1080/";

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

            screenshotPosition.Text = "1 / " + (filtered.Screenshots.Count - 1);

            Output.WriteLine(filtered.Screenshots.ToArray().ToString());

            return Task.CompletedTask;
        }

        private void NextImage(OBJData filtered)
        {
            if (Initial == (filtered.Screenshots.Count - 1)) Initial = 0;
            var newImage = filtered.Screenshots[Initial++];
            screenshotPosition.Text = Initial + " / " + (filtered.Screenshots.Count - 1);

            var ssRequest = WebRequest.Create(newImage);
            Output.WriteLine("New Image: " + newImage);

            using (var response = ssRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                gameScreenshots.SizeMode = PictureBoxSizeMode.StretchImage;
                gameScreenshots.Image = Image.FromStream(stream);
            }
        }

        private void GameScreenshots_Click(object sender, EventArgs e)
        {
            NextImage(filteredAlt);
            string combinedString = string.Join("\n", filteredAlt.Screenshots.ToArray());
            Output.WriteLine(filteredAlt.Screenshots.Count.ToString());
            Output.WriteLine(combinedString);
        }
        public async Task<bool> CheckForInternetConnection(string url, int timeout = 10000)
        {
            var httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = System.Net.Http.HttpMethod.Head // Not GET, but HEAD
            };
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var result = await httpClient.SendAsync(request);
            Output.WriteLine("Requesting Site: " + url + "\r\n Status: " + result.StatusCode);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> ConnectionsCheck(string ID)
        {
            var apiURL = "https://tinfoil.media/Title/ApiJson/";
            var mainURL = "https://tinfoil.io/Title/" + ID;
            var imageURL = "https://tinfoil.media/ti/" + ID + "/200/200/";
            var bannerURL = "https://tinfoil.media/thi/" + ID + "/1920/1080/";
            var checkedConn1 = await CheckForInternetConnection(apiURL);
            var checkedConn2 = await CheckForInternetConnection(imageURL);
            var checkedConn3 = await CheckForInternetConnection(bannerURL);
            var checkedConn4 = await CheckForInternetConnection(mainURL);

            if (checkedConn1 == false)
            {
                MessageBox.Show("Your device was unable to connect to one of the APIs: \r\n" + apiURL, "Error");
                return false;
            } else if (checkedConn2 == false)
            {
                MessageBox.Show("Your device was unable to connect to one of the APIs: \r\n" + imageURL, "Error");
                return false;
            } else if (checkedConn3 == false)
            {
                MessageBox.Show("Your device was unable to connect to one of the APIs: \r\n" + bannerURL, "Error");
                return false;
            } else if (checkedConn4 == false)
            {
                MessageBox.Show("Your device was unable to connect to one of the APIs: \r\n" + mainURL, "Error");
                return false;
            }
            else if (checkedConn1 && checkedConn2 && checkedConn3 && checkedConn4 == false)
            {
                MessageBox.Show("Your device was unable to connect to one or more of the APIs: \r\n" + apiURL + "\r\n" + imageURL + "\r\n" + bannerURL + "\r\n" + mainURL, "Error");
                return false;
            } else return true;
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            //Is Running Check
            if (running)
            {
                MessageBox.Show("You may not start another download until your current one is finished!", "Error");
                return;
            }

            //Directory Checks
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\Tinfoil Resource Downloader\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //Icons
            var dir2 = (dir + filteredAlt.FolderFormatName + @"\Icon");
            if (!Directory.Exists(dir2))
            {
                Directory.CreateDirectory(dir2);
            }

            //Banners
            var dir3 = (dir + filteredAlt.FolderFormatName + @"\Banner");
            if (!Directory.Exists(dir3))
            {
                Directory.CreateDirectory(dir3);
            }

            //Screenshots
            var dir4 = (dir + filteredAlt.FolderFormatName + @"\Screenshots");
            if (!Directory.Exists(dir4))
            {
                Directory.CreateDirectory(dir4);
            }

            var JSONOutput = new JSONOutput()
            {
                ID = filteredAlt.ID,
                Name = filteredAlt.Name,
                FormattedName = filteredAlt.FormattedName,
                FolderFormatName = filteredAlt.FolderFormatName,
                Size = filteredAlt.Size,
                Publisher = filteredAlt.Publisher,
                ReleaseDate = filteredAlt.ReleaseDate,
                Icon = filteredAlt.NewIcon,
                Banner = filteredAlt.Banner,
                Screenshots = filteredAlt.Screenshots,
                TinfoilSite = filteredAlt.TinfoilSite
            };
            string json = JsonConvert.SerializeObject(JSONOutput, Formatting.Indented);


            //Check if anything is selected first
            if (downloadType.SelectedItem == null)
            {
                MessageBox.Show("You need to select a download type first!", "Error");
                return;
            }

            Icon NewIcon = Icon.ExtractAssociatedIcon("./assets/Logo.ico");

            DownloadStatus downloadStatus = new DownloadStatus();

            Form window = new Form
            {
                Text = "Download Status",
                TopLevel = true,
                FormBorderStyle = FormBorderStyle.Fixed3D, //Disables user resizing
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = downloadStatus.Size, //size the form to fit the content
                Icon = NewIcon,
                StartPosition = FormStartPosition.CenterScreen
            };

            DownloadWindow = window;
            downloadStatus.SetWindow(window);
            downloadStatus.GetParentWindow(this);
            window.Controls.Add(downloadStatus);
            downloadStatus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            UserProgress = downloadStatus;

            switch (downloadType.SelectedItem)
            {
                case "Download All":
                    Output.WriteLine("Downloading All Started");
                    window.Show(this);
                    SetRunningState(true);
                    downloadStatus.UpdateStatus("Downloading All Resources...");

                    //Icon
                    downloadStatus.UpdateIcon("Icon", "Downloading");
                    await SaveImageAsync(filteredAlt.NewIcon, (dir + filteredAlt.FolderFormatName + @"\Icon\Icon"), ".png", downloadStatus);
                    downloadStatus.UpdateStatus("Downloaded Icon! Now Downloading Banner...");
                    downloadStatus.UpdateIcon("Icon", "Finished");

                    //Banner
                    downloadStatus.UpdateIcon("Banner", "Downloading");
                    await SaveImageAsync(filteredAlt.Banner, (dir + filteredAlt.FolderFormatName + @"\Banner\Banner"), ".png", downloadStatus);
                    downloadStatus.UpdateStatus("Downloaded Banner! Now Downloading Screenshots...");
                    downloadStatus.UpdateIcon("Banner", "Finished");

                    //Screenshots
                    downloadStatus.UpdateIcon("Screenshots", "Downloading");
                    foreach (var picture in filteredAlt.Screenshots.Select((value, i) => new { i, value }))
                    {
                        await SaveImageAsync(picture.value, (dir + filteredAlt.FolderFormatName + @"\Screenshots\Screenshot-" + (picture.i + 1)), ".png", downloadStatus);
                    }
                    downloadStatus.UpdateStatus("Downloaded All Screenshots! Now Downloading JSON File...");
                    downloadStatus.UpdateIcon("Screenshots", "Finished");

                    //JSON
                    downloadStatus.UpdateIcon("JSON", "Downloading");
                    File.WriteAllText((dir + filteredAlt.FolderFormatName + @"\" + filteredAlt.FolderFormatName + ".json"), json);
                    Output.WriteLine("Downloaded All");
                    downloadStatus.UpdateIcon("JSON", "Finished");
                    downloadStatus.UpdateStatus("All Resources Downloaded!");
                    downloadStatus.UpdateButton(true);
                    break;
                case "Download Icon":
                    window.Show(this);
                    SetRunningState(true);
                    Output.WriteLine("Downloading Icon Started");
                    downloadStatus.UpdateIcon("Icon", "Downloading");
                    await SaveImageAsync(filteredAlt.NewIcon, (dir + filteredAlt.FolderFormatName + @"\Icon\Icon"), ".png", downloadStatus);
                    Output.WriteLine("Downloaded Icon");
                    downloadStatus.UpdateStatus("Downloaded Icon!");
                    downloadStatus.UpdateIcon("Icon", "Finished");
                    downloadStatus.UpdateButton(true);
                    break;
                case "Download Banner":
                    window.Show(this);
                    SetRunningState(true);
                    Output.WriteLine("Downloading Banner Started");
                    downloadStatus.UpdateIcon("Banner", "Downloading");
                    await SaveImageAsync(filteredAlt.Banner, (dir + filteredAlt.FolderFormatName + @"\Banner\Banner"), ".png", downloadStatus);
                    Output.WriteLine("Downloaded Banner");
                    downloadStatus.UpdateStatus("Downloaded Banner!");
                    downloadStatus.UpdateIcon("Banner", "Finished");
                    downloadStatus.UpdateButton(true);
                    break;
                case "Download JSON File":
                    window.Show(this);
                    SetRunningState(true);
                    downloadStatus.UpdateStatus("Now Downloading JSON File...");
                    downloadStatus.UpdateIcon("JSON", "Downloading");
                    Output.WriteLine("Generating JSON File");
                    File.WriteAllText((dir + filteredAlt.FolderFormatName + @"\" + filteredAlt.FolderFormatName + ".json"), json);
                    Output.WriteLine("Downloaded JSON");
                    downloadStatus.UpdateStatus("Downloaded JSON!");
                    downloadStatus.UpdateIcon("JSON", "Finished");
                    downloadStatus.UpdateButton(true);
                    break;
                case "Download Screenshots":
                    window.Show(this);
                    SetRunningState(true);
                    Output.WriteLine("Downloading Screenshots Started");
                    downloadStatus.UpdateIcon("Screenshots", "Downloading");
                    foreach (var picture in filteredAlt.Screenshots.Select((value, i) => new { i, value }))
                    {
                        await SaveImageAsync(picture.value, (dir + filteredAlt.FolderFormatName + @"\Screenshots\Screenshot-" + (picture.i + 1)), ".png", downloadStatus);
                    }
                    Output.WriteLine("Downloaded Screenshots");
                    downloadStatus.UpdateStatus("Downloaded Screenshots!");
                    downloadStatus.UpdateIcon("Screenshots", "Finished");
                    downloadStatus.UpdateButton(true);
                    break;
            }
        }

        public async Task SaveImageAsync(string imageUrl, string filename, string format, DownloadStatus manager)
        {
            var downloadOpt = new DownloadConfiguration()
            {
                BufferBlockSize = 10240,
                ChunkCount = 8,
                MaxTryAgainOnFailover = 5,
                Timeout = 1000,
                ClearPackageOnCompletionWithFailure = true,
            };
            var downloader = new DownloadService(downloadOpt);
            downloader.DownloadProgressChanged += ProgressChanged;
            downloader.DownloadFileCompleted += ProgressFinished;

            manager.UpdateStatus("Downloading \"" + filename + format + "\"...");

            await downloader.DownloadFileTaskAsync(imageUrl, (filename + format));
        }

        private void gameIdInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GameIdInputButton_Click(this, new EventArgs());
            }
        }

        public void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            UserProgress.UpdateProgressBar((int)e.ProgressPercentage);
        }
        public void ProgressFinished(object sender, AsyncCompletedEventArgs e)
        {
            UserProgress.UpdateProgressBar(0);
        }

        public string CharachterCleaner(string input)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            var output = r.Replace(input, "");
            return output;
        }

        public void SetRunningState(bool state)
        {
            running = state;
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
        public string TinfoilSite { get; set; }

        public static explicit operator OBJData(EventArgs v)
        {
            throw new NotImplementedException();
        }
    }

    public class JSONOutput
    {
        public List<string> Screenshots = new List<string>();
        public string ID { get; set; }
        public string Name { get; set; }
        public string FormattedName { get; set; }
        public string FolderFormatName { get; set; }
        public string Icon { get; set; }
        public string Banner { get; set; }
        public string ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Size { get; set; }
        public string TinfoilSite { get; set; }

    }

    public class RootObject
    {
        public List<OBJData> Data { get; set; }
    }
}
