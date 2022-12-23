using System;

namespace AmpacheApi.DataTypes.Authentication
{
    public class LoginSuccessInfo
    {
        public string auth { get; set; }
        public string api { get; set; }
        public DateTimeOffset session_expire { get; set; }
        public DateTimeOffset update { get; set; }
        public DateTimeOffset add { get; set; }
        public DateTimeOffset clean { get; set; }
        public int songs { get; set; }
        public int albums { get; set; }
        public int artists { get; set; }
        public int playlists { get; set; }
        public int videos { get; set; }
        public int catalogs { get; set; }
    }
}
