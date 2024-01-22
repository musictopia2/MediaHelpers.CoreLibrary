namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public class MockRerunTelevisionRemoteControlHostService : IRerunTelevisionRemoteControlHostService
{
    Func<Task>? IBasicTelevisionRemoteControlHostService<BasicTelevisionModel>.NewClient { get; set; }
    Func<Task>? IBasicTelevisionRemoteControlHostService<BasicTelevisionModel>.SkipEpisodeForever { get; set; }
    Func<EnumTelevisionHoliday, Task>? IBasicTelevisionRemoteControlHostService<BasicTelevisionModel>.ModifyHoliday { get; set; }
    Func<Task>? IRerunTelevisionRemoteControlHostService.SkipEpisodeTemporarily { get; set; }
    Func<Task>? IBasicTelevisionRemoteControlHostService<BasicTelevisionModel>.EditLater { get; set; }

    Task IBasicTelevisionRemoteControlHostService<BasicTelevisionModel>.InitializeAsync()
    {
        return Task.CompletedTask;
    }

    Task IBasicTelevisionRemoteControlHostService<BasicTelevisionModel>.SendProgressAsync(BasicTelevisionModel show)
    {
        return Task.CompletedTask;
    }
}