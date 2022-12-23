using AmpacheApi.DataTypes;
using AmpacheApi.DataTypes.Authentication;

namespace AmpacheApi.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class AmpacheClientTests
{
    public TestContext TestContext { get; set; }
    private IAmpacheAuthInfo _ampacheAuthInfo = new AmpacheAuthInfo
    {
        AmpacheUrl = "https://music.daltonsbaker.com",
        UserName = "test",
        Password = "test"
    };

    [TestCleanup]
    public void LogoutOfAmpache()
    {
        //I might implement this eventually
    }


    [TestMethod]
    public void CreateSutTest()
    {
        IAmpacheClient sut = CreateSut();
        Assert.IsNotNull(sut);
    }

    [TestMethod]
    public void LoginTest_GoodLogin()
    {
        //Arrange
        IAmpacheClient sut = CreateSut();

        //Act
        bool actual = sut.Login();

        //Assert
        Assert.IsTrue(actual);
        Assert.IsNotNull(_ampacheAuthInfo.AuthToken);
        TestContext.WriteLine(_ampacheAuthInfo.AuthToken);
    }

    [TestMethod]
    public void LoginTest_BadLogin()
    {
        //Arrange
        IAmpacheAuthInfo badLogin = new AmpacheAuthInfo();
        IAmpacheClient sut = CreateSut(badLogin);

        //Act
        bool actual = sut.Login();

        //Assert
        Assert.IsFalse(actual);
        Assert.IsNull(badLogin.AuthToken);
    }

    [TestMethod]
    public void GetAllArtistsTest()
    {
        //Arrange
        IAmpacheClient sut = CreateSut(_ampacheAuthInfo);

        //Act
        List<Artist> artists = sut.GetAllArtists();

        //Assert
        Assert.IsNotNull(artists);
        Assert.IsTrue(artists.Count > 0);
    }

    [TestMethod]
    public void GetAllArtistAlbumsTest()
    {
        //Arrange
        IAmpacheClient sut = CreateSut();

        //Act
        List<Artist> artists = sut.GetAllArtists();
        List<Album> albums = sut.GetArtistAlbums(artists.First().id);

        //Assert
        Assert.IsNotNull(albums);
        Assert.IsTrue(albums.Count > 0);
    }

    [TestMethod]
    public void GetAllAlbumSongsTest()
    {
        //Arrange
        IAmpacheClient sut = CreateSut();

        //Act
        List<Artist> artists = sut.GetAllArtists();
        List<Album> albums = sut.GetArtistAlbums(artists.First().id);
        List<Song> songs = sut.GetAlbumSongs(albums.First().id);

        //Assert
        Assert.IsNotNull(songs);
        Assert.IsTrue(songs.Count > 0);
    }

    [TestMethod]
    public void GetArtistTest()
    {
        //Arrange
        IAmpacheClient sut = CreateSut();

        //Act
        List<Artist> artists = sut.GetAllArtists();
        Artist artist = sut.GetArtist(artists.First().id);

        //Assert
        Assert.IsNotNull(artist);
        Assert.IsNotNull(artist.id);
        Assert.IsNotNull(artist.name);
    }

    [TestMethod]
    public void GetAlbumTest()
    {
        //Arrange
        IAmpacheClient sut = CreateSut();

        //Act
        List<Artist> artists = sut.GetAllArtists();
        List<Album> albums = sut.GetArtistAlbums(artists.First().id);
        Album album = sut.GetAlbum(albums.First().id);

        //Assert
        Assert.IsNotNull(album);
        Assert.IsNotNull(album.id);
        Assert.IsNotNull(album.name);
    }

    public IAmpacheClient CreateSut(IAmpacheAuthInfo authInfo = null)
    {
        return new AmpacheClient(authInfo ?? _ampacheAuthInfo);
    }
}

