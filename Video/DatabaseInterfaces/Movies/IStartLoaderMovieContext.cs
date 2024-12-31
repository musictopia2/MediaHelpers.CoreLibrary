namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.Movies;
public interface IStartLoaderMovieContext<M>
    where M : class, IMainMovieTable
{
    M? CurrentMovie { get; set; }
    void PopulateChosenMovie(int movieID);
    Task DislikeMovieAsync();
    Task UpdateMovieAsync(); //needs a way to update a movie.
    Task InitializeMovieAsync();
}