namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IRerunLoaderTelevisionContext : IStartLoaderTelevisionContext
{

    //Task<BasicList<IEpisodeTable>> GetHolidayListAsync(EnumTelevisionHoliday currentHoliday);
    Task<IEpisodeTable> GetManuelEpisodeAsync(int showID, int episodeID); //reruns should not need this.
    Task TemporarilySkipEpisodeAsync();
    Task EndEpisodeAsync(); //if episode is needed, should be under CurrentEpisode.
    Task<bool> CanAutomaticallyGoToNextEpisodeAsync();
}