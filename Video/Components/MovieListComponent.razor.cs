namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class MovieListComponent
{
    [Inject]
    private MovieListViewModel? DataContext { get; set; }
    [Inject]
    private IMovieVideoLoader? Loader { get; set; }
    private static string GetMargins => "margin-bottom: 5px";
    private static void Refresh() //needed so it can refresh its state.
    {

    }
    private void DoChooseMovie()
    {
        if (DataContext!.SelectedItem is null)
        {
            throw new CustomBasicException("No movie was chosen");
        }
        Loader!.ChoseMovie(DataContext.SelectedItem);
    }
    private void AutoResume()
    {
        DataContext!.AutoResume();
        DoChooseMovie();
    }
}