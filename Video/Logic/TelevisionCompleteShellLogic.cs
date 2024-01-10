namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionCompleteShellLogic(IStartListTelevisionContext dats) : ITelevisionShellLogic
{
    //all you need is the start.  which means needs to register start as well (?)
    async Task<IEpisodeTable?> ITelevisionShellLogic.GetPreviousEpisodeAsync()
    {
        bool hadPrevious = await dats.HadPreviousEpisodeAsync();
        if (hadPrevious == false)
        {
            return null;
        }
        return dats.CurrentEpisode;
    }
}