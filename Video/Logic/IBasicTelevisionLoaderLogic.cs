namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface IBasicTelevisionLoaderLogic<E>
    where E: class, IEpisodeTable
{

    //better to assume it all requires generics.

    /// <summary>
    /// this needs to not only do a simple function but also do other calculations.
    /// this is used to autoresume the video if needed.
    /// </summary>
    /// <param name="episode"></param>
    /// <returns></returns>
    int GetSeconds(E episode);
    Task UpdateTVShowProgressAsync(E episode, int position);
    /// <summary>
    /// this also needs to close out of the program as well.
    /// </summary>
    /// <param name="episode"></param>
    /// <returns></returns>
    Task FinishTVEpisodeAsync(E episode);
    Task EndTVEpisodeEarlyAsync(E episode);
    Task InitializeEpisodeAsync(E episode);
    //Task AddToHistoryAsync(IEpisodeTable episode);
    /// <summary>
    /// this was used because there is a known problem that when skipping episode, that the skip information gets corrupted.  one solution is to close out and go back in again.
    /// either create the app to reload.  otherwise, you can simply close out (then you have to manually reopn again).  if you do nothing, then you have to manually close out as well.
    /// </summary>
    /// <param name="newEpisode"></param>
    /// <returns></returns>
    Task ReloadAppAsync(E newEpisode);
    Task ForeverSkipEpisodeAsync(E episode);
    Task EditEpisodeLaterAsync(E episode);

    //not everything can temporarily skip though.

    //Task TemporarilySKipEpisodeAsync(IEpisodeTable episode);
    Task ModifyHolidayAsync(E episode, EnumTelevisionHoliday holiday);
    //since needs to know to populate all the way as needed.
    E GetChosenEpisode(); //since i have the helpers, can access from that now.
}