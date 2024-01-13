namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionListViewModel<E>(ITelevisionListLogic list, INextEpisodeLogic<E> next) : MainVideoListViewModel<IShowTable>
    where E : class, IEpisodeTable
{
    public override async Task InitAsync()
    {
        VideoList = await list.GetShowListAsync();
        FocusCombo?.Invoke();
    }

    public async Task<E?> GetEpisodeChosenAsync()
    {
        //still needs this though.
        if (SelectedItem is null)
        {
            throw new CustomBasicException("You never chose a show to watch.  Rethink");
        }
        E? output = await next.GetNextEpisodeAsync(SelectedItem);
        return output;
    }
}