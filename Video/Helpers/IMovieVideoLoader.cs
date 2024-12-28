namespace MediaHelpers.CoreLibrary.Video.Helpers;
public interface IMovieVideoLoader<M>
    where M : class, IMainMovieTable
{
    void ChoseMovie(M movie);
}