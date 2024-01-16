namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public class MockFirstRunTelevisionRemoteControlHostService : IFirstRunTelevisionRemoteControlHostService
{
    Func<Task>? IBasicTelevisionRemoteControlHostService.NewClient { get; set; }
    Func<Task>? IBasicTelevisionRemoteControlHostService.SkipEpisodeForever { get; set; }
    Func<EnumTelevisionHoliday, Task>? IBasicTelevisionRemoteControlHostService.ModifyHoliday { get; set; }
    Func<Task>? IFirstRunTelevisionRemoteControlHostService.Start { get; set; }
    Func<Task>? IFirstRunTelevisionRemoteControlHostService.IntroBegins { get; set; }
    Func<Task>? IFirstRunTelevisionRemoteControlHostService.EndEpisode { get; set; }
    Func<Task>? IFirstRunTelevisionRemoteControlHostService.Rewind { get; set; }
    Func<Task>? IFirstRunTelevisionRemoteControlHostService.ThemeSongOver { get; set; }
    Func<Task>? IBasicTelevisionRemoteControlHostService.EditLater { get; set; }

    //anything else needed will be generated as well.
    Task IBasicTelevisionRemoteControlHostService.InitializeAsync()
    {
        return Task.CompletedTask;
    }

    Task IBasicTelevisionRemoteControlHostService.SendProgressAsync(TelevisionModel show)
    {
        return Task.CompletedTask;
    }
}