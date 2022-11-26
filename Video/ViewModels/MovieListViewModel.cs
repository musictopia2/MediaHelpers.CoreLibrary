namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class MovieListViewModel : MainVideoListViewModel<IMainMovieTable>
{
    private readonly IMovieListLogic _movieListLogic;
    private readonly IMessageBox _message;
    public MovieListViewModel(IMovieListLogic movieListLogic, IMessageBox message)
    {
        _movieListLogic = movieListLogic;
        _message = message;
    }
    public EnumMovieSelectionMode SelectionMode { get; set; } = EnumMovieSelectionMode.AlreadyWatched;
    private IMainMovieTable? _lastMovie;
    public async Task GetMovieListAsync()
    {
        VideoList = await _movieListLogic.GetMovieListAsync(SelectionMode);
        _lastMovie = _movieListLogic.GetLastMovie(VideoList);
        CanAutoResume = _lastMovie != null;
        FocusCombo?.Invoke();
    }
    public bool CanAutoResume { get; private set; }
    public void AutoResume()
    {
        if (_lastMovie == null)
        {
            throw new CustomBasicException("Cannot autoresume movie because last movie was nothing.  Rethink");
        }
        SelectedItem = _lastMovie;
    }
    public bool CanShowLastWatched => CanChooseVideo;
    public async Task ShowLastWatchedAsync()
    {
        if (SelectedItem == null)
        {
            throw new CustomBasicException("You never selected the movie to show the details.  Rethink");
        }
        if (SelectedItem.LastWatched.HasValue == false)
        {
            await _message.ShowMessageAsync("You never watched this movie before");
            return;
        }
        await _message.ShowMessageAsync($"The last time you watched the movie was {SelectedItem.LastWatched!.Value}");
    }
    public bool CanShowInfoLast => CanAutoResume;
    public async Task ShowInfoLastAsync()
    {
        if (_lastMovie == null)
        {
            throw new CustomBasicException("Cannot show last info because was nothing.  Rethink");
        }
        await _message.ShowMessageAsync($"The Last Movie You Need To Watch Was {_lastMovie.Title}");
    }
    public override async Task InitAsync()
    {
        await GetMovieListAsync();
    }
}