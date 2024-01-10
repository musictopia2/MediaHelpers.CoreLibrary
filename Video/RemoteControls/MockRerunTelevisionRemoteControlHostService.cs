namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public class MockRerunTelevisionRemoteControlHostService : IRerunTelevisionRemoteControlHostService
{
    Func<Task>? IBasicTelevisionRemoteControlHostService.NewClient { get; set; }
    Func<Task>? IBasicTelevisionRemoteControlHostService.SkipEpisodeForever { get; set; }
    Func<EnumTelevisionHoliday, Task>? IBasicTelevisionRemoteControlHostService.ModifyHoliday { get; set; }
    Func<Task>? IRerunTelevisionRemoteControlHostService.SkipEpisodeTemporarily { get; set; }

    Task IBasicTelevisionRemoteControlHostService.InitializeAsync()
    {
        return Task.CompletedTask;
    }

    Task IBasicTelevisionRemoteControlHostService.SendProgressAsync(TelevisionModel show)
    {
        return Task.CompletedTask;
    }
}