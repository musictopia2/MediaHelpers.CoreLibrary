namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class MovieLoaderViewModel : VideoMainLoaderViewModel<IMainMovieTable>
{
    private readonly IFullVideoPlayer _player;
    private readonly IMovieLoaderLogic _loader;
    private readonly IMoviesRemoteControlHostService _hostService;
    private readonly IExit _exit;
    private readonly ISystemError _error;
    public MovieLoaderViewModel(IFullVideoPlayer player,
        IMovieLoaderLogic loader,
        MovieContainerClass movieContainer,
        IMoviesRemoteControlHostService hostService,
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

        _hostService.DislikeMovie = async () =>
        {
            await DislikeMovieAsync();
        };

        SelectedItem = movieContainer.MovieChosen;
    }
    private async Task DislikeMovieAsync()
    {
        if (SelectedItem is null)
        {
            return;
        }
        _player.StopPlay();
        await _loader.DislikeMovieAsync(SelectedItem);
        _exit.ExitApp();
    }
    protected override Task SendOtherDataAsync()
    {
        return _hostService.SendProgressAsync(new MoviesModel(SelectedItem!.Title, ProgressText));
    }
    public string Button3Text { get; set; } = "";
    public bool Button3Visible { get; set; }
    public override async Task SaveProgressAsync()
    {
        if (SelectedItem!.ResumeAt.HasValue == false || SelectedItem.ResumeAt!.Value < VideoPosition)
        {
            SelectedItem.ResumeAt = VideoPosition;
        }
        await _loader.UpdateMovieAsync(SelectedItem);
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
            VideoPath = SelectedItem!.Path;
            SelectedItem.LastWatched = DateTime.Now;
            if (SelectedItem.ResumeAt.HasValue == false)
            {
                await _loader.UpdateMovieAsync(SelectedItem);
            }
            else
            {
                await _loader.AddToHistoryAsync(SelectedItem);
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
            if (SelectedItem!.Opening.HasValue == false)
            {
                Button3Text = "Movie Started";
                VideoLength = 0;
            }
            else if (SelectedItem.Closing.HasValue == false)
            {
                Button3Text = "Movie Ended";
                VideoLength = 0;
            }
            else
            {
                VideoLength = SelectedItem.Closing!.Value;
            }
            ResumeSecs = _secs;
            VideoPosition = _secs;
            await _hostService.InitializeAsync();
            await ShowVideoLoadedAsync();
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
    }
    public bool CanButton3Process => Button3Visible;
    public async Task Button3ProcessAsync()
    {
        if (Button3Text == "Movie Started")
        {
            await StartMovieAsync();
        }
        else if (Button3Text == "Movie Ended")
        {
            await EndMovieAsync();
        }
    }
    private async Task StartMovieAsync()
    {
        if (VideoPosition > 0)
        {
            SelectedItem!.Opening = VideoPosition;
            await _loader.UpdateMovieAsync(SelectedItem);
        }
        Button3Visible = true;
        Button3Text = "Movie Ended";
        await Task.Delay(2000);
        Button3Visible = false;
    }
    private async Task EndMovieAsync()
    {
        //because fat albert was less than 20 minutes.
        //may do something else eventually.
        if (VideoLength < 1200)
        {
            if (VideoPosition + 120 < VideoLength)
            {
                return;
            }
        }
        else if (VideoPosition < 1200)
        {
            return;
        }
        SelectedItem!.Closing = VideoPosition;
        await VideoFinishedAsync();
    }
}