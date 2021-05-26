using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Web;
using LOE;

namespace LoeClient
{
    public partial class HomeForm : Form
    {
        const string VLCDIR = @"C:\Program Files (x86)\VideoLAN\VLC";
        //const string VLCDIR = @"C:\SomethingBogus";
        const string VLCSITE = "https://www.videolan.org/vlc/download-windows.html";
        ApiClient apiClient;
        Movie[] recentMovies;

        public HomeForm(AuthToken authToken)
        {
            InitializeComponent();
            this.apiClient = new ApiClient();
            this.apiClient.authToken = authToken;
            this.GetRecentMovies();
            this.VerifyInstallation();
        }
        private void VerifyInstallation() {
            if (!Directory.Exists(VLCDIR)) {
                MessageBox.Show("Please Install VLC Media Player");
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = VLCSITE;
                process.Start();
            }
        }
        private async void GetRecentMovies() {
            this.recentMovies = await this.apiClient.GetRecentMovies(8);
            this.FillPictures();
        }
        private void FillPictures() {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.ImageLocation = ApiClient.ConvertPath(this.recentMovies[0].cover_path);
            pictureBox1.MouseClick += delegate (object sender, MouseEventArgs e) { OnMouseClick(sender, e, ApiClient.ConvertPath(this.recentMovies[0].file_path)); };
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.ImageLocation = ApiClient.ConvertPath(this.recentMovies[1].cover_path);
            pictureBox2.MouseClick += delegate (object sender, MouseEventArgs e) { OnMouseClick(sender, e, ApiClient.ConvertPath(this.recentMovies[1].file_path)); };
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.ImageLocation = ApiClient.ConvertPath(this.recentMovies[2].cover_path);
            pictureBox3.MouseClick += delegate (object sender, MouseEventArgs e) { OnMouseClick(sender, e, ApiClient.ConvertPath(this.recentMovies[2].file_path)); };
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.ImageLocation = ApiClient.ConvertPath(this.recentMovies[3].cover_path);
            pictureBox4.MouseClick += delegate (object sender, MouseEventArgs e) { OnMouseClick(sender, e, ApiClient.ConvertPath(this.recentMovies[3].file_path)); };
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.ImageLocation = ApiClient.ConvertPath(this.recentMovies[4].cover_path);
            pictureBox5.MouseClick += delegate (object sender, MouseEventArgs e) { OnMouseClick(sender, e, ApiClient.ConvertPath(this.recentMovies[4].file_path)); };
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.ImageLocation = ApiClient.ConvertPath(this.recentMovies[5].cover_path);
            pictureBox6.MouseClick += delegate (object sender, MouseEventArgs e) { OnMouseClick(sender, e, ApiClient.ConvertPath(this.recentMovies[5].file_path)); };
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.ImageLocation = ApiClient.ConvertPath(this.recentMovies[6].cover_path);
            pictureBox7.MouseClick += delegate (object sender, MouseEventArgs e) { OnMouseClick(sender, e, ApiClient.ConvertPath(this.recentMovies[6].file_path)); };
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.ImageLocation = ApiClient.ConvertPath(this.recentMovies[7].cover_path);
            pictureBox8.MouseClick += delegate (object sender, MouseEventArgs e) { OnMouseClick(sender, e, ApiClient.ConvertPath(this.recentMovies[7].file_path)); };
        }
        private void OnMouseClick(object sender, MouseEventArgs e, string videoUri) {
            this.LaunchVideo(Regex.Replace(videoUri, @"\s", "%20"));
        }
        private void LaunchVideo(string videoUri) {
            System.Diagnostics.Process vlc = new System.Diagnostics.Process();
            vlc.StartInfo.UseShellExecute = false;
            vlc.StartInfo.FileName = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";
            vlc.StartInfo.Arguments = videoUri;
            vlc.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm(this.apiClient.authToken);
            searchForm.Show();
        }
    }
}
