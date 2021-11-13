namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface IMovieListLogic
{
    Task<BasicList<IMainMovieTable>> GetMovieListAsync(EnumMovieSelectionMode selectionMode);
    /// <summary>
    /// this returns the last movie.  could even be null if there was no last movie watched.
    /// </summary>
    /// <param name="movies"></param>
    /// <returns></returns>
    IMainMovieTable? GetLastMovie(BasicList<IMainMovieTable> movies);
}