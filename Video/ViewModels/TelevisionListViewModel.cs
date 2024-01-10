namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionListViewModel(ITelevisionListLogic logic) : MainVideoListViewModel<IShowTable>
{
    public override async Task InitAsync()
    {
        VideoList = await logic.GetShowListAsync();
        FocusCombo?.Invoke();
    }
    public async Task<IEpisodeTable?> GetEpisodeChosenAsync()
    {
        if (SelectedItem == null)
        {
            throw new CustomBasicException("You never chose a show to watch.  Rethink");
        }
        IEpisodeTable? output = await logic.GetNextEpisodeAsync(SelectedItem); 
        return output;
    }
}