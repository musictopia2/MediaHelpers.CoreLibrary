namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.Movies;
public interface IRerunListMovieContext<M> : IStartMovieListContext<M>
    where M : class, IMainMovieTable
{
    
}