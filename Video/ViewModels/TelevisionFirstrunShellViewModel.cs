namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionFirstrunShellViewModel<E>(ITelevisionShellLogic<E> logic) : ITelevisionShellViewModel<E>
    where E : class, IEpisodeTable
{
    public EnumTelevisionHoliday CurrentHoliday { get; private set; } = EnumTelevisionHoliday.None;
    public E? PreviousEpisode { get; private set; }
    public bool IsLoaded { get; private set; }
    //public bool DidReset => false; //always false for this implementation for it.
    public async Task InitAsync()
    {
        PreviousEpisode = await logic.GetPreviousEpisodeAsync();
        IsLoaded = true;
        //may now have previous episode.
    }
    public void ResetHoliday() //nothing for holidays
    {
        
    }
}