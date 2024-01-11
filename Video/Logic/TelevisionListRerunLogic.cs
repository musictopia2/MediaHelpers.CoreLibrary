namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListRerunLogic(IRerunListTelevisionContext data) : ITelevisionListLogic
{
    Task<BasicList<IShowTable>> ITelevisionListLogic.GetShowListAsync()
    {
        return data.ListShowsAsync();
    }
}