namespace MediaHelpers.CoreLibrary.Music.DB.Models;
public interface IBaseSong : IPlaySong
{
    bool Christmas { get; set; }
    int ArtistID { get; set; }
    bool Romantic { get; set; }
    int? YearSong { get; set; }
    bool WorkOut { get; set; }
    string SpecialFormat { get; set; }
    string ShowType { get; set; }
    bool DeleteThis { get; set; } //looks like there needs to be a field for whether a song is deleted or not.
    int SongNumber { get; set; }
}