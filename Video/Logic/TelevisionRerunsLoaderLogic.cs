namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionRerunsLoaderLogic : ITelevisionLoaderLogic
{
    private readonly ITelevisionContext _data;
    private readonly IExit _exit;
    public TelevisionRerunsLoaderLogic(ITelevisionContext data, IExit exit)
    {
        _data = data;
        _exit = exit;
    }
    async Task ITelevisionLoaderLogic.AddToHistoryAsync(IEpisodeTable episode)
    {
        await AddToHistoryAsync(episode);
    }
    async Task ITelevisionLoaderLogic.EndTVEpisodeEarlyAsync(IEpisodeTable episode)
    {
        await FinishEpisodeAsync(episode);
    }
    private async Task FinishEpisodeAsync(IEpisodeTable episode)
    {
        episode.ResumeAt = null;
        _data.CurrentEpisode = episode;
        await _data.UpdateEpisodeAsync();
    }
    async Task ITelevisionLoaderLogic.FinishTVEpisodeAsync(IEpisodeTable episode)
    {
        await FinishEpisodeAsync(episode);
        _exit.ExitApp();
    }
    Task ITelevisionLoaderLogic.ForeverSkipEpisodeAsync(IEpisodeTable episode)
    {
        _data.CurrentEpisode = episode; //just in case.
        return _data.ForeverSkipEpisodeAsync();
    }
    int ITelevisionLoaderLogic.GetSeconds(IEpisodeTable episode)
    {
        return episode.GetSeconds(_data);
    }
    Task ITelevisionLoaderLogic.UpdateTVShowProgressAsync(IEpisodeTable episode, int position)
    {
        _data.CurrentEpisode = episode;
        _data.Seconds = position;
        return Task.CompletedTask;
    }
    Task ITelevisionLoaderLogic.ModifyHolidayAsync(IEpisodeTable episode, EnumTelevisionHoliday holiday)
    {
        _data.CurrentEpisode = episode;
        return _data.ModifyHolidayCategoryForEpisodeAsync(holiday);
    }
    private async Task AddToHistoryAsync(IEpisodeTable episode)
    {
        _data.CurrentEpisode = episode;
        await _data.AddReRunViewHistory();
    }
    /// <summary>
    /// this will add to history and make the context for television decide how to reload again
    /// </summary>
    /// <param name="newEpisode">this is the new episode chosen</param>
    /// <returns></returns>
    async Task ITelevisionLoaderLogic.ReloadAppAsync(IEpisodeTable newEpisode)
    {
        await AddToHistoryAsync(newEpisode);
        _data.ReloadApp();
    }
}