namespace MediaHelpers.CoreLibrary.Video.Helpers;
public class TelevisionContainerClass<E>
    where E : class, IEpisodeTable
{
    public E? EpisodeChosen { get; set; }
}