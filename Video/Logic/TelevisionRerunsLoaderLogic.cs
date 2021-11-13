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
        _data.CurrentEpisode = episode;
        await _data.AddReRunViewHistory();
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
}