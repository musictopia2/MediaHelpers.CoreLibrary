namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionFirstrunLoaderLogic<E>(IFirstRunLoaderTelevisionContext<E> data) : IFirstRunTelevisionLoaderLogic<E>
    where E : class, IEpisodeTable
{
    async Task IBasicTelevisionLoaderLogic<E>.InitializeEpisodeAsync(E episode)
    {
        await InitializeEpisodeAsync(episode);
    }
    //async Task ITelevisionLoaderLogic.AddToHistoryAsync(IEpisodeTable episode)
    //{
    //    await AddToHistoryAsync(episode);
    //}
    private async Task InitializeEpisodeAsync(E episode)
    {
        data.CurrentEpisode = episode;
        await data.InitializeEpisodeAsync();
        //await _data.AddFirstRunViewHistoryAsync();
    }
    async Task IBasicTelevisionLoaderLogic<E>.EndTVEpisodeEarlyAsync(E episode)
    {
        await EndEpisodeAsync(episode);
    }

    private async Task EndEpisodeAsync(E episode)
    {
        episode.ResumeAt = null;
        data.CurrentEpisode = episode;
        await data.FinishVideoFirstRunAsync(); //should not need a way to be able to end episode since it most likely will close out.
    }
    async Task IBasicTelevisionLoaderLogic<E>.FinishTVEpisodeAsync(E episode)
    {
        await EndEpisodeAsync(episode);
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
    Task IBasicTelevisionLoaderLogic<E>.ForeverSkipEpisodeAsync(E episode) //even reruns can skip forever.
    {
        data.CurrentEpisode = episode;
        return data.ForeverSkipEpisodeAsync();
    }
    Task IBasicTelevisionLoaderLogic<E>.EditEpisodeLaterAsync(E episode)
    {
        data.CurrentEpisode = episode;
        return data.EditEpisodeLaterAsync();
    }
    Task IBasicTelevisionLoaderLogic<E>.ModifyHolidayAsync(E episode, EnumTelevisionHoliday holiday)
    {
        data.CurrentEpisode = episode;
        return data.ModifyHolidayCategoryForEpisodeAsync(holiday);
    }
    Task IFirstRunTelevisionLoaderLogic<E>.IntroBeginsAsync(E episode)
    {
        data.CurrentEpisode = episode;
        return data.IntroBeginsAsync();
    }
    Task IFirstRunTelevisionLoaderLogic<E>.ThemeSongOverAsync(E episode)
    {
        data.CurrentEpisode = episode;
        return data.ThemeSongOverAsync();
    }

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