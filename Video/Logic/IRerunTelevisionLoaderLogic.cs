namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface IRerunTelevisionLoaderLogic<E> : IBasicTelevisionLoaderLogic<E>
    where E : class, IEpisodeTable
{
    Task TemporarilySKipEpisodeAsync(E episode);
    Task<bool> CanGoToNextEpisodeAsync();
    //this means the firstrun can have other functions for that remote control.
}