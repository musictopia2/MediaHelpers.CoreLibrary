namespace MediaHelpers.CoreLibrary.Music.DB.Models;
public interface IArtist
{
    int ID { get; set; }
    string ArtistName { get; set; }
    IEnumerable<IBaseSong> SimpleSongList { get; } //because of the way castings work, i am still forced to use this one.
}