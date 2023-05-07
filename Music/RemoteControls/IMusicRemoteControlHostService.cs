namespace MediaHelpers.CoreLibrary.Music.RemoteControls;
public interface IMusicRemoteControlHostService
{
    Task InitializeAsync();
    Task SendProgressAsync(SongModel song);
    Func<Task>? NewClient { get; set; }
    Func<Task>? DeleteSong { get; set; } //since television did not have anything for pause function, maybe not needed here either.
    Func<Task>? IncreaseWeight { get; set; }
    Func<Task>? DecreaseWeight { get; set; }
    Action? PlayPause { get; set; } //looks like it can't just play/pause anymore.  since its more advanced now.
}