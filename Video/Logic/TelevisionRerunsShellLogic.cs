namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionRerunsShellLogic(ITelevisionContext dats) : ITelevisionShellLogic
{
    async Task<IEpisodeTable?> ITelevisionShellLogic.GetPreviousShowAsync()
    {
        bool hadPrevious = dats.HadPreviousShow();
        await Task.CompletedTask;
        if (hadPrevious == false)
        {
            return null;
        }
        dats.LoadResumeTVEpisodeForReruns();
        return dats.CurrentEpisode;
    }
}