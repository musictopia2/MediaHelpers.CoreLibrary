namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public interface ITelevisionShellViewModel
{
    EnumTelevisionHoliday CurrentHoliday { get; }
    IEpisodeTable? PreviousEpisode { get; }
    bool IsLoaded { get; }
    Task InitAsync();
    void ResetHoliday();
    bool DidReset { get; }
}