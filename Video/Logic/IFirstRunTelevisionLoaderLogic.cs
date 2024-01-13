namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface IFirstRunTelevisionLoaderLogic<E> : IBasicTelevisionLoaderLogic<E>
    where E : class, IEpisodeTable
{
    //has to figure out what is needed for first run stuff here.
    Task IntroBeginsAsync(E episode);
    Task ThemeSongOverAsync(E episode);
}