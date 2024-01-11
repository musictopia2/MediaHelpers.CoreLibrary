namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionBasicFirstRunNextEpisodeLogic(IFirstRunBasicTelevisionContext data) : INextEpisodeLogic
{
    async Task<IEpisodeTable?> INextEpisodeLogic.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        IEpisodeTable? episode = await data.GetNextEpisodeAsync(selectedItem.ID);
        if (episode is null)
        {
            await data.FinishVideoFirstRunAsync(selectedItem.ID);
            return null;
        }
        return episode;
    }
}