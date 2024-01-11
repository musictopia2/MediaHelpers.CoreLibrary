namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IStartLoaderTelevisionContext : IStartBasicTelevisionContext
{
    //anything that is needed on both is here.
    int Seconds { get; set; }
    Task ReloadAppAsync();
    Task UpdateEpisodeAsync();
    Task InitializeEpisodeAsync();
    Task ModifyHolidayCategoryForEpisodeAsync(EnumTelevisionHoliday holiday); //i think
    Task ForeverSkipEpisodeAsync();
    void PopulateChosenEpisode(int episodeID);
}