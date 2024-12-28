namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.Movies;
public interface IStartMovieListContext<M>
    where M : class, IMainMovieTable
{
    Task<BasicList<M>> GetMovieListAsync(bool isChristmas);
}