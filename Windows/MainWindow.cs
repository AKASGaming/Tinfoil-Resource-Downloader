using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public EventHandler<DownloadProgressChangedEventArgs> OnDownloadProgressChanged { get; private set; }
        public EventHandler<AsyncCompletedEventArgs> OnDownloadFileCompleted { get; private set; }
        public DownloadStatus UserProgress { get; set; }
        public Form DownloadWindow { get; set; }
        public bool running { get; set; }


        public Window2()
        {
            InitializeComponent();
            this.Text = this.Text + " - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            downloadType.Enabled = false;
            downloadButton.Enabled = false;
            gameScreenshots.Enabled = false;
        }

        private async void GameIdInputButton_Click(object sender, EventArgs e)
        {
            gameIdInputButton.Enabled = false;
            var ID = gameIdInput.Text;
            bool IDCheck = Regex.IsMatch(ID, "^0100[0-9A-Z]{3}0[0-9A-Z]{5}000");
            var apiURL = "https://tinfoil.media/Title/ApiJson/";

            if (IDCheck == false)
            {
                Output.WriteLine("Failed ID Regex");
                MessageBox.Show("Inputted ID is not valid", "Error");
                gameIdInputButton.Enabled = true;
            } else if (await ConnectionsCheck(ID))
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
                        downloadButton.Enabled = true;
                        downloadType.Enabled = true;
                        gameScreenshots.Enabled = true;
                        gameIdInputButton.Enabled = true;
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
            var GameRelease = filtered.release_date;
            var GamePub = filtered.Publisher;
            var GameSize = filtered.Size;
            var GameIcon = "https://tinfoil.media/ti/" + filtered.ID + "/0/0/";
            var GameBanner = "https://tinfoil.media/thi/" + filtered.ID + "/0/0/";
            var baseURL = "https://tinfoil.io/Title/";
            var GameURL = baseURL + GameID;
            //Output.WriteLine(GameIcon);

            gameNameLabel.Text = ("Game Name: " + GameNameFormatted);
            gameIdLabel.Text = ("Game ID: " + GameID);
            gameRelease.Text = ("Release Date: " + GameRelease);
            gameBan1.Text = (GameBanner);
            gameIco1.Text = (GameIcon);
            gameSize.Text = ("Game Size: " + GameSize);
            gamePublisher.Text = ("Publisher: " + GamePub);
            gameURL1.Text = (GameURL);

            Output.WriteLine("Game Name Formatted: " + GameNameFormatted);
            Output.WriteLine("Game Name Folder Formatted: " + GameNameFolderFormat);
            Output.WriteLine(reggex);
            Output.WriteLine(GameRelease);
            filtered.FormattedName = GameNameFormatted;
            filtered.FolderFormatName = GameNameFolderFormat;
            filtered.TinfoilSite = baseURL + GameID;

            //Downloaded Folder Check
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\Tinfoil Resource Downloader\" + filtered.FolderFormatName;
            if (!Directory.Exists(dir))
            {
                YN.Text = "No";
                YN.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                YN.Text = "Yes (Downloading again will overwrite data!)";
                YN.ForeColor = System.Drawing.Color.Green;
            }
        }

        private async Task LoadImages(OBJData filtered)
        {
            var imageURL = "https://tinfoil.media/ti/";
            var bannerURL = "https://tinfoil.media/thi/";
            var client = new HttpClient();

            //Clear All Old Images
            filtered.Screenshots.Clear();
            filtered.NewIcon = null;
            filtered.Banner = null;

            //Icon
            
            var iconRequest = await client.GetAsync(imageURL + filtered.ID + "/0/0/");

            using (var response = iconRequest.Content)
            using (var stream = await response.ReadAsStreamAsync())
            {
                gameIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                gameIcon.Image = Image.FromStream(stream);
            }
            filtered.NewIcon = imageURL + filtered.ID + "/0/0/";

            //Banner
            var bannerRequest = await client.GetAsync(bannerURL + filtered.ID + "/1920/1080/");

            using (var response = bannerRequest.Content)
            using (var stream = await response.ReadAsStreamAsync())
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
            var ssRequest = await client.GetAsync(filtered.Screenshots.First());

            using (var response = ssRequest.Content)
            using (var stream = await response.ReadAsStreamAsync())
            {
                gameScreenshots.SizeMode = PictureBoxSizeMode.StretchImage;
                gameScreenshots.Image = Image.FromStream(stream);
            }

            screenshotPosition.Text = "1 / " + (filtered.Screenshots.Count - 1);

            //Output.WriteLine(filtered.Screenshots.ToArray().ToString());
        }

        private async void NextImage(OBJData filtered)
        {
            if (Initial == (filtered.Screenshots.Count - 1)) Initial = 0;
            var newImage = filtered.Screenshots[Initial++];
            screenshotPosition.Text = Initial + " / " + (filtered.Screenshots.Count - 1);

            var client = new HttpClient();
            var ssRequest = await client.GetAsync(newImage);

            //Output.WriteLine("New Image: " + newImage);

            using (var response = ssRequest.Content)
            using (var stream = await response.ReadAsStreamAsync())
            {
                gameScreenshots.SizeMode = PictureBoxSizeMode.StretchImage;
                gameScreenshots.Image = Image.FromStream(stream);
            }
        }

        private void GameScreenshots_Click(object sender, EventArgs e)
        {
            NextImage(filteredAlt);
            //string combinedString = string.Join("\n", filteredAlt.Screenshots.ToArray());
            Output.WriteLine(filteredAlt.Screenshots.Count.ToString());
            //Output.WriteLine(combinedString);
        }
        public async Task<bool> CheckForInternetConnection(string url, int timeout = 10000)
        {
            var httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Head // Not GET, but HEAD
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
                MessageBox.Show("Your device was unable to connect to one of the APIs: \r\n" + apiURL + "\r\n\r\nIs your device connected to the internet? Is Tinfoil.io/Title down?", "Error");
                return false;
            } else if (checkedConn2 == false)
            {
                MessageBox.Show("Your device was unable to connect to one of the APIs: \r\n" + imageURL + "\r\n\r\nIs your device connected to the internet? Is Tinfoil.io/Title down?", "Error");
                return false;
            } else if (checkedConn3 == false)
            {
                MessageBox.Show("Your device was unable to connect to one of the APIs: \r\n" + bannerURL + "\r\n\r\nIs your device connected to the internet? Is Tinfoil.io/Title down?", "Error");
                return false;
            } else if (checkedConn4 == false)
            {
                MessageBox.Show("Your device was unable to connect to one of the APIs: \r\n" + mainURL + "\r\n\r\nIs your device connected to the internet? Is Tinfoil.io/Title down?", "Error");
                return false;
            }
            else if (checkedConn1 && checkedConn2 && checkedConn3 && checkedConn4 == false)
            {
                MessageBox.Show("Your device was unable to connect to one or more of the APIs: \r\n" + apiURL + "\r\n" + imageURL + "\r\n" + bannerURL + "\r\n" + mainURL + "\r\n\r\nIs your device connected to the internet? Is Tinfoil.io/Title down?", "Error");
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
                ReleaseDate = filteredAlt.release_date,
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
                ControlBox = false,
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
                    downloadStatus.UpdateStatus("Downloaded All Screenshots! Now Generating JSON File...");
                    downloadStatus.UpdateIcon("Screenshots", "Finished");

                    //JSON
                    downloadStatus.UpdateIcon("JSON", "Downloading");
                    downloadStatus.UpdateStatus("Generating \"" + (dir + filteredAlt.FolderFormatName + @"\" + filteredAlt.FolderFormatName + ".json") + "\"...");
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
                    downloadStatus.UpdateStatus("Generating \"" + (dir + filteredAlt.FolderFormatName + @"\" + filteredAlt.FolderFormatName + ".json") + "\"...");
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
        public string release_date { get; set; }
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
