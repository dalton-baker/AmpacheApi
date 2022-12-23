namespace AmpacheApi.DataTypes.Authentication
{
    public interface IAmpacheAuthInfo
    {
        string AmpacheUrl { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string AuthToken { get; set; }

    }

    public class AmpacheAuthInfo : IAmpacheAuthInfo
    {
        public string AmpacheUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
    }
}
