using System.Net;

namespace AmpacheApi.DataTypes.Authentication
{
    public class AuthError
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public HttpStatusCode code { get; set; }
        public string message { get; set; }
    }

}
