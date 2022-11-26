namespace MediaHelpers.CoreLibrary.Music.ViewModels;
public interface IPlayPauseClass
{
    void PlayPause();
    bool CanPause { get; }
}