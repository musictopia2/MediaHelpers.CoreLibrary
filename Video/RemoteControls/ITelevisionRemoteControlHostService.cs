namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface ITelevisionRemoteControlHostService
{
    Task InitializeAsync();
    Task SendProgressAsync(TelevisionModel show);
    Func<Task>? NewClient { get; set; }
    Func<Task>? SkipEpisodeForever { get; set; }
    Func<Task>? SkipEpisodeTemporarily { get; set; }
    Func<EnumTelevisionHoliday, Task>? ModifyHoliday { get; set; }
}