using System;

namespace LOE {
    public class Song {
        public int UID { get; set; }
        public string title { get; set; }
        public string artist { get; set; }
        public string album { get; set; }
        public int year { get; set; }
        public int track_number { get; set; }
        public string genre { get; set; }
        public string band { get; set; }
        public string length { get; set; }
        public string publisher { get; set; }
        public string bpm { get; set; }
        public string feat { get; set; }
        public string file_path { get; set; }
        public string cover_path { get; set; }
        public DateTime created_date { get; set; }
        public int play_count { get; set; }
        public DateTime last_play { get; set; }
        public string artist_country { get; set; }
    }
}