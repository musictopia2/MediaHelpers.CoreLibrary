namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public abstract class BaseLocalMovieLoaderViewModel<M, T> : VideoMainLoaderViewModel<M>,
    IStartLoadingViewModel,
    IMovieLoaderViewModel
    where M : class, IMainMovieTable
    where T : class, IBasicMoviesModel, new()
{
    private readonly IFullVideoPlayer _player;
    private readonly IMovieLoaderLogic<M> _loader;
    private readonly IBasicMoviesRemoteControlHostService<T> _hostService;
    private readonly IExit _exit;
    private readonly ISystemError _error;
    public BaseLocalMovieLoaderViewModel(IFullVideoPlayer player,
        IMovieLoaderLogic<M> loader,
        MovieContainerClass<M> movieContainer,
        IBasicMoviesRemoteControlHostService<T> hostService,
        IExit exit,
        ISystemError error
        ) : base(player, error, exit)
    {
        _player = player;
        _loader = loader;
        _hostService = hostService;
        _exit = exit;
        _error = error;
        _hostService.NewClient = SendOtherDataAsync;
        _hostService.DislikeMovie = DislikeMovieAsync;
        if (mm1.MovieChosen.HasValue == false)
        {
            throw new CustomBasicException("No movie was chosen");
        }
        movieContainer.MovieChosen = _loader.GetChosenMovie();
        if (movieContainer.MovieChosen is null)
        {
            throw new CustomBasicException("There was no movie chosen.  Rethink");
        }
        SelectedItem = movieContainer.MovieChosen;
        if (CanInitializeRemoteControlAfterPlayerInit == false)
        {
            StartPossibleRemoteControl(); //means do here.
        }
    }
    protected async void StartPossibleRemoteControl()
    {
        await _hostService.InitializeAsync();
        await SendOtherDataAsync();
    }
    private async Task DislikeMovieAsync()
    {
        if (SelectedItem is null)
        {
            return;
        }
        await Execute.OnUIThreadAsync(async () =>
        {
            var tempItem = StopEpisode();
            await _loader.DislikeMovieAsync(tempItem);
            _exit.ExitApp();
        });
    }
    protected abstract bool CanInitializeRemoteControlAfterPlayerInit { get; }
    protected override Task SendOtherDataAsync()
    {
        T model = new()
        {
            Progress = ProgressText,
            MovieName = SelectedItem!.Title
        };
        return _hostService.SendProgressAsync(model);
    }
    public override async Task SaveProgressAsync()
    {
        try
        {
            if (SelectedItem!.ResumeAt.HasValue == false || SelectedItem.ResumeAt!.Value < VideoPosition)
            {
                SelectedItem.ResumeAt = VideoPosition;
            }
            await _loader.UpdateMovieAsync(SelectedItem);
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }

    }
    public override Task VideoFinishedAsync()
    {
        return _loader.FinishMovieAsync(SelectedItem!);
    }
    private async Task<int> ResumeAtAsync()
    {
        if (SelectedItem!.ResumeAt.HasValue == false)
        {
            return 0;
        }
        if (SelectedItem.ResumeAt!.Value == -1)
        {
            SelectedItem.ResumeAt = 0;
            await _loader.UpdateMovieAsync(SelectedItem);
            return 0;
        }
        return SelectedItem.ResumeAt.Value;
    }
    private int _secs;
    protected override async Task BeforePlayerInitAsync()
    {
        try
        {
            await base.BeforePlayerInitAsync();
            _secs = await ResumeAtAsync();
            if (_secs == 0 && SelectedItem!.Opening.HasValue == true)
            {
                _secs = SelectedItem.Opening!.Value;
            }
            VideoPath = SelectedItem!.FullPath();
            SelectedItem.LastWatched = DateOnly.FromDateTime(DateTime.Now);
            if (SelectedItem.ResumeAt.HasValue == false)
            {
                await _loader.UpdateMovieAsync(SelectedItem);
            }
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
    }
    protected override async Task AfterPlayerInitAsync()
    {
        try
        {
            await ProcessSkipsAsync();
            ResumeSecs = _secs;
            VideoPosition = _secs;
            if (CanInitializeRemoteControlAfterPlayerInit)
            {
                await _hostService.InitializeAsync();
            }
            await ShowVideoLoadedAsync();
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
    }
    private async Task ProcessSkipsAsync()
    {
        Player.AddScenesToSkip(SelectedItem!.Skips); //hopefully this simple.
        var tvLength = Player.Length();
        await CalculateDurationAsync(tvLength);
    }
    private async Task CalculateDurationAsync(int movieLength)
    {
        int newLength;
        if (SelectedItem!.Closing.HasValue == true)
        {
            newLength = SelectedItem.Closing!.Value;
        }
        else
        {
            newLength = movieLength;
        }
        VideoLength = newLength;
        await ShowVideoLoadedAsync();
    }
    public Action? StartLoadingPlayer { get; set; }
}