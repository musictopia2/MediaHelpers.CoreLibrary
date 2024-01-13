namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionCompleteShellLogic<E>(IStartListTelevisionContext<E> dats) : ITelevisionShellLogic<E>
    where E: class, IEpisodeTable
{
    //all you need is the start.  which means needs to register start as well (?)
    async Task<E?> ITelevisionShellLogic<E>.GetPreviousEpisodeAsync()
    {
        bool hadPrevious = await dats.HadPreviousEpisodeAsync();
        if (hadPrevious == false)
        {
            return null;
        }
        return dats.CurrentEpisode;
    }
}