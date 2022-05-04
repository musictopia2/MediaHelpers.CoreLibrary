namespace MediaHelpers.CoreLibrary.Music.DB.Models;
public interface IPlaySong
{
    int ID { get; set; }
    string SongName { get; set; }
    string ArtistName { get; set; } //not has to allow writing because dapper is different.
    string FullPath { get; }
    int Length { get; set; }
    int SongNumber { get; set; }
    bool DeleteThis { get; set; } //looks like there needs to be a field for whether a song is deleted or not.
    string GetSongArtistDisplay(); //i think we still need this one.
}
