namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IStartTelevisionContext
{
    //anything that is needed on both is here.
    int Seconds { get; set; }
    IEpisodeTable CurrentEpisode { get; set; }
    Task ReloadAppAsync();
    Task UpdateEpisodeAsync();
    Task<BasicList<IShowTable>> ListShowsAsync();
    Task<bool> HadPreviousEpisodeAsync();
    Task InitializeEpisodeAsync();
    Task<IEpisodeTable?> GetNextEpisodeAsync(int showID);
    Task ModifyHolidayCategoryForEpisodeAsync(EnumTelevisionHoliday holiday); //i think
    Task ForeverSkipEpisodeAsync();
}