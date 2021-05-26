using System;

namespace LOE {
    public class Episode {
        public int UID { get; set; }
        public string show_title { get; set; }
        public string genre { get; set; }
        public int season_number { get; set; }
        public int season_year { get; set; }
        public string ep_number { get; set; }
        public int runtime { get; set; }
        public string cover_path { get; set; }
        public string file_path { get; set; }
        public string ep_title { get; set; }
    }
}