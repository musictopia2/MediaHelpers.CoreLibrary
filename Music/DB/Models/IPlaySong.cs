namespace MediaHelpers.CoreLibrary.Music.DB.Models;
public interface IPlaySong
{
    int ID { get; set; }
    string SongName { get; set; }
    string ArtistName { get; set; } //not has to allow writing because dapper is different.
    string FullPath { get; }
    int Length { get; set; }
    int SongNumber { get; set; }
    string GetSongArtistDisplay(); //i think we still need this one.
}
