namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface ITelevisionContext
{
    int Seconds { get; set; }
    IEpisodeTable CurrentEpisode { get; set; }
    BasicList<IEpisodeTable> GetHolidayList(EnumTelevisionHoliday currentHoliday, int currentWeight);
    bool HadPreviousShow();
    Task InitializeFirstRunEpisodeAsync();
    Task InitializeRerunEpisodeAsync();
    //Task AddFirstRunViewHistoryAsync();
    //Task AddReRunViewHistory();
    BasicList<IShowTable> ListShows(EnumTelevisionCategory televisionCategory);
    void LoadResumeTVEpisodeForReruns();
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