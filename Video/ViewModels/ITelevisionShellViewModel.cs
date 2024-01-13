namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public interface ITelevisionShellViewModel<E>
    where E: class, IEpisodeTable
{
    EnumTelevisionHoliday CurrentHoliday { get; }
    E? PreviousEpisode { get; }
    bool IsLoaded { get; }
    Task InitAsync();
    void ResetHoliday();
    //i don't think you can do based on manually doing it.
    //because when you go into the other program, its not going to know anything about it.

    //bool DidReset { get; }
}