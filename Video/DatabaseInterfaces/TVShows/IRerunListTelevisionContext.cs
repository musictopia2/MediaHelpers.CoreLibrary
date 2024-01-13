namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IRerunListTelevisionContext<E> : IStartListTelevisionContext<E>
    where E : class,  IEpisodeTable
{
    //only useful to make it easier to know what is being asked for.
}