using System.Collections.Generic;

namespace AmpacheApi.DataTypes
{
    public class Song
    {
        public string TimeSting
        {
            get
            {
                string timeString = "";
                int modifiedTime = time;

                int hours = time / 3600;

                if (hours != 0)
                {
                    timeString += $"{hours}:";

                    modifiedTime = modifiedTime - hours * 3600;

                    if (modifiedTime / 60 < 10)
                    {
                        timeString += $"0";
                    }
                }

                int minutes = modifiedTime / 60;

                if (minutes == 0)
                {
                    timeString += $"0:";
                }
                else
                {
                    timeString += $"{minutes}:";
                }

                int seconds = modifiedTime % 60;

                if (seconds < 10)
                {
                    timeString += $"0";
                }

                timeString += $"{seconds}";

                return timeString;
            }
        }

        public long id { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public Artist artist { get; set; }
        public Album album { get; set; }
        public List<Tag> tag { get; set; }
        public string filename { get; set; }
        public int track { get; set; }
        public int playlisttrack { get; set; }
        public int time { get; set; }
        public int year { get; set; }
        public int bitrate { get; set; }
        public int rate { get; set; }
        public string mode { get; set; }
        public string mime { get; set; }
        public string url { get; set; }
        public int size { get; set; }
        public object mbid { get; set; }
        public object album_mbid { get; set; }
        public string artist_mbid { get; set; }
        public object albumartist_mbid { get; set; }
        public string art { get; set; }
        public int flag { get; set; }
        public object preciserating { get; set; }
        public object rating { get; set; }
        public object averagerating { get; set; }
        public int playcount { get; set; }
        public int catalog { get; set; }
        public string composer { get; set; }
        public object channels { get; set; }
        public string comment { get; set; }
        public string publisher { get; set; }
        public string language { get; set; }
        public string replaygain_album_gain { get; set; }
        public string replaygain_album_peak { get; set; }
        public string replaygain_track_gain { get; set; }
        public string replaygain_track_peak { get; set; }
        public List<Genre> genre { get; set; }
    }
}
