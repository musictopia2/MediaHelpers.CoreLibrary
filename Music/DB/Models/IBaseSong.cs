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
}