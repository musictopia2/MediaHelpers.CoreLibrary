namespace MediaHelpers.CoreLibrary.Music.DB.Models;
public interface IPlayListSong
{
    int ID { get; set; }
    int PlayList { get; set; }
    int SongID { get; set; }
    int SongNumber { get; set; }
    IBaseSong Song { get; }
}