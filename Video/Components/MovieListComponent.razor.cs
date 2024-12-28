namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class MovieListComponent<M>
    where M : class, IMainMovieTable
{
    [Inject]
    private MovieListViewModel<M>? DataContext { get; set; }
    [Inject]
    private IMovieVideoLoader<M>? Loader { get; set; }
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