namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public class MockMoviesRemoteControlHostService : IMoviesRemoteControlHostService
{
    Func<Task>? IMoviesRemoteControlHostService.NewClient { get; set; }
    Func<Task>? IMoviesRemoteControlHostService.DislikeMovie { get; set; }
    Task IMoviesRemoteControlHostService.InitializeAsync()
    {
        return Task.CompletedTask;
    }
    Task IMoviesRemoteControlHostService.SendProgressAsync(MoviesModel show)
    {
        return Task.CompletedTask;
    }
}