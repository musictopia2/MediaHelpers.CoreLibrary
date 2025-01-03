﻿namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface IMovieListLogic<M>
    where M : class, IMainMovieTable
{
    Task<BasicList<M>> GetMovieListAsync();
    /// <summary>
    /// this returns the last movie.  could even be null if there was no last movie watched.
    /// </summary>
    /// <param name="movies"></param>
    /// <returns></returns>
    M? GetLastMovie(BasicList<M> movies);
}