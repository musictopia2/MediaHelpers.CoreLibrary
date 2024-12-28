namespace MediaHelpers.CoreLibrary.Video.Logic;
public class MovieRerunsLoaderLogic<M>(IRerunLoaderMovieContext<M> dats, IExit exit) : IRerunMovieLoaderLogic<M>
    where M : class, IMainMovieTable
{
    Task IMovieLoaderLogic<M>.DislikeMovieAsync(M selectedMovie)
    {
        dats.CurrentMovie = selectedMovie;
        return dats.DislikeMovieAsync();
    }
    Task IRerunMovieLoaderLogic<M>.EditMovieLaterAsync(M selectedMovie)
    {
        dats.CurrentMovie = selectedMovie;
        return dats.EditMovieLaterAsync();
    }
    async Task IMovieLoaderLogic<M>.FinishMovieAsync(M selectedMovie)
    {
        selectedMovie.ResumeAt = null;
        await UpdateMovieAsync(selectedMovie);
        exit.ExitApp();
    }
    private Task UpdateMovieAsync(M selectedMovie)
    {
        selectedMovie.ResumeAt = null;
        return dats.UpdateMovieAsync();
    }
    Task IMovieLoaderLogic<M>.UpdateMovieAsync(M selectedMovie)
    {
        return UpdateMovieAsync(selectedMovie);
    }
}