namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IFirstRunLoaderTelevisionContext<E> : IStartLoaderTelevisionContext<E>, IFirstRunBasicTelevisionContext<E>
    where E : class, IEpisodeTable
{
    
    Task IntroBeginsAsync();
    Task ThemeSongOverAsync();
    //extra remote control functions will be here.
}