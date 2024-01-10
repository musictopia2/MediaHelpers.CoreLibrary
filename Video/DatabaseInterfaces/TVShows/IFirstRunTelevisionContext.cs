namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IFirstRunTelevisionContext : IStartTelevisionContext
{
    Task FinishVideoFirstRunAsync();
    Task FinishVideoFirstRunAsync(int showID);
    Task IntroBeginsAsync();
    Task ThemeSongOverAsync();
    //extra remote control functions will be here.
}