namespace MediaHelpers.CoreLibrary.Music.RemoteControls;
public class MockMusicRemoteControlHostService : IMusicRemoteControlHostService
{
    Func<Task>? IMusicRemoteControlHostService.NewClient { get; set; }
    Func<Task>? IMusicRemoteControlHostService.DeleteSong { get; set; }
    Func<Task>? IMusicRemoteControlHostService.IncreaseWeight { get; set; }
    Func<Task>? IMusicRemoteControlHostService.DecreaseWeight { get; set; }
    Task IMusicRemoteControlHostService.InitializeAsync()
    {
        return Task.CompletedTask;
    }
    Task IMusicRemoteControlHostService.SendProgressAsync(SongModel song)
    {
        return Task.CompletedTask;
    }
}