namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionRerunsLoaderLogic(IRerunTelevisionContext data) : IRerunTelevisionLoaderLogic
{
    async Task IBasicTelevisionLoaderLogic.EndTVEpisodeEarlyAsync(IEpisodeTable episode)
    {
        await FinishEpisodeAsync(episode);
    }
    private async Task FinishEpisodeAsync(IEpisodeTable episode)
    {
        episode.ResumeAt = null;
        data.CurrentEpisode = episode;
        await data.UpdateEpisodeAsync();
    }
    async Task IBasicTelevisionLoaderLogic.FinishTVEpisodeAsync(IEpisodeTable episode)
    {
        await FinishEpisodeAsync(episode);
        await data.EndEpisodeAsync(); //may reopen for another episode (depends)
    }
    Task IBasicTelevisionLoaderLogic.ForeverSkipEpisodeAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode; //just in case.
        return data.ForeverSkipEpisodeAsync();
    }
    int IBasicTelevisionLoaderLogic.GetSeconds(IEpisodeTable episode)
    {
        return episode.GetSeconds(data);
    }
    Task IBasicTelevisionLoaderLogic.UpdateTVShowProgressAsync(IEpisodeTable episode, int position)
    {
        data.CurrentEpisode = episode;
        data.Seconds = position;
        return Task.CompletedTask;
    }
    Task IBasicTelevisionLoaderLogic.ModifyHolidayAsync(IEpisodeTable episode, EnumTelevisionHoliday holiday)
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
    async Task IBasicTelevisionLoaderLogic.ReloadAppAsync(IEpisodeTable newEpisode)
    {
        await InitializeEpisodeAsync(newEpisode);
        await data.ReloadAppAsync();
    }
    async Task IBasicTelevisionLoaderLogic.InitializeEpisodeAsync(IEpisodeTable episode)
    {
        await InitializeEpisodeAsync(episode);
    }
    private async Task InitializeEpisodeAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode;
        await data.InitializeEpisodeAsync();
    }

    async Task IRerunTelevisionLoaderLogic.TemporarilySKipEpisodeAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode;
        await data.TemporarilySkipEpisodeAsync();
    }

    Task<bool> IRerunTelevisionLoaderLogic.CanGoToNextEpisodeAsync()
    {
        return data.CanAutomaticallyGoToNextEpisodeAsync();
    }
}