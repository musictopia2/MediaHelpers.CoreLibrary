namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IRerunTelevisionRemoteControlHostService : IBasicTelevisionRemoteControlHostService
{
    Func<Task>? SkipEpisodeTemporarily { get; set; }
    //has to do something else for first run
}