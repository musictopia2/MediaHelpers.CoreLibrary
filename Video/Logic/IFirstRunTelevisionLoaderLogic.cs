namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface IFirstRunTelevisionLoaderLogic : IBasicTelevisionLoaderLogic
{
    //has to figure out what is needed for first run stuff here.
    Task IntroBeginsAsync(IEpisodeTable episode);
    Task ThemeSongOverAsync(IEpisodeTable episode);
}