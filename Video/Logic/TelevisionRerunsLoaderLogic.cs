using System.Net.NetworkInformation;

namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionRerunsLoaderLogic(ITelevisionContext data, IExit exit) : ITelevisionLoaderLogic
{
    //async Task ITelevisionLoaderLogic.AddToHistoryAsync(IEpisodeTable episode)
    //{
    //    await AddToHistoryAsync(episode);
    //}
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
        exit.ExitApp();
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
    /// this will add to history and make the context for television decide how to reload again
    /// </summary>
    /// <param name="newEpisode">this is the new episode chosen</param>
    /// <returns></returns>
    async Task ITelevisionLoaderLogic.ReloadAppAsync(IEpisodeTable newEpisode)
    {
        await InitializeEpisodeAsync(newEpisode);
        data.ReloadApp();
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
}