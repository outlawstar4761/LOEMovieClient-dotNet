using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using LOE;

namespace LOE
{
    public class ApiClient
    {
        static HttpClient client = new HttpClient();
        static string UriBase = "http://api.outlawdesigns.io/LOE/";

        static void PrepClient()
        {
            client.BaseAddress = new Uri(UriBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<Movie> GetMovie(int UID)
        {
            PrepClient();
            string endPoint = client.BaseAddress + "movie/" + UID;
            Movie movie = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                movie = await response.Content.ReadAsAsync<Movie>();
            }
            return movie;
        }
        public static async Task<Song> GetSong(int UID)
        {
            PrepClient();
            string endPoint = client.BaseAddress + "music/" + UID;
            Song song = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                song = await response.Content.ReadAsAsync<Song>();
            }
            return song;
        }
        public static async Task<Episode> GetEpisode(int UID)
        {
            PrepClient();
            string endPoint = client.BaseAddress + "tv/" + UID;
            Episode episode = null;
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                episode = await response.Content.ReadAsAsync<Episode>();
            }
            return episode;
        }
        public static async Task<Song[]> GetRecentSongs(int limit)
        {
            PrepClient();
            string endPoint = client.BaseAddress + "music/recent/" + limit;
            Song[] songs = new Song[limit];
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                songs = await response.Content.ReadAsAsync<Song[]>();
            }
            return songs;
        }
        public static async Task<Song[]> getAllSongs() {
            PrepClient();
            string endPoint = client.BaseAddress + "music/";
            var songs = new List<Song>();
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode) {
                songs = await response.Content.ReadAsAsync<Song>();
            }
            return songs;
        }
        public static async Task<Movie[]> getAllMovies() {
            PrepClient();
            string endPoint = client.BaseAddress + "movie/";
            var movies = new List<Movie>();
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode) {
                movies = await response.Content.ReadAsAsync<Movie>();
            }
            return movies;
        }
        public static async Task<Episode[]> getAllEpisodes() {
            PrepClient();
            string endPoint = client.BaseAddress + "tv/";
            var episodes = new List<Episode>();
            HttpResponseMessage response = await client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode) {
                episodes = await response.Content.ReadAsAsync<Episode>();
            }
            return episodes;
        }
    }
}