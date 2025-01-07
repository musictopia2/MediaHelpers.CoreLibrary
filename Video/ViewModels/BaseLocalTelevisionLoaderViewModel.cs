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
    protected static bool DidManuallyChooseHolidayEpisode(IEpisodeTable episode)
    {
        return episode.Holiday != EnumTelevisionHoliday.None;
    }
    private async Task ModifyHolidayAsync(HolidayModel holiday)
    {
        if (holiday.Holiday == SelectedItem!.Holiday)
        {
            _toast.ShowInfoToast("No holiday change");
            return;
        }
        EnumTelevisionHoliday previous = SelectedItem.Holiday!.Value;
        E tempItem;
        if (DoStopForHoliday)
        {
            tempItem = StopVideo();
        }
        else
        {
            tempItem = SelectedItem;
        }
        await _loadLogic.ModifyHolidayAsync(tempItem, holiday.Holiday);
        await FinishModifyingHoliday(tempItem, previous, holiday.NextMode);
    }
    protected virtual bool DoStopForHoliday => true;
    protected abstract Task FinishModifyingHoliday(IEpisodeTable tempItem, EnumTelevisionHoliday holiday, EnumNextMode nextMode);
    protected abstract Task StartNextEpisodeAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday);
    protected abstract Task FinishSkippingEpisodeForeverAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday, EnumNextMode nextMode);
    protected abstract Task FinishEditEpisodeLaterAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday, EnumNextMode nextMode);
    private async Task EditEpisodeLaterAsync(EnumNextMode mode)
    {
        var tempItem = StopVideo();
        await _loadLogic.EditEpisodeLaterAsync(tempItem);
        await FinishEditEpisodeLaterAsync(tempItem, tempItem.Holiday!.Value, mode);
    }
    private async Task SkipEpisodeForeverAsync(EnumNextMode mode)
    {
        var tempItem = StopVideo();
        await _loadLogic.ForeverSkipEpisodeAsync(tempItem);
        await FinishSkippingEpisodeForeverAsync(tempItem, tempItem.Holiday!.Value, mode);
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

    public Action? StartLoadingPlayer { get; set; }

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
    private (int startTime, int howLong) GetSimpleSkipData()
    {
        return (SelectedItem!.BeginAt, SelectedItem!.OpeningLength!.Value);
    }
    private async Task ProcessSkipsAsync()
    {
        if (_hasIntro)
        {
            var (StartTime, HowLong) = GetSimpleSkipData();
            SkipSceneClass skip = new()
            {
                StartTime = StartTime,
                HowLong = HowLong
            };
            var list = new BasicList<SkipSceneClass> { skip };
            Player.AddScenesToSkip(list);
        }
        else if (SelectedItem!.Skips.Count > 0)
        {
            Player.AddScenesToSkip(SelectedItem.Skips); //hopefully this simple.
        }
        var tvLength = await Player.LengthAsync();
        await CalculateDurationAsync(tvLength);
    }
    protected abstract bool CanInitializeRemoteControlAfterPlayerInit { get; }
    protected override async Task AfterPlayerInitAsync()
    {
        try
        {
            await ProcessSkipsAsync();
            if (CanInitializeRemoteControlAfterPlayerInit)
            {
                await _hostService.InitializeAsync();
            }
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
    }
    private async Task CalculateDurationAsync(int tvLength)
    {
        int newLength;
        //used to have the part where the show had to be at least 20 minutes in order to show the length.
        //however, cartoons like woody woodpecker was only around 6 minutes.  too unpredictable to be smart enough.
        //thsi means if the closing length was there, then use it no matter what.  if it causes part of an episode to be missed, too bad.  the solution would be to update the episode in the data source to 0 for closing length
        //TimeSpan thisSpan = TimeSpan.FromSeconds(tvLength);
        if (SelectedItem!.ClosingLength.HasValue == true)
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