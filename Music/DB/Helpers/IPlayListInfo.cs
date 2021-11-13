namespace MediaHelpers.CoreLibrary.Music.DB.Helpers;
public interface IPlayListInfo
{
    IPlayListSong GetNewPlayListSong();
    IPlayListProgress GetNewPlayListProgress();
}