namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionCompleteShellLogic(ITelevisionContext dats) : ITelevisionShellLogic
{
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