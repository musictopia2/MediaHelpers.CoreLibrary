namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionFirstrunLoaderLogic : ITelevisionLoaderLogic
{
    private readonly ITelevisionContext _data;
    public TelevisionFirstrunLoaderLogic(ITelevisionContext data)
    {
        _data = data;
    }
    async Task ITelevisionLoaderLogic.AddToHistoryAsync(IEpisodeTable episode)
    {
        await AddToHistoryAsync(episode);
    }
    private async Task AddToHistoryAsync(IEpisodeTable episode)
    {
        _data.CurrentEpisode = episode;
        await _data.AddFirstRunViewHistoryAsync();
    }
    async Task ITelevisionLoaderLogic.EndTVEpisodeEarlyAsync(IEpisodeTable episode)
    {
        await EndEpisodeAsync(episode);
    }

    private async Task EndEpisodeAsync(IEpisodeTable episode)
    {
        episode.ResumeAt = null;
        _data.CurrentEpisode = episode;
        await _data.FinishVideoFirstRunAsync();
    }
    async Task ITelevisionLoaderLogic.FinishTVEpisodeAsync(IEpisodeTable episode)
    {
        await EndEpisodeAsync(episode);
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
    Task ITelevisionLoaderLogic.ForeverSkipEpisodeAsync(IEpisodeTable episode) //even reruns can skip forever.
    {
        _data.CurrentEpisode = episode;
        return _data.ForeverSkipEpisodeAsync();
    }
    Task ITelevisionLoaderLogic.ModifyHolidayAsync(IEpisodeTable episode, EnumTelevisionHoliday holiday)
    {
        _data.CurrentEpisode = episode;
        return _data.ModifyHolidayCategoryForEpisodeAsync(holiday);
    }
    //was going to not support it but decided that if somehow it happened, then go ahead and close out and go back in (even on firstrun shows).
    async Task ITelevisionLoaderLogic.ReloadAppAsync(IEpisodeTable newEpisode)
    {
        await AddToHistoryAsync(newEpisode);
        _data.ReloadApp();
    }
}