namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IBasicTelevisionRemoteControlHostService<T>
    where T: class, IBasicTelevisionModel, new()
{
    Task InitializeAsync();
    Task SendProgressAsync(T show);
    Func<Task>? NewClient { get; set; }
    Func<EnumNextMode, Task>? SkipEpisodeForever { get; set; } //will start to enable when skipping episode what should happen.
    Func<EnumNextMode,  Task>? EditLater { get; set; }
    //iffy
    Func<HolidayModel, Task>? ModifyHoliday { get; set; } //will have to serialize/deserialize here.  because the complex actions only support one parameter, not 2.
}