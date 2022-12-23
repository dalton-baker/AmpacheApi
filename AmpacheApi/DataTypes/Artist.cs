using System.Collections.Generic;

namespace AmpacheApi.DataTypes
{
    public class Artist
    {
        public long id { get; set; }
        public string name { get; set; }
        public int albums { get; set; }
        public int songs { get; set; }
        public List<Tag> tag { get; set; }
        public string art { get; set; }
        public int flag { get; set; }
        public object preciserating { get; set; }
        public object rating { get; set; }
        public object averagerating { get; set; }
        public string mbid { get; set; }
        public string summary { get; set; }
        public string yearformed { get; set; }
        public string placeformed { get; set; }
    }
}
