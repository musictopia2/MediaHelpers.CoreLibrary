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
    /// <summary>
    /// this was used because there is a known problem that when skipping episode, that the skip information gets corrupted.  one solution is to close out and go back in again.
    /// either create the app to reload.  otherwise, you can simply close out (then you have to manually reopn again).  if you do nothing, then you have to manually close out as well.
    /// </summary>
    /// <param name="newEpisode"></param>
    /// <returns></returns>
    Task ReloadAppAsync(IEpisodeTable newEpisode);
    Task ForeverSkipEpisodeAsync(IEpisodeTable episode);
    Task ModifyHolidayAsync(IEpisodeTable episode, EnumTelevisionHoliday holiday);
}