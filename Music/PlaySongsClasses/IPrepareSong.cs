namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
public interface IPrepareSong
{
    /// <summary>
    /// the action sent is code that will run when this method invokes the action.
    /// usually will show that properties has changed so ui can do what it needs to do.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    Task<bool> PrepareSongAsync(IBaseSong currentSong, int resumeAt);
}