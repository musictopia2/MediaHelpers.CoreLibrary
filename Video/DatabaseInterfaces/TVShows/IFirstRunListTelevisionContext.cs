namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IFirstRunListTelevisionContext : IStartListTelevisionContext
{
    Task FinishVideoFirstRunAsync();
    Task FinishVideoFirstRunAsync(int showID);
    Task IntroBeginsAsync();
    Task ThemeSongOverAsync();
    
}