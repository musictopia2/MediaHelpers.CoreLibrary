namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IBasicTelevisionRemoteControlHostService
{
    Task InitializeAsync();
    Task SendProgressAsync(TelevisionModel show);
    Func<Task>? NewClient { get; set; }
    Func<Task>? SkipEpisodeForever { get; set; }
    Func<Task>? EditLater { get; set; }
    Func<EnumTelevisionHoliday, Task>? ModifyHoliday { get; set; }
}