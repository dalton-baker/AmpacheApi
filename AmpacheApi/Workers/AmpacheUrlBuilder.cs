using AmpacheApi.DataTypes.Authentication;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace AmpacheApi.Workers
{
    public class AmpacheUrlBuilder
    {
        private string _baseUrl;
        private readonly List<string> _queries = new List<string>();

        public AmpacheUrlBuilder(IAmpacheAuthInfo ampacheAuthInfo)
        {
            WithBaseUrl(ampacheAuthInfo.AmpacheUrl).WithAuthentication(ampacheAuthInfo.AuthToken);
        }

        public AmpacheUrlBuilder()
        { }

        public AmpacheUrlBuilder WithBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl.Trim(new char[] { '/' });
            return this;
        }

        public AmpacheUrlBuilder WithAuthentication(string athentication)
        {
            _queries.Add($"auth={athentication}");
            return this;
        }

        public AmpacheUrlBuilder WithAction(string action)
        {
            _queries.Add($"action={action}");
            return this;
        }

        public AmpacheUrlBuilder WithFilter(string filter)
        {
            _queries.Add($"filter={filter}");
            return this;
        }

        public AmpacheUrlBuilder WithLoginInfo(IAmpacheAuthInfo ampacheAuthInfo)
        {
            WithBaseUrl(ampacheAuthInfo.AmpacheUrl);

            string unixTime = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            string auth = ComputeSha256Hash($"{unixTime}{ComputeSha256Hash(ampacheAuthInfo.Password)}");
            string user = ampacheAuthInfo.UserName;

            _queries.Add($"auth={auth}");
            _queries.Add($"timestamp={unixTime}");
            _queries.Add($"user={user}");

            return this;
        }

        public AmpacheUrlBuilder WithCustomQuery(string queryName, string query)
        {
            _queries.Add($"{queryName}={query}");
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrEmpty(_baseUrl))
            {
                return string.Empty;
            }

            string fullUrl = $"{_baseUrl}/server/json.server.php";

            for (int i = 0; i < _queries.Count; i++)
            {
                if (i == 0)
                    fullUrl += "?";

                fullUrl += _queries[i] + "&";
            }

            return fullUrl.Trim(new char[] { '&' }); ;
        }

        //this function was shamelessly take from here: https://www.c-sharpcorner.com/article/compute-sha256-hash-in-c-sharp/
        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
