namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListFirstrunLogic<E>(IFirstRunListTelevisionContext<E> data) : ITelevisionListLogic
    where E : class,  IEpisodeTable
{
    Task<BasicList<IShowTable>> ITelevisionListLogic.GetShowListAsync()
    {
        return data.ListShowsAsync();
    }
}