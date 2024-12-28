namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IBasicMoviesRemoteControlHostService
{
    Task InitializeAsync();
    Task SendProgressAsync(MoviesModel movie);
    Func<Task>? NewClient { get; set; }
    Func<Task>? DislikeMovie { get; set; }
}