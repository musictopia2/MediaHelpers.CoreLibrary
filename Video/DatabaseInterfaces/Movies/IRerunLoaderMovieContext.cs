namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.Movies;
public interface IRerunLoaderMovieContext<M> : IStartLoaderMovieContext<M>
    where M : class, IMainMovieTable
{
    Task EditMovieLaterAsync();
}