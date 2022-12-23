using AmpacheApi.DataTypes;
using AmpacheApi.DataTypes.Authentication;
using AmpacheApi.Workers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AmpacheApi
{
    public interface IAmpacheClient
    {
        bool Login();
        List<Artist> GetAllArtists();
        Artist GetArtist(long artistId);
        List<Album> GetArtistAlbums(long artistId);
        Album GetAlbum(long albumId);
        List<Song> GetAlbumSongs(long filter);
    }

    public class AmpacheClient : IAmpacheClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAmpacheAuthInfo _authInfo;

        public AmpacheClient(IAmpacheAuthInfo authInfo)
        {
            _authInfo = authInfo;
            _httpClient = new HttpClient();
        }

        public bool Login()
        {
            if (string.IsNullOrEmpty(_authInfo.UserName) ||
               string.IsNullOrEmpty(_authInfo.Password) ||
               string.IsNullOrEmpty(_authInfo.AmpacheUrl) ||
               _authInfo == null)
            {
                return false;
            }

            Debug.WriteLine($"Auth: {_authInfo.AuthToken ?? "none"}");

            string request = new AmpacheUrlBuilder()
                                    .WithAction("handshake")
                                    .WithLoginInfo(_authInfo)
                                    .Build();

            bool loginSuccess = GetRequest(request, out LoginSuccessInfo loginSuccessInfo);

            if (!loginSuccess || loginSuccessInfo == null || loginSuccessInfo.auth == null)
            {
                return false;
            }

            _authInfo.AuthToken = loginSuccessInfo.auth;
            return true;
        }

        public List<Artist> GetAllArtists()
        {
            string request = new AmpacheUrlBuilder(_authInfo)
                                    .WithAction("artists")
                                    .Build();

            if (GetRequest(request, out List<Artist> returnInfo))
            {
                return returnInfo;
            }

            if (Login())
            {
                return GetAllArtists();
            }

            return null;
        }

        public Artist GetArtist(long artistId)
        {
            string request = new AmpacheUrlBuilder(_authInfo)
                                    .WithAction("artist")
                                    .WithFilter(artistId.ToString())
                                    .Build();

            if (GetRequest(request, out Artist returnInfo))
            {
                return returnInfo;
            }

            if (Login())
            {
                return GetArtist(artistId);
            }

            return null;
        }

        public List<Album> GetArtistAlbums(long artistId)
        {
            string request = new AmpacheUrlBuilder(_authInfo)
                                    .WithAction("artist_albums")
                                    .WithFilter(artistId.ToString())
                                    .Build();

            if (GetRequest(request, out List<Album> returnInfo))
            {
                return returnInfo;
            }

            if (Login())
            {
                return GetArtistAlbums(artistId);
            }

            return null;
        }

        public Album GetAlbum(long albumId)
        {
            string request = new AmpacheUrlBuilder(_authInfo)
                                    .WithAction("album")
                                    .WithFilter(albumId.ToString())
                                    .Build();

            if (GetRequest(request, out Album returnInfo))
            {
                return returnInfo;
            }

            if (Login())
            {
                return GetAlbum(albumId);
            }

            return null;
        }

        public List<Song> GetAlbumSongs(long albumId)
        {
            string request = new AmpacheUrlBuilder(_authInfo)
                                    .WithAction("album_songs")
                                    .WithFilter(albumId.ToString())
                                    .Build();

            if (GetRequest(request, out List<Song> returnInfo))
            {
                return returnInfo;
            }

            if (Login())
            {
                return GetAlbumSongs(albumId);
            }

            return null;
        }

        private bool GetRequest<T>(string urlWithQuerries, out T result)
        {
            HttpResponseMessage response = _httpClient.GetAsync(urlWithQuerries).Result;
            string content = response.Content.ReadAsStringAsync().Result;

            try
            {
                var error = JsonConvert.DeserializeObject<AuthError>(content);
                if (error.error.code == HttpStatusCode.Unauthorized)
                {
                    result = default;
                    return false;
                }
            }
            catch (Exception) { }

            try
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            catch (JsonSerializationException)
            {
                //there are situations where Ampache returns a list of an object when 
                //all you request is one object
                result = JsonConvert.DeserializeObject<List<T>>(content).First();
            }

            return true;
        }
    }
}
