namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IRerunLoaderTelevisionContext : IStartLoaderTelevisionContext
{
    Task<IEpisodeTable> GetManuelEpisodeAsync(int showID, int episodeID);
    Task TemporarilySkipEpisodeAsync();
    Task EndEpisodeAsync(); //if episode is needed, should be under CurrentEpisode.
    Task<bool> CanAutomaticallyGoToNextEpisodeAsync();
}