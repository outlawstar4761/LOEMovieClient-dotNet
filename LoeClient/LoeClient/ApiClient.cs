using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using LOE;

namespace LOE
{
    public class ApiClient
    {
        static HttpClient client = new HttpClient();
        static string UriBase = "https://api.outlawdesigns.io:9669/";
        public AuthToken authToken;

        public ApiClient() {
            //client.BaseAddress = new Uri(UriBase);
        }

        static void PrepClient()
        {
            client.BaseAddress = new Uri(UriBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<AuthToken> Login(string Username, string Password)
        {
            PrepClient();
            string endPoint = client.BaseAddress + "authenticate/";
            client.DefaultRequestHeaders.Add("request_token", Username);
            client.DefaultRequestHeaders.Add("password", Password);
            AuthToken token = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadAsAsync<AuthToken>();
            }
            return token;
        }
        public static async Task<AuthToken> VerifyToken(string authToken) {
            string endPoint = client.BaseAddress + "verify/";
            client.DefaultRequestHeaders.Add("auth_token", authToken);
            AuthToken token = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode) {
                token = await response.Content.ReadAsAsync<AuthToken>();
            }
            return token;
        }
        public async Task<Movie> GetMovie(int UID)
        {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "movie/" + UID;
            Movie movie = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                movie = await response.Content.ReadAsAsync<Movie>();
            }
            return movie;
        }
        public async Task<Song> GetSong(int UID)
        {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "music/" + UID;
            Song song = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                song = await response.Content.ReadAsAsync<Song>();
            }
            return song;
        }
        public async Task<Episode> GetEpisode(int UID)
        {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "tv/" + UID;
            Episode episode = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                episode = await response.Content.ReadAsAsync<Episode>();
            }
            return episode;
        }
        public async Task<Song[]> GetRecentSongs(int limit)
        {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "music/recent/" + limit;
            Song[] songs = new Song[limit];
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                songs = await response.Content.ReadAsAsync<Song[]>();
            }
            return songs;
        }
        public async Task<Movie[]> GetRecentMovies(int limit)
        {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "movie/recent/" + limit;
            Movie[] movies = new Movie[limit];
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode) {
                movies = await response.Content.ReadAsAsync<Movie[]>();
            }
            return movies;
        }
        public async Task<Song[]> getAllSongs()
        {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "music/";
            Song[] songs = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                songs = await response.Content.ReadAsAsync<Song[]>();
            }
            return songs;
        }
        public async Task<Movie[]> getAllMovies()
        {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "movie/";
            Movie[] movies = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                movies = await response.Content.ReadAsAsync<Movie[]>();
            }
            return movies;
        }
        public async Task<Episode[]> getAllEpisodes()
        {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "tv/";
            Episode[] episodes = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                episodes = await response.Content.ReadAsAsync<Episode[]>();
            }
            return episodes;
        }
        public async Task<Movie[]> searchMovies(string key, string searchQuery) {
            client.DefaultRequestHeaders.Add("auth_token", this.authToken.token);
            string endPoint = client.BaseAddress + "movie/search/" + key + "/" + searchQuery;
            Movie[] movies = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode) {
                movies = await response.Content.ReadAsAsync<Movie[]>();
            }
            return movies;
        }
        public static string ConvertPath(string path) {
            return Regex.Replace(path,"/LOE/","http://loe.outlawdesigns.io/");
        }
        public static string DownloadCover(string cover_path,int UID) {
            string coverUri = ConvertPath(cover_path);
            string localPath = @"C:\temp\" + UID + ".jpg";
            using (WebClient webClient = new WebClient()) {
                webClient.DownloadFileAsync(new Uri(coverUri), @"C:\temp\" + UID + ".jpg");
            }
            return localPath;
        }
    }
}