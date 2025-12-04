namespace MediaHelpers.CoreLibrary.Video.Logic;
public class MovieRerunsListLogic<M>(IRerunListMovieContext<M> dats, IDateOnlyPicker picker) : IMovieListLogic<M>
    where M : class, IMainMovieTable
{
    M? IMovieListLogic<M>.GetLastMovie(BasicList<M> movies)
    {
        var tempList = movies.GetConditionalItems(x => x.LastWatched.HasValue == true);
        tempList.Sort(comparison: (xx, yy) =>
        {
            return yy.LastWatched!.Value.CompareTo(xx.LastWatched!.Value);
        });
        M output = tempList.Find(x => x.ResumeAt.HasValue == true)!;
        return output;
    }
    async Task<BasicList<M>> IMovieListLogic<M>.GetMovieListAsync()
    {
        DateOnly thisDate = picker.GetCurrentDate; //has to use picker so i can mock it if necessary
        bool isChristmas = thisDate.IsBetweenThanksgivingAndChristmas;
        return await dats.GetMovieListAsync(isChristmas);
    }
}