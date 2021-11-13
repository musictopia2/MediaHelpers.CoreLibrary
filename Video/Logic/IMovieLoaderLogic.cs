namespace MediaHelpers.CoreLibrary.Video.Logic;
/// <summary>
/// this is used in cases where you chose a movie and it loads the movie.
/// shows all the processes that get called when you are watching a movie.
/// </summary>
public interface IMovieLoaderLogic
{
    Task FinishMovieAsync(IMainMovieTable selectedMovie);
    Task UpdateMovieAsync(IMainMovieTable selectedMovie);
    Task AddToHistoryAsync(IMainMovieTable selectedMovie);
    Task DislikeMovieAsync(IMainMovieTable selectedMovie);
}