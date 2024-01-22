namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IBasicTelevisionRemoteControlHostService<T>
    where T: class, IBasicTelevisionModel, new()
{
    Task InitializeAsync();
    Task SendProgressAsync(T show);
    Func<Task>? NewClient { get; set; }
    Func<Task>? SkipEpisodeForever { get; set; }
    Func<Task>? EditLater { get; set; }
    Func<EnumTelevisionHoliday, Task>? ModifyHoliday { get; set; }
}