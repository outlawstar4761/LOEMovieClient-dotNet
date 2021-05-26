using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using LOE;

namespace LoeClient
{
    public partial class SearchForm : Form
    {
        ApiClient apiClient;
        Movie[] SearchResults;
        PictureBox[] resultBoxes;

        public SearchForm(AuthToken authToken)
        {
            InitializeComponent();
            this.apiClient = new ApiClient();
            this.apiClient.authToken = authToken;
            this.resultBoxes = new PictureBox[0];
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.ExecuteSearch();
        }
        private async void ExecuteSearch() {
            this.ClearOutput();
            string searchkey = SearchKeyBox.Text;
            if (searchkey == "Length") {
                searchkey = "run_time";
            } else if (searchkey == "Year") {
                searchkey = "relyear";
            }
            this.SearchResults = await this.apiClient.searchMovies(searchkey,SearchTextBox.Text);
            this.CreateOutput();
        }
        private void CreateOutput() {
            this.resultBoxes = new PictureBox[this.SearchResults.Length];
            int startX = 28;
            int startY = 93;
            for (int i = 0; i < this.SearchResults.Length; i++) {
                int j = i;
                this.resultBoxes[i] = new PictureBox
                {
                    Name = "result" + i,
                    Size = new Size(118,192),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ImageLocation = ApiClient.ConvertPath(this.SearchResults[i].cover_path),
                    Location = new Point(startX, startY),
                };
                this.resultBoxes[i].MouseClick += delegate (object sender, MouseEventArgs e) { this.OnMouseClick(sender, e, ApiClient.ConvertPath(this.SearchResults[j].file_path)); };
                this.Controls.Add(this.resultBoxes[i]);
                if (((i + 1) % 4) == 0)
                {
                    startX = 28;
                    startY += 234;
                }
                else {
                    startX += 216;
                }
            }
        }
        private void ClearOutput() {
            if (this.resultBoxes.Length != 0) {
                foreach (PictureBox resultBox in this.resultBoxes)
                {
                    this.Controls.Remove(resultBox);
                    resultBox.Dispose();
                }
            }
        }
        private void OnMouseClick(object sender, MouseEventArgs e, string videoUri)
        {
            this.LaunchVideo(Regex.Replace(videoUri, @"\s", "%20"));
        }
        private void LaunchVideo(string videoUri)
        {
            System.Diagnostics.Process vlc = new System.Diagnostics.Process();
            vlc.StartInfo.UseShellExecute = false;
            vlc.StartInfo.FileName = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";
            vlc.StartInfo.Arguments = videoUri;
            vlc.Start();
        }
    }
}
