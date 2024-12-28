namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IRerunMoviesRemoveControlHostService : IBasicMoviesRemoteControlHostService
{
    Func<Task>? EditLater { get; set; }
    Func<Task>? ExitEarly { get; set; }
}