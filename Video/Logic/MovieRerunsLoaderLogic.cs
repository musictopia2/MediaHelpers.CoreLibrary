namespace MediaHelpers.CoreLibrary.Video.Logic;
public class MovieRerunsLoaderLogic<M>(IRerunLoaderMovieContext<M> dats, IExit exit) : IRerunMovieLoaderLogic<M>
    where M : class, IMainMovieTable
{
    async Task IMovieLoaderLogic<M>.DislikeMovieAsync(M selectedMovie)
    {
        dats.CurrentMovie = selectedMovie;
        await dats.DislikeMovieAsync();
        exit.ExitApp();
    }
    async Task IRerunMovieLoaderLogic<M>.EditMovieLaterAsync(M selectedMovie)
    {
        dats.CurrentMovie = selectedMovie;
        await dats.EditMovieLaterAsync();
        exit.ExitApp();
    }
    async Task IMovieLoaderLogic<M>.FinishMovieAsync(M selectedMovie)
    {
        await UpdateMovieAsync(selectedMovie);
        exit.ExitApp();
    }
    private Task UpdateMovieAsync(M selectedMovie)
    {
        dats.CurrentMovie = selectedMovie;
        selectedMovie.ResumeAt = null;
        return dats.UpdateMovieAsync();
    }
    Task IMovieLoaderLogic<M>.UpdateMovieAsync(M selectedMovie)
    {
        return UpdateMovieAsync(selectedMovie);
    }
}