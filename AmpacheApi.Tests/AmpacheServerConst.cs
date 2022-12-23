using AmpacheApi.DataTypes;

namespace AmpacheApi.Tests;

public static partial class AmpacheServerConst
{
    public const int Song1Id = 1;
    public const int Song2Id = 2;
    public const int Song3Id = 3;
    public const int Song4Id = 4;
    public const int Song5Id = 5;
    public const int Song6Id = 6;
    public const int Song7Id = 7;
    public const int Song8Id = 8;
    public const int Song9Id = 9;

    public static List<Song> SongList
    {
        get
        {
            return new List<Song>
                {
                    new Song{ id = Song1Id, album=AlbumList[0], artist=AlbumList[0].artist },
                    new Song{ id = Song2Id, album=AlbumList[0], artist=AlbumList[0].artist },
                    new Song{ id = Song3Id, album=AlbumList[1], artist=AlbumList[1].artist },
                    new Song{ id = Song4Id, album=AlbumList[1], artist=AlbumList[1].artist },
                    new Song{ id = Song5Id, album=AlbumList[2], artist=AlbumList[2].artist },
                    new Song{ id = Song6Id, album=AlbumList[2], artist=AlbumList[2].artist },
                    new Song{ id = Song7Id, album=AlbumList[3], artist=AlbumList[3].artist },
                    new Song{ id = Song8Id, album=AlbumList[3], artist=AlbumList[3].artist },
                    new Song{ id = Song9Id, album=AlbumList[3], artist=AlbumList[3].artist },
                };
        }
    }

    public const int Artist1Id = 1;
    public const int Artist2Id = 2;
    public const int Artist3Id = 3;

    public static List<Artist> ArtistList
    {
        get
        {
            return new List<Artist>
                {
                    new Artist{ id = Artist1Id },
                    new Artist{ id = Artist2Id },
                    new Artist{ id = Artist3Id },
                };
        }
    }

    public const int Album1Id = 1;
    public const int Album2Id = 2;
    public const int Album3Id = 3;
    public const int Album4Id = 4;

    public static List<Album> AlbumList
    {
        get
        {
            return new List<Album>
                {
                    new Album{ id = Album1Id, artist = ArtistList[0] },
                    new Album{ id = Album2Id, artist = ArtistList[1] },
                    new Album{ id = Album3Id, artist = ArtistList[1] },
                    new Album{ id = Album4Id, artist = ArtistList[2] },
                };
        }
    }
}
