namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface INextEpisodeLogic
{
    /// <summary>
    /// the view model is responsible for figuring out what was chosen.
    /// then this one is responsible for figuring out what episode it is so other actions can be done.
    /// if none, then this should show the messagebox and close the app.
    /// </summary>
    /// <param name="selectedItem"></param>
    /// <returns></returns>
    Task<IEpisodeTable?> GetNextEpisodeAsync(IShowTable selectedItem);
    //can no longer be part of the television list.  since the loader can't know anything about the list anymore.
}