namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public interface ITelevisionShellViewModel
{
    EnumTelevisionHoliday CurrentHoliday { get; }
    IEpisodeTable? PreviousEpisode { get; }
    bool IsLoaded { get; }
    Task InitAsync();
    void ResetHoliday();
    //i don't think you can do based on manually doing it.
    //because when you go into the other program, its not going to know anything about it.

    //bool DidReset { get; }
}