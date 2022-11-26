namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionFirstrunShellViewModel : ITelevisionShellViewModel
{
    public EnumTelevisionHoliday CurrentHoliday { get; private set; } = EnumTelevisionHoliday.None;
    public IEpisodeTable? PreviousEpisode { get; private set; }
    public bool IsLoaded { get; private set; }
    public bool DidReset => false; //always false for this implementation for it.
    public Task InitAsync()
    {
        IsLoaded = false;
        PreviousEpisode = null;
        IsLoaded = true;
        return Task.CompletedTask;
    }
    public void ResetHoliday() //nothing for holidays
    {
        
    }
}