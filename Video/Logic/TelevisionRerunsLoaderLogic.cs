namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionRerunsLoaderLogic<E>(IRerunLoaderTelevisionContext<E> data) : IRerunTelevisionLoaderLogic<E>
    where E : class, IEpisodeTable
{
    async Task IBasicTelevisionLoaderLogic<E>.EndTVEpisodeEarlyAsync(E episode)
    {
        await FinishEpisodeAsync(episode);
    }
    private async Task FinishEpisodeAsync(E episode)
    {
        episode.ResumeAt = null;
        data.CurrentEpisode = episode;
        await data.UpdateEpisodeAsync();
    }
    async Task IBasicTelevisionLoaderLogic<E>.FinishTVEpisodeAsync(E episode)
    {
        await FinishEpisodeAsync(episode);
        await data.EndEpisodeAsync(); //may reopen for another episode (depends)
    }
    Task IBasicTelevisionLoaderLogic<E>.ForeverSkipEpisodeAsync(E episode)
    {
        data.CurrentEpisode = episode; //just in case.
        return data.ForeverSkipEpisodeAsync();
    }
    Task IBasicTelevisionLoaderLogic<E>.EditEpisodeLaterAsync(E episode)
    {
        data.CurrentEpisode = episode;
        return data.EditEpisodeLaterAsync();
    }
    int IBasicTelevisionLoaderLogic<E>.GetSeconds(E episode)
    {
        return episode.GetSeconds(data);
    }
    Task IBasicTelevisionLoaderLogic<E>.UpdateTVShowProgressAsync(E episode, int position)
    {
        data.CurrentEpisode = episode;
        data.Seconds = position;
        return Task.CompletedTask;
    }
    Task IBasicTelevisionLoaderLogic<E>.ModifyHolidayAsync(E episode, EnumTelevisionHoliday holiday)
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
    async Task IBasicTelevisionLoaderLogic<E>.ReloadAppAsync(E newEpisode)
    {
        await InitializeEpisodeAsync(newEpisode);
        await data.ReloadAppAsync();
    }
    async Task IBasicTelevisionLoaderLogic<E>.InitializeEpisodeAsync(E episode)
    {
        await InitializeEpisodeAsync(episode);
    }
    private async Task InitializeEpisodeAsync(E episode)
    {
        data.CurrentEpisode = episode;
        await data.InitializeEpisodeAsync();
    }

    async Task IRerunTelevisionLoaderLogic<E>.TemporarilySKipEpisodeAsync(E episode)
    {
        data.CurrentEpisode = episode;
        await data.TemporarilySkipEpisodeAsync();
    }

    Task<bool> IRerunTelevisionLoaderLogic<E>.CanGoToNextEpisodeAsync()
    {
        return data.CanAutomaticallyGoToNextEpisodeAsync();
    }
    //for now, just have repeating code.  may rethink later.
    E IBasicTelevisionLoaderLogic<E>.GetChosenEpisode()
    {
        if (ee1.EpisodeChosen is null)
        {
            throw new CustomBasicException("Needs a chosen episode");
        }
        data.PopulateChosenEpisode(ee1.EpisodeChosen.Value);
        if (data.CurrentEpisode is null)
        {
            throw new CustomBasicException("No episode found");
        }
        return data.CurrentEpisode;
    }

    
}