namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionFirstrunLoaderLogic(IFirstRunTelevisionContext data) : IFirstRunTelevisionLoaderLogic
{
    async Task IBasicTelevisionLoaderLogic.InitializeEpisodeAsync(IEpisodeTable episode)
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
        await data.InitializeEpisodeAsync();
        //await _data.AddFirstRunViewHistoryAsync();
    }
    async Task IBasicTelevisionLoaderLogic.EndTVEpisodeEarlyAsync(IEpisodeTable episode)
    {
        await EndEpisodeAsync(episode);
    }

    private async Task EndEpisodeAsync(IEpisodeTable episode)
    {
        episode.ResumeAt = null;
        data.CurrentEpisode = episode;
        await data.FinishVideoFirstRunAsync(); //should not need a way to be able to end episode since it most likely will close out.
    }
    async Task IBasicTelevisionLoaderLogic.FinishTVEpisodeAsync(IEpisodeTable episode)
    {
        await EndEpisodeAsync(episode);
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
    Task IBasicTelevisionLoaderLogic.ForeverSkipEpisodeAsync(IEpisodeTable episode) //even reruns can skip forever.
    {
        data.CurrentEpisode = episode;
        return data.ForeverSkipEpisodeAsync();
    }
    Task IBasicTelevisionLoaderLogic.ModifyHolidayAsync(IEpisodeTable episode, EnumTelevisionHoliday holiday)
    {
        data.CurrentEpisode = episode;
        return data.ModifyHolidayCategoryForEpisodeAsync(holiday);
    }
    //was going to not support it but decided that if somehow it happened, then go ahead and close out and go back in (even on firstrun shows).
    async Task IBasicTelevisionLoaderLogic.ReloadAppAsync(IEpisodeTable newEpisode)
    {
        //may have to rethink if one is youtube and the other is not.
        await InitializeEpisodeAsync(newEpisode); //i think.
        await data.ReloadAppAsync();
        //await AddToHistoryAsync(newEpisode);
    }

    Task IFirstRunTelevisionLoaderLogic.IntroBeginsAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode;
        return data.IntroBeginsAsync();
    }
    Task IFirstRunTelevisionLoaderLogic.ThemeSongOverAsync(IEpisodeTable episode)
    {
        data.CurrentEpisode = episode;
        return data.ThemeSongOverAsync();
    }
}