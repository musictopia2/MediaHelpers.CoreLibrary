namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionListViewModel(ITelevisionListLogic list, INextEpisodeLogic next) : MainVideoListViewModel<IShowTable>
{
    public override async Task InitAsync()
    {
        VideoList = await list.GetShowListAsync();
        FocusCombo?.Invoke();
    }

    public async Task<IEpisodeTable?> GetEpisodeChosenAsync()
    {
        //still needs this though.
        if (SelectedItem is null)
        {
            throw new CustomBasicException("You never chose a show to watch.  Rethink");
        }
        IEpisodeTable? output = await next.GetNextEpisodeAsync(SelectedItem);
        return output;
    }
}