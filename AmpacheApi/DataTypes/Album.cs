using System.Collections.Generic;

namespace AmpacheApi.DataTypes
{
    public class Album
    {
        public long id { get; set; }
        public string name { get; set; }
        public Artist artist { get; set; }
        public int year { get; set; }
        public int tracks { get; set; }
        public int disk { get; set; }
        public List<Tag> tag { get; set; }
        public string art { get; set; }
        public int flag { get; set; }
        public object preciserating { get; set; }
        public object rating { get; set; }
        public object averagerating { get; set; }
        public object mbid { get; set; }
    }

}
