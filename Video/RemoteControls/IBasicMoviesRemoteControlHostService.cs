namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IBasicMoviesRemoteControlHostService<T>
    where T: class, IBasicMoviesModel, new()
{
    Task InitializeAsync();
    Task SendProgressAsync(T movie);
    Func<Task>? NewClient { get; set; }
    Func<Task>? DislikeMovie { get; set; }
}