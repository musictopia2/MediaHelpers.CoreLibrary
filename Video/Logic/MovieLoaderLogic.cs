namespace MediaHelpers.CoreLibrary.Video.Logic;
public class MovieLoaderLogic : IMovieLoaderLogic
{
    private readonly IMovieContext _dats;
    private readonly IExit _exit;
    public MovieLoaderLogic(IMovieContext dats, IExit exit)
    {
        _dats = dats;
        _exit = exit;
    }
    Task IMovieLoaderLogic.AddToHistoryAsync(IMainMovieTable selectedMovie)
    {
        return _dats.AddHistoryAsync(selectedMovie);
    }
    Task IMovieLoaderLogic.DislikeMovieAsync(IMainMovieTable selectedMovie)
    {
        return _dats.DislikeMovieAsync(selectedMovie);
    }
    async Task IMovieLoaderLogic.FinishMovieAsync(IMainMovieTable selectedMovie)
    {
        selectedMovie.ResumeAt = null;
        await UpdateMovieAsync(selectedMovie);
        _exit.ExitApp();
    }
    private Task UpdateMovieAsync(IMainMovieTable selectedMovie)
    {
        return _dats.UpdateMovieAsync(selectedMovie);
    }
    Task IMovieLoaderLogic.UpdateMovieAsync(IMainMovieTable selectedMovie)
    {
        return UpdateMovieAsync(selectedMovie);
    }
}