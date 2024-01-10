namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IStartListTelevisionContext : IStartBasicTelevisionContext
{
    
    Task<BasicList<IShowTable>> ListShowsAsync();
    Task<bool> HadPreviousEpisodeAsync();
    
}