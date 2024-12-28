namespace MediaHelpers.CoreLibrary.Video.Helpers;
public class MovieContainerClass<M>
    where M : class, IMainMovieTable
{
    public M? MovieChosen { get; set; }
}