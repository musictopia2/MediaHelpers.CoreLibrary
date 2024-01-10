namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionFirstrunLoaderLogic(ITelevisionContext data) : ITelevisionLoaderLogic
{
    async Task ITelevisionLoaderLogic.InitializeEpisodeAsync(IEpisodeTable episode)
    {
        await InitializeEpisodeAsync(episode);
    }
    //async Task ITelevisionLoaderLogic.AddToHistoryAsync(IEpisodeTable episode)
    //{
    //    await AddToHistoryAsync(episode);
    //}
    private async Task InitializeEpisodeAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode;
        await data.InitializeFirstRunEpisodeAsync();
        //await _data.AddFirstRunViewHistoryAsync();
    }
    async Task ITelevisionLoaderLogic.EndTVEpisodeEarlyAsync(IEpisodeTable episode)
    {
        await EndEpisodeAsync(episode);
    }

    private async Task EndEpisodeAsync(IEpisodeTable episode)
    {
        episode.ResumeAt = null;
        data.CurrentEpisode = episode;
        await data.FinishVideoFirstRunAsync(); //should not need a way to be able to end episode since it most likely will close out.
    }
    async Task ITelevisionLoaderLogic.FinishTVEpisodeAsync(IEpisodeTable episode)
    {
        await EndEpisodeAsync(episode);
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
    Task ITelevisionLoaderLogic.ForeverSkipEpisodeAsync(IEpisodeTable episode) //even reruns can skip forever.
    {
        data.CurrentEpisode = episode;
        return data.ForeverSkipEpisodeAsync();
    }
    Task ITelevisionLoaderLogic.ModifyHolidayAsync(IEpisodeTable episode, EnumTelevisionHoliday holiday)
    {
        data.CurrentEpisode = episode;
        return data.ModifyHolidayCategoryForEpisodeAsync(holiday);
    }
    //was going to not support it but decided that if somehow it happened, then go ahead and close out and go back in (even on firstrun shows).
    async Task ITelevisionLoaderLogic.ReloadAppAsync(IEpisodeTable newEpisode)
    {
        //may have to rethink if one is youtube and the other is not.
        await InitializeEpisodeAsync(newEpisode); //i think.
        await data.ReloadAppAsync();
        //await AddToHistoryAsync(newEpisode);
    }

    Task ITelevisionLoaderLogic.TemporarilySKipEpisodeAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode;
        return data.TemporarilySkipEpisodeAsync();
    }
}