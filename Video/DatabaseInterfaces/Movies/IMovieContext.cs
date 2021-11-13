namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.Movies;
public interface IMovieContext
{
    Task AddHistoryAsync(IMainMovieTable thisMovie);
    BasicList<IMainMovieTable> GetMovieList(EnumMovieSelectionMode selectionMode, bool isChristmas);
    Task UpdateMovieAsync(IMainMovieTable movie);
    Task DislikeMovieAsync(IMainMovieTable movie);
}