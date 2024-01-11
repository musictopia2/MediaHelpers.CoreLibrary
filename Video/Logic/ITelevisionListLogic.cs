namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface ITelevisionListLogic
{
    
    Task<BasicList<IShowTable>> GetShowListAsync();
}