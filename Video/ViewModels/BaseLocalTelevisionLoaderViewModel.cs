namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public abstract class BaseLocalTelevisionLoaderViewModel<E, T> : VideoMainLoaderViewModel<E>, ITelevisionLoaderViewModel, IStartLoadingViewModel
    where E: class, IEpisodeTable
    where T: class, IBasicTelevisionModel, new()
{
    private readonly IBasicTelevisionLoaderLogic<E> _loadLogic;
    private readonly ITelevisionVideoLoader<E> _reload;
    private readonly IBasicTelevisionRemoteControlHostService<T> _hostService;
    private readonly ISystemError _error;
    private readonly IToast _toast;
    public BaseLocalTelevisionLoaderViewModel(IFullVideoPlayer player,
        IBasicTelevisionLoaderLogic<E> loadLogic,
        ITelevisionVideoLoader<E> reload,
        TelevisionContainerClass<E> containerClass,
        IBasicTelevisionRemoteControlHostService<T> hostService,
        ISystemError error,
        IToast toast,
        IExit exit
        ) : base(player, error, exit)
    {
        _loadLogic = loadLogic;
        _reload = reload;
        _hostService = hostService;
        _error = error;
        _toast = toast;
        if (ee1.EpisodeChosen.HasValue == false)
        {
            throw new CustomBasicException("No episode was chosen");
        }
        containerClass.EpisodeChosen = _loadLogic.GetChosenEpisode();
        if (containerClass.EpisodeChosen is null)
        {
            throw new CustomBasicException("There was no episode chosen.  Rethink");
        }
        _hostService.NewClient = SendOtherDataAsync;
        _hostService.SkipEpisodeForever = SkipEpisodeForeverAsync;
        _hostService.ModifyHoliday = ModifyHolidayAsync;
        _hostService.EditLater = EditEpisodeLaterAsync;
        SelectedItem = containerClass.EpisodeChosen;
    }
    protected static bool DidManuallyChooseHolidayEpisode(IEpisodeTable episode)
    {
        return episode.Holiday != EnumTelevisionHoliday.None;
    }
    private async Task ModifyHolidayAsync(EnumTelevisionHoliday holiday)
    {
        if (holiday == SelectedItem!.Holiday)
        {
            _toast.ShowInfoToast("No holiday change");
            return;
        }
        EnumTelevisionHoliday previous = SelectedItem.Holiday!.Value;
        var tempItem = StopEpisode();
        await _loadLogic.ModifyHolidayAsync(tempItem, holiday);
        await FinishModifyingHoliday(tempItem, previous);
        //await StartNextEpisodeAsync(tempItem, previous);
    }
    protected abstract Task FinishModifyingHoliday(IEpisodeTable tempItem, EnumTelevisionHoliday holiday);
    protected abstract Task StartNextEpisodeAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday);

    protected abstract Task FinishSkippingEpisodeForeverAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday);
    protected abstract Task FinishEditEpisodeLaterAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday);
    private async Task EditEpisodeLaterAsync()
    {
        var tempItem = StopEpisode();
        await _loadLogic.EditEpisodeLaterAsync(tempItem);
        await FinishEditEpisodeLaterAsync(tempItem, tempItem.Holiday!.Value);
    }
    private async Task SkipEpisodeForeverAsync()
    {
        var tempItem = StopEpisode();
        await _loadLogic.ForeverSkipEpisodeAsync(tempItem);
        await FinishSkippingEpisodeForeverAsync(tempItem, tempItem.Holiday!.Value);
    }
    protected E StopEpisode()
    {
        ResumeSecs = 0;
        VideoPosition = 0;
        if (SelectedItem is null)
        {
            throw new CustomBasicException("No episode was even chosen");
        }
        var tempItem = SelectedItem;
        SelectedItem = null;
        if (tempItem is null)
        {
            throw new CustomBasicException("The temp item is null.  Wrong");
        }
        Player.StopPlay();
        return tempItem;
    }
    protected void LoadNewEpisode()
    {
        if (SelectedItem is null)
        {
            throw new CustomBasicException("No episode was even chosen");
        }
        _reload.ChoseEpisode(SelectedItem);
    }
    public override Task SaveProgressAsync()
    {
        return _loadLogic.UpdateTVShowProgressAsync(SelectedItem!, VideoPosition);
    }
    public override Task VideoFinishedAsync()
    {
        return _loadLogic.FinishTVEpisodeAsync(SelectedItem!);
    }
    private bool _hasIntro;
    private void BeforeInitEpisode()
    {
        int secs = _loadLogic.GetSeconds(SelectedItem!);
        ResumeSecs = secs;
        VideoPosition = ResumeSecs;
        VideoPath = SelectedItem!.FullPath();
        _hasIntro = SelectedItem.BeginAt > 0;
    }
    protected override async Task BeforePlayerInitAsync()
    {
        try
        {
            await base.BeforePlayerInitAsync();
            BeforeInitEpisode();
            await _loadLogic.InitializeEpisodeAsync(SelectedItem!);
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
    }
    private (int startTime, int howLong) GetSkipData()
    {
        return (SelectedItem!.BeginAt, SelectedItem!.OpeningLength!.Value);
    }
    private async Task ProcessSkipsAsync()
    {
        if (_hasIntro)
        {
            var (StartTime, HowLong) = GetSkipData();
            SkipSceneClass skip = new()
            {
                StartTime = StartTime,
                HowLong = HowLong
            };
            var list = new BasicList<SkipSceneClass> { skip };
            Player.AddScenesToSkip(list);
        }
        var tvLength = Player.Length();
        await CalculateDurationAsync(tvLength);
    }
    protected override async Task AfterPlayerInitAsync()
    {
        try
        {
            await ProcessSkipsAsync();

            await _hostService.InitializeAsync();
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
    }
    private async Task CalculateDurationAsync(int tvLength)
    {
        int newLength;
        TimeSpan thisSpan = TimeSpan.FromSeconds(tvLength);
        if (thisSpan.Minutes >= 20 && SelectedItem!.ClosingLength.HasValue == true)
        {
            newLength = tvLength - SelectedItem.ClosingLength!.Value;
        }
        else
        {
            newLength = tvLength;
        }
        VideoLength = newLength;
        await ShowVideoLoadedAsync();
    }
    protected override Task SendOtherDataAsync()
    {
        T television = GetTelevisionDataToSend();
        return _hostService.SendProgressAsync(television);
    }
    protected abstract T GetTelevisionDataToSend();
}