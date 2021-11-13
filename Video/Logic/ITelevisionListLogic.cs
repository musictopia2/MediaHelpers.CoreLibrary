namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface ITelevisionListLogic
{
    /// <summary>
    /// the view model is responsible for figuring out what was chosen.
    /// then this one is responsible for figuring out what episode it is so other actions can be done.
    /// if none, then this should show the messagebox and close the app.
    /// </summary>
    /// <param name="selectedItem"></param>
    /// <returns></returns>
    Task<IEpisodeTable?> GetNextEpisodeAsync(IShowTable selectedItem);
    Task<BasicList<IShowTable>> GetShowListAsync();
}