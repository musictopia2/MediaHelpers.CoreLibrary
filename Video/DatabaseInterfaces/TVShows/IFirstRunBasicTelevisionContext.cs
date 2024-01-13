namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IFirstRunBasicTelevisionContext<E> : IStartBasicTelevisionContext<E>
    where E : class, IEpisodeTable
{
    Task FinishVideoFirstRunAsync();
    Task FinishVideoFirstRunAsync(int showID);
}