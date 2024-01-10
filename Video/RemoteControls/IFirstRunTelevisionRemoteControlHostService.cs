namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IFirstRunTelevisionRemoteControlHostService : IBasicTelevisionRemoteControlHostService
{
    //anything that is now specialized with firstrun will be here.
    Func<Task>? Start { get; set; }
    Func<Task>? IntroBegins { get; set; }
    Func<Task>? EndEpisode { get; set; }
    Func<Task>? Rewind { get; set; }
    Func<Task>? ThemeSongOver { get; set; }

}