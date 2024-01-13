namespace MediaHelpers.CoreLibrary.Video.Helpers;
public interface ITelevisionVideoLoader<T>
    where T: class, IEpisodeTable
{
    void ChoseEpisode(T episode);
}