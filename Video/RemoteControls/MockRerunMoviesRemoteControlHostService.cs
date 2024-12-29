namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public class MockRerunMoviesRemoteControlHostService : IRerunMoviesRemoveControlHostService<BasicMoviesModel>, IBasicMoviesRemoteControlHostService<BasicMoviesModel>
{
    Func<Task>? IBasicMoviesRemoteControlHostService<BasicMoviesModel>.NewClient { get; set; }
    Func<Task>? IBasicMoviesRemoteControlHostService<BasicMoviesModel>.DislikeMovie { get; set; }
    Func<Task>? IRerunMoviesRemoveControlHostService<BasicMoviesModel>.EditLater { get; set; }
    Func<Task>? IRerunMoviesRemoveControlHostService<BasicMoviesModel>.ExitEarly { get; set; }
    Task IBasicMoviesRemoteControlHostService<BasicMoviesModel>.InitializeAsync()
    {
        return Task.CompletedTask;
    }
    Task IBasicMoviesRemoteControlHostService<BasicMoviesModel>.SendProgressAsync(BasicMoviesModel movie)
    {
        return Task.CompletedTask;
    }
}