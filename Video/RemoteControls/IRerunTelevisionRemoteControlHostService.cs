namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IRerunTelevisionRemoteControlHostService : IBasicTelevisionRemoteControlHostService<BasicTelevisionModel>
{
    Func<Task>? SkipEpisodeTemporarily { get; set; }
    //has to do something else for first run
}