namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public class MockRerunMoviesRemoteControlHostService : IRerunMoviesRemoveControlHostService
{
    Func<Task>? IBasicMoviesRemoteControlHostService.NewClient { get; set; }
    Func<Task>? IBasicMoviesRemoteControlHostService.DislikeMovie { get; set; }
    Func<Task>? IRerunMoviesRemoveControlHostService.EditLater { get; set; }
    Func<Task>? IRerunMoviesRemoveControlHostService.ExitEarly { get; set; }
    Task IBasicMoviesRemoteControlHostService.InitializeAsync()
    {
        return Task.CompletedTask;
    }
    Task IBasicMoviesRemoteControlHostService.SendProgressAsync(MoviesModel show)
    {
        return Task.CompletedTask;
    }
}