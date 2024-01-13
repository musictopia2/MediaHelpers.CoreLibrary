namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionBasicFirstRunNextEpisodeLogic<E>(IFirstRunBasicTelevisionContext<E> data) : INextEpisodeLogic<E>
    where E : class, IEpisodeTable
{
    async Task<E?> INextEpisodeLogic<E>.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        E? episode = await data.GetNextEpisodeAsync(selectedItem.ID);
        if (episode is null)
        {
            await data.FinishVideoFirstRunAsync(selectedItem.ID);
            return null;
        }
        return episode;
    }
}