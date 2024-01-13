namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListRerunLogic<E>(IRerunListTelevisionContext<E> data) : ITelevisionListLogic
    where E : class, IEpisodeTable
{
    Task<BasicList<IShowTable>> ITelevisionListLogic.GetShowListAsync()
    {
        return data.ListShowsAsync();
    }
}