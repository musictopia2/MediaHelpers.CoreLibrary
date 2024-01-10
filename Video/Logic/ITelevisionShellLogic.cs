namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface ITelevisionShellLogic
{
    Task<IEpisodeTable?> GetPreviousEpisodeAsync(); //if it returns nothing, then there was no previous episode 
}