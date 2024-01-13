namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IRerunLoaderTelevisionContext<E> : IStartLoaderTelevisionContext<E>, IRerunStartTelevisionContext<E>
    where E : class, IEpisodeTable
{
    Task<IEpisodeTable> GetManuelEpisodeAsync(int showID, int episodeID);
    Task TemporarilySkipEpisodeAsync();
    Task EndEpisodeAsync(); //if episode is needed, should be under CurrentEpisode.
    Task<bool> CanAutomaticallyGoToNextEpisodeAsync();
}