namespace MediaHelpers.CoreLibrary.Video.Logic;
/// <summary>
/// this is used in cases where you chose a movie and it loads the movie.
/// shows all the processes that get called when you are watching a movie.
/// </summary>
public interface IMovieLoaderLogic<M>
    where M: class, IMainMovieTable
{
    Task FinishMovieAsync(M selectedMovie);
    Task UpdateMovieAsync(M selectedMovie);
    Task DislikeMovieAsync(M selectedMovie);
    
}