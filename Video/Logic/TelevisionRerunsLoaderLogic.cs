namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionRerunsLoaderLogic(ITelevisionContext data) : ITelevisionLoaderLogic
{
    async Task ITelevisionLoaderLogic.EndTVEpisodeEarlyAsync(IEpisodeTable episode)
    {
        await FinishEpisodeAsync(episode);
    }
    private async Task FinishEpisodeAsync(IEpisodeTable episode)
    {
        episode.ResumeAt = null;
        data.CurrentEpisode = episode;
        await data.UpdateEpisodeAsync();
    }
    async Task ITelevisionLoaderLogic.FinishTVEpisodeAsync(IEpisodeTable episode)
    {
        await FinishEpisodeAsync(episode);
        await data.EndEpisodeAsync(); //may reopen for another episode (depends)
    }
    Task ITelevisionLoaderLogic.ForeverSkipEpisodeAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode; //just in case.
        return data.ForeverSkipEpisodeAsync();
    }
    int ITelevisionLoaderLogic.GetSeconds(IEpisodeTable episode)
    {
        return episode.GetSeconds(data);
    }
    Task ITelevisionLoaderLogic.UpdateTVShowProgressAsync(IEpisodeTable episode, int position)
    {
        data.CurrentEpisode = episode;
        data.Seconds = position;
        return Task.CompletedTask;
    }
    Task ITelevisionLoaderLogic.ModifyHolidayAsync(IEpisodeTable episode, EnumTelevisionHoliday holiday)
    {
        data.CurrentEpisode = episode;
        return data.ModifyHolidayCategoryForEpisodeAsync(holiday);
    }
    //private async Task AddToHistoryAsync(IEpisodeTable episode)
    //{
    //    _data.CurrentEpisode = episode;
    //    await _data.AddReRunViewHistory();
    //}
    /// <summary>
    /// this will initialize the episode so it can do whatever is needed.  then the context has to decide how it will reload
    /// </summary>
    /// <param name="newEpisode">this is the new episode chosen</param>
    /// <returns></returns>
    async Task ITelevisionLoaderLogic.ReloadAppAsync(IEpisodeTable newEpisode)
    {
        await InitializeEpisodeAsync(newEpisode);
        await data.ReloadAppAsync();
    }
    async Task ITelevisionLoaderLogic.InitializeEpisodeAsync(IEpisodeTable episode)
    {
        await InitializeEpisodeAsync(episode);
    }
    private async Task InitializeEpisodeAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode;
        await data.InitializeRerunEpisodeAsync();
    }

    async Task ITelevisionLoaderLogic.TemporarilySKipEpisodeAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode;
        await data.TemporarilySkipEpisodeAsync();
    }
}