namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IRerunMoviesRemoveControlHostService<T> : IBasicMoviesRemoteControlHostService<T>
    where T : class, IBasicMoviesModel, new()
{
    Func<Task>? EditLater { get; set; }
    Func<Task>? ExitEarly { get; set; }
}