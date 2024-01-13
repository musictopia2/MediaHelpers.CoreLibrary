namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface ITelevisionShellLogic<E>
    where E: class, IEpisodeTable
{
    Task<E?> GetPreviousEpisodeAsync(); //if it returns nothing, then there was no previous episode 
}