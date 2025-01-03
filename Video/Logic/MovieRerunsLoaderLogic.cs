﻿namespace MediaHelpers.CoreLibrary.Video.Logic;
public class MovieRerunsLoaderLogic<M>(IRerunLoaderMovieContext<M> dats, IExit exit) : IRerunMovieLoaderLogic<M>
    where M : class, IMainMovieTable
{
    async Task IBasicMovieLoaderLogic<M>.DislikeMovieAsync(M selectedMovie)
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
    async Task IBasicMovieLoaderLogic<M>.FinishMovieAsync(M selectedMovie)
    {
        selectedMovie.ResumeAt = null; //must be here.  otherwise, autoresume does not work.
        await UpdateMovieAsync(selectedMovie);
        exit.ExitApp();
    }
    M IBasicMovieLoaderLogic<M>.GetChosenMovie()
    {
        if (mm1.MovieChosen is null)
        {
            throw new CustomBasicException("No movie chosen");
        }
        dats.PopulateChosenMovie(mm1.MovieChosen.Value);
        return dats.CurrentMovie!;
    }
    Task IBasicMovieLoaderLogic<M>.InitializeMovieAsync(M selectedMovie)
    {
        dats.CurrentMovie = selectedMovie;
        return dats.InitializeMovieAsync();
    }
    private Task UpdateMovieAsync(M selectedMovie)
    {
        dats.CurrentMovie = selectedMovie;
        return dats.UpdateMovieAsync();
    }
    Task IBasicMovieLoaderLogic<M>.UpdateMovieAsync(M selectedMovie)
    {
        return UpdateMovieAsync(selectedMovie);
    }
}