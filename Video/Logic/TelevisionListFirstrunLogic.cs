namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListFirstrunLogic : ITelevisionListLogic
{
    private readonly ITelevisionContext _data;
    public TelevisionListFirstrunLogic(ITelevisionContext data)
    {
        _data = data;
    }
    async Task<IEpisodeTable?> ITelevisionListLogic.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        var episode = _data.GetNextFirstRunEpisode(selectedItem.ID);
        if (episode is null)
        {
            await _data.FinishVideoFirstRunAsync(selectedItem.ID);
            return null;
        }
        return episode;
    }
    Task<BasicList<IShowTable>> ITelevisionListLogic.GetShowListAsync()
    {
        return Task.FromResult(_data.ListShows(EnumTelevisionCategory.FirstRun));
    }
}