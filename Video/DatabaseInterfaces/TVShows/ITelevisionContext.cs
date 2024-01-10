namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface ITelevisionContext
{
    int Seconds { get; set; }
    IEpisodeTable CurrentEpisode { get; set; }
    //if weights are needed, something else has to be created for it.

    Task<BasicList<IEpisodeTable>> GetHolidayListAsync(EnumTelevisionHoliday currentHoliday);

    Task<bool> HadPreviousEpisodeAsync();
    Task InitializeFirstRunEpisodeAsync();
    Task InitializeRerunEpisodeAsync();
    //Task AddFirstRunViewHistoryAsync();
    //Task AddReRunViewHistory();
    Task<BasicList<IShowTable>> ListShowsAsync(EnumTelevisionCategory televisionCategory);

    //BasicList<IShowTable> ListShows(EnumTelevisionCategory televisionCategory);
    //if there was a previous show, this is responsible for getting the show as well.

    //void LoadResumeTVEpisodeForReruns();
    Task UpdateEpisodeAsync();
    Task FinishVideoFirstRunAsync(); 
    Task FinishVideoFirstRunAsync(int showID);
    IEpisodeTable? GetNextFirstRunEpisode(int showID);
    IEpisodeTable GenerateNewRerunEpisode(int showID);
    /// <summary>
    /// this is needed so if you need to manually select an episode for testing or other purposes, that can be done.
    /// looks like needs showid.  otherwise, may be unable to get the full path.
    /// </summary>
    /// <param name="episodeID"></param>
    /// <returns></returns>
    IEpisodeTable GetManuelEpisode(int showID, int episodeID);
    Task ForeverSkipEpisodeAsync();
    Task TemporarilySkipEpisodeAsync(); //this means will do so temporarily.
    Task ModifyHolidayCategoryForEpisodeAsync(EnumTelevisionHoliday holiday);
    void ReloadApp();
}