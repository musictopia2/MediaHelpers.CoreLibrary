namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface ITelevisionShellLogic
{
    Task<IEpisodeTable?> GetPreviousShowAsync(); //if it returns nothing, then there was no previous show. 
}