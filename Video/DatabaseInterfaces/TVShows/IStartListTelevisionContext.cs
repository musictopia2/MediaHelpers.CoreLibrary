namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IStartListTelevisionContext<E> : IStartBasicTelevisionContext<E>
    where E : class, IEpisodeTable
{
    
    Task<BasicList<IShowTable>> ListShowsAsync();
    Task<bool> HadPreviousEpisodeAsync();
    
}