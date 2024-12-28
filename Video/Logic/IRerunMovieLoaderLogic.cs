namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface IRerunMovieLoaderLogic<M> : IMovieLoaderLogic<M>
    where M : class, IMainMovieTable
{
    //since i have finishmovie, does not matter if its early.
    Task EditMovieLaterAsync(M selectedMovie);
}