namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListFirstrunLogic(IFirstRunListTelevisionContext data) : ITelevisionListLogic
{
    Task<BasicList<IShowTable>> ITelevisionListLogic.GetShowListAsync()
    {
        return data.ListShowsAsync();
    }
}