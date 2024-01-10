namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public class MockTelevisionRemoteControlHostService : ITelevisionRemoteControlHostService
{
    Func<Task>? ITelevisionRemoteControlHostService.NewClient { get; set; }
    Func<Task>? ITelevisionRemoteControlHostService.SkipEpisodeForever { get; set; }
    Func<EnumTelevisionHoliday, Task>? ITelevisionRemoteControlHostService.ModifyHoliday { get; set; }
    Func<Task>? ITelevisionRemoteControlHostService.SkipEpisodeTemporarily { get; set; }
    Task ITelevisionRemoteControlHostService.InitializeAsync()
    {
        return Task.CompletedTask;
    }
    Task ITelevisionRemoteControlHostService.SendProgressAsync(TelevisionModel show)
    {
        return Task.CompletedTask;
    }
}