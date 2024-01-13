namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IFirstRunListTelevisionContext<E> : IStartListTelevisionContext<E>, IFirstRunBasicTelevisionContext<E>
    where E : class, IEpisodeTable
{
    
    
}