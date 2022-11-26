namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionListViewModel : MainVideoListViewModel<IShowTable>
{
    private readonly ITelevisionListLogic _logic;
    public TelevisionListViewModel(ITelevisionListLogic logic)
    {
        _logic = logic;
    }
    public override async Task InitAsync()
    {
        VideoList = await _logic.GetShowListAsync();
        FocusCombo?.Invoke();
    }
    public async Task<IEpisodeTable?> GetEpisodeChosenAsync()
    {
        if (SelectedItem == null)
        {
            throw new CustomBasicException("You never chose a show to watch.  Rethink");
        }
        IEpisodeTable? output = await _logic.GetNextEpisodeAsync(SelectedItem); 
        return output;
    }
}