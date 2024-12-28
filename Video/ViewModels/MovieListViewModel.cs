namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class MovieListViewModel<M>(IMovieListLogic<M> movieListLogic, IMessageBox message) : MainVideoListViewModel<M>
    where M : class, IMainMovieTable
{
    private M? _lastMovie;
    public async Task GetMovieListAsync()
    {
        VideoList = await movieListLogic.GetMovieListAsync();
        _lastMovie = movieListLogic.GetLastMovie(VideoList);
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
            await message.ShowMessageAsync("You never watched this movie before");
            return;
        }
        await message.ShowMessageAsync($"The last time you watched the movie was {SelectedItem.LastWatched!.Value}");
    }
    public bool CanShowInfoLast => CanAutoResume;
    public async Task ShowInfoLastAsync()
    {
        if (_lastMovie == null)
        {
            throw new CustomBasicException("Cannot show last info because was nothing.  Rethink");
        }
        await message.ShowMessageAsync($"The Last Movie You Need To Watch Was {_lastMovie.Title}");
    }
    public override async Task InitAsync()
    {
        await GetMovieListAsync();
    }
}