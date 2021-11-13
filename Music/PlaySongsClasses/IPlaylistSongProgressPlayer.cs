namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
public interface IPlaylistSongProgressPlayer : IProgressMusicPlayer
{
    int UpTo { get; set; }
    int SongsLeft { get; set; }
    Action UpdateProgress { get; set; }
}