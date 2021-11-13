namespace MediaHelpers.CoreLibrary.Music.DB.Models;
public interface IPlayListProgress
{
    int PlayList { get; set; }
    int SongNumber { get; set; }
    int ResumeAt { get; set; }
}