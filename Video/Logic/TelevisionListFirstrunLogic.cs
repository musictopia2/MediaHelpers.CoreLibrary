namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListFirstrunLogic(ITelevisionContext data) : ITelevisionListLogic
{
    async Task<IEpisodeTable?> ITelevisionListLogic.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        var episode = data.GetNextFirstRunEpisode(selectedItem.ID);
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