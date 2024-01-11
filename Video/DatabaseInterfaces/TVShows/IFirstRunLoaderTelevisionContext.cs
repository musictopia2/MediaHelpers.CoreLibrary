namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IFirstRunLoaderTelevisionContext : IStartLoaderTelevisionContext, IFirstRunBasicTelevisionContext
{
    
    Task IntroBeginsAsync();
    Task ThemeSongOverAsync();
    //extra remote control functions will be here.
}