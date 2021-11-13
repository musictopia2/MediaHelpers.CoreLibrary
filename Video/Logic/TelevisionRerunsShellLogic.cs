namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionRerunsShellLogic : ITelevisionShellLogic
{
    private readonly ITelevisionContext _dats;
    public TelevisionRerunsShellLogic(ITelevisionContext dats)
    {
        _dats = dats;
    }
    async Task<IEpisodeTable?> ITelevisionShellLogic.GetPreviousShowAsync()
    {
        bool hadPrevious = _dats.HadPreviousShow();
        await Task.CompletedTask;
        if (hadPrevious == false)
        {
            return null;
        }
        _dats.LoadResumeTVEpisodeForReruns();
        return _dats.CurrentEpisode;
    }
}