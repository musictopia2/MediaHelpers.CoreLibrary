namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public abstract class BaseLocalTelevisionLoaderViewModel : VideoMainLoaderViewModel<IEpisodeTable>, ITelevisionLoaderViewModel
{
    private readonly IBasicTelevisionLoaderLogic _loadLogic;
    //private readonly TelevisionHolidayViewModel _holidayViewModel;
    private readonly IBasicTelevisionRemoteControlHostService _hostService;

    //private readonly ITelevisionListLogic _listLogic;
    private readonly ISystemError _error;
    private readonly IToast _toast;
    //protected readonly bool WasHoliday;
    //private readonly bool _wasHoliday;
    public BaseLocalTelevisionLoaderViewModel(IFullVideoPlayer player,
        IBasicTelevisionLoaderLogic loadLogic,
        //TelevisionHolidayViewModel holidayViewModel,
        TelevisionContainerClass containerClass,
        IBasicTelevisionRemoteControlHostService hostService,
        //ITelevisionListLogic listLogic,
        ISystemError error,
        IToast toast,
        IExit exit
        ) : base(player, error, exit)
    {
        _loadLogic = loadLogic;
        //_holidayViewModel = holidayViewModel;
        
        _hostService = hostService;
        //_listLogic = listLogic;
        _error = error;
        _toast = toast;
        if (ee1.EpisodeChosen.HasValue == false)
        {
            throw new CustomBasicException("No episode was chosen");
        }
        containerClass.EpisodeChosen = _loadLogic.GetChosenEpisode();
        //_loadLogiclogic.PopulateChosenEpisode()
        if (containerClass.EpisodeChosen is null)
        {
            throw new CustomBasicException("There was no episode chosen.  Rethink");
        }
        _hostService.NewClient = SendOtherDataAsync;
        _hostService.SkipEpisodeForever = SkipEpisodeForeverAsync;
        _hostService.ModifyHoliday = ModifyHolidayAsync;

        //_hostService.SkipEpisodeTemporarily = SkipEpisodeTemporarilyAsync;
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
        var tempItem = StopEpisode();
        EnumTelevisionHoliday previous = SelectedItem.Holiday!.Value;
        await _loadLogic.ModifyHolidayAsync(tempItem, holiday);
        await StartNextEpisodeAsync(tempItem, previous);
    }
    protected abstract Task StartNextEpisodeAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday);

    protected abstract Task FinishSkippingEpisodeForeverAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday);

    private async Task SkipEpisodeForeverAsync()
    {
        var tempItem = StopEpisode();
        await _loadLogic.ForeverSkipEpisodeAsync(tempItem);
        await FinishSkippingEpisodeForeverAsync(tempItem, tempItem.Holiday!.Value);


        //bool manuallyChose = tempItem.Holiday != EnumTelevisionHoliday.None;
        //if (WasHoliday && manuallyChose == false)
        //{
        //    _exit.ExitApp();
        //    //could eventually decide to have the choices on the remote control.
        //    //this would mean that if you open and its holiday, then if you want to choose holiday, then will do it.  otherwise, load list
        //    return; //has to exit the app at this point.  because its too complicated on what it should do next.
        //    //_holidayViewModel.RemoveHolidayEpisode(tempItem);
        //}
        //if (WasHoliday)
        //{
        //    if (_holidayViewModel.IsLoaded == false)
        //    {
        //        await _holidayViewModel.InitAsync(tempItem.Holiday!.Value);
        //    }
        //    else
        //    {
        //        _holidayViewModel.RemoveHolidayEpisode(tempItem);
        //    }
        //}
        //await StartNextEpisodeAsync(tempItem);
    }
    protected IEpisodeTable StopEpisode()
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
    //protected async Task StartNextEpisodeAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday)
    //{
    //    IShowTable show = tempItem.ShowTable;
    //    bool manuallyChose = holiday != EnumTelevisionHoliday.None;
    //    if (WasHoliday && manuallyChose == false)
    //    {
    //        _error.ShowSystemError("Holidays for starting next episode should have exited the app because too many possibilities");
    //        _exit.ExitApp(); //if this is the case, not sure what we can do then.
    //        return;
    //        //throw new CustomBasicException("Holidays for starting next episode should have exited the app because too many possibilities");
    //    }
    //    SelectedItem = manuallyChose ? _holidayViewModel.GetHolidayEpisode(show.LengthType) : await _nextLogic.GetNextEpisodeAsync(show);
    //    if (SelectedItem is null)
    //    {
    //        _error.ShowSystemError("Selected Item Was Null");
    //        return;
    //    }
    //    try
    //    {
    //        await _loadLogic.ReloadAppAsync(SelectedItem!);
    //    }
    //    catch (Exception ex)
    //    {
    //        _error.ShowSystemError(ex.Message);
    //    }
    //}
    protected async Task ReloadAppAsync()
    {
        try
        {
            await _loadLogic.ReloadAppAsync(SelectedItem!);
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
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
        int startAt;
        if (SelectedItem!.StartAt.HasValue == false)
        {
            startAt = 0;
        }
        else
        {
            startAt = SelectedItem.StartAt.Value; //i think.
        }
        return _hostService.SendProgressAsync(new TelevisionModel(SelectedItem.ShowTable.ShowName, ProgressText, SelectedItem.Holiday!, CanPlay == false, startAt));
    }

}