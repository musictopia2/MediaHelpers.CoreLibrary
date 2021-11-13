namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface ITelevisionLoaderLogic
{
    /// <summary>
    /// this needs to not only do a simple function but also do other calculations.
    /// this is used to autoresume the video if needed.
    /// </summary>
    /// <param name="episode"></param>
    /// <returns></returns>
    int GetSeconds(IEpisodeTable episode);
    Task UpdateTVShowProgressAsync(IEpisodeTable episode, int position);
    /// <summary>
    /// this also needs to close out of the program as well.
    /// </summary>
    /// <param name="episode"></param>
    /// <returns></returns>
    Task FinishTVEpisodeAsync(IEpisodeTable episode);
    Task EndTVEpisodeEarlyAsync(IEpisodeTable episode);
    Task AddToHistoryAsync(IEpisodeTable episode);
    Task ForeverSkipEpisodeAsync(IEpisodeTable episode);
    Task ModifyHolidayAsync(IEpisodeTable episode, EnumTelevisionHoliday holiday);
}