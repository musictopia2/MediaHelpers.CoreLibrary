namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.Movies;
public interface IStartLoaderMovieContext<M>
    where M : class, IMainMovieTable
{
    M? CurrentMovie { get; set; }
    Task DislikeMovieAsync();
    Task UpdateMovieAsync(); //needs a way to update a movie.
}