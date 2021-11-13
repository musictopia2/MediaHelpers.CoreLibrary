namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IMoviesRemoteControlHostService
{
    Task InitializeAsync();
    Task SendProgressAsync(MoviesModel movie);
    Func<Task>? NewClient { get; set; }
    Func<Task>? DislikeMovie { get; set; }
}