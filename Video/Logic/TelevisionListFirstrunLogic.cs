namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListFirstrunLogic(IFirstRunTelevisionContext data) : ITelevisionListLogic
{
    async Task<IEpisodeTable?> ITelevisionListLogic.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        IEpisodeTable? episode = await data.GetNextEpisodeAsync(selectedItem.ID);
        if (episode is null)
        {
            await data.FinishVideoFirstRunAsync(selectedItem.ID);
            return null;
        }
        return episode;
    }
    Task<BasicList<IShowTable>> ITelevisionListLogic.GetShowListAsync()
    {
        return data.ListShowsAsync();
    }
}