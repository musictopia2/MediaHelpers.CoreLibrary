using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace MediaHelpers.CoreLibrary.Video.Logic;
public class MovieListLogic : IMovieListLogic
{
    private readonly IMovieContext _dats;
    private readonly IDateOnlyPicker _picker;
    public MovieListLogic(IMovieContext dats, IDateOnlyPicker picker)
    {
        _dats = dats;
        _picker = picker;
    }
    IMainMovieTable? IMovieListLogic.GetLastMovie(BasicList<IMainMovieTable> movies)
    {
        var tempList = movies.GetConditionalItems(Items => Items.LastWatched.HasValue == true);
        tempList.Sort(comparison: (xx, yy) =>
        {
            return yy.LastWatched!.Value.CompareTo(xx.LastWatched!.Value);
        });
        IMainMovieTable output = tempList.Find(Items => Items.ResumeAt.HasValue == true
        || Items.Opening.HasValue == false && Items.Closing.HasValue == false)!;
        return output;
    }
    Task<BasicList<IMainMovieTable>> IMovieListLogic.GetMovieListAsync(EnumMovieSelectionMode selectionMode)
    {
        DateOnly thisDate = _picker.GetCurrentDate; //has to use picker so i can mock it if necessary
        bool isChristmas = thisDate.IsBetweenThanksgivingAndChristmas();
        return Task.FromResult(_dats.GetMovieList(selectionMode, isChristmas));
    }
}
