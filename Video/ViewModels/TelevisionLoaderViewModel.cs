namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionLoaderViewModel : VideoMainLoaderViewModel<IEpisodeTable>, ITelevisionLoaderViewModel
{
    private readonly ITelevisionLoaderLogic _loadLogiclogic;
    private readonly TelevisionHolidayViewModel _holidayViewModel;
    private readonly ITelevisionRemoteControlHostService _hostService;
    private readonly ITelevisionListLogic _listLogic;
    private readonly ISystemError _error;
    private readonly IToast _toast;
    private readonly IExit _exit;
    private readonly bool _wasHoliday;
    public TelevisionLoaderViewModel(IFullVideoPlayer player,
        ITelevisionLoaderLogic loadLogic,
        TelevisionHolidayViewModel holidayViewModel,
        IDateOnlyPicker picker,
        TelevisionContainerClass containerClass,
        ITelevisionRemoteControlHostService hostService,
        ITelevisionListLogic listLogic,
        ITelevisionShellViewModel shellViewModel,
        ISystemError error,
        IToast toast,
        IExit exit
        ) : base(player, error, exit)
    {
        _loadLogiclogic = loadLogic;
        _holidayViewModel = holidayViewModel;
        if (shellViewModel.DidReset)
        {
            _wasHoliday = false;
        }
        else
        {
            var temps = picker.GetCurrentDate.WhichHoliday();
            _wasHoliday = temps is not EnumTelevisionHoliday.None; //its dependent on date period now.
        }
        _hostService = hostService;
        _listLogic = listLogic;
        _error = error;
        _toast = toast;
        _exit = exit;
        if (containerClass.EpisodeChosen is null)
        {
            throw new CustomBasicException("There was no episode chosen.  Rethink");
        }
        _hostService.NewClient = SendOtherDataAsync;
        _hostService.SkipEpisodeForever = SkipEpisodeForeverAsync;
        _hostService.ModifyHoliday = ModifyHolidayAsync;
        _hostService.SkipEpisodeTemporarily = SkipEpisodeTemporarilyAsync;
        SelectedItem = containerClass.EpisodeChosen;
    }
    private async Task ModifyHolidayAsync(EnumTelevisionHoliday holiday)
    {
        if (holiday == SelectedItem!.Holiday)
        {
            _toast.ShowInfoToast("No holiday change");
            return;
        }
        var tempItem = StopEpisode();
        await _loadLogiclogic.ModifyHolidayAsync(tempItem, holiday);
        await StartNextEpisodeAsync(tempItem);
    }
    private async Task SkipEpisodeTemporarilyAsync()
    {
        var tempItem = StopEpisode();
        await _loadLogiclogic.TemporarilySKipEpisodeAsync(tempItem);
        if (_wasHoliday && _holidayViewModel.ManuallyChoseHoliday == false)
        {
            _exit.ExitApp();
            return;
        }
        await StartNextEpisodeAsync(tempItem);
    }
    private async Task SkipEpisodeForeverAsync()
    {
        var tempItem = StopEpisode();
        await _loadLogiclogic.ForeverSkipEpisodeAsync(tempItem);
        if (_wasHoliday && _holidayViewModel.ManuallyChoseHoliday == false)
        {
            _exit.ExitApp();
            //could eventually decide to have the choices on the remote control.
            //this would mean that if you open and its holiday, then if you want to choose holiday, then will do it.  otherwise, load list
            return; //has to exit the app at this point.  because its too complicated on what it should do next.
            //_holidayViewModel.RemoveHolidayEpisode(tempItem);
        }
        if (_wasHoliday)
        {
            _holidayViewModel.RemoveHolidayEpisode(tempItem);
        }
        await StartNextEpisodeAsync(tempItem);
    }
    private IEpisodeTable StopEpisode()
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
    private async Task StartNextEpisodeAsync(IEpisodeTable tempItem)
    {
        IShowTable show = tempItem.ShowTable;
        if (_wasHoliday && _holidayViewModel.ManuallyChoseHoliday == false)
        {
            _error.ShowSystemError("Holidays for starting next episode should have exited the app because too many possibilities");
            _exit.ExitApp(); //if this is the case, not sure what we can do then.
            return;
            //throw new CustomBasicException("Holidays for starting next episode should have exited the app because too many possibilities");
        }
        SelectedItem = _holidayViewModel.ManuallyChoseHoliday ? _holidayViewModel.GetHolidayEpisode(show.LengthType) : await _listLogic.GetNextEpisodeAsync(show);
        if (SelectedItem is null)
        {
            _error.ShowSystemError("Selected Item Was Null");
            return;
        }
        try
        {
            await _loadLogiclogic.ReloadAppAsync(SelectedItem!);
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
        //await _loadLogiclogic.AddToHistoryAsync(SelectedItem!); 
        //await ProcessSkipsAsync();
        //await ShowVideoLoadedAsync();
    }
    public override Task SaveProgressAsync()
    {
        return _loadLogiclogic.UpdateTVShowProgressAsync(SelectedItem!, VideoPosition);
    }
    public override Task VideoFinishedAsync()
    {
        return _loadLogiclogic.FinishTVEpisodeAsync(SelectedItem!);
    }
    private bool _hasIntro;
    private void BeforeInitEpisode()
    {
        int secs = _loadLogiclogic.GetSeconds(SelectedItem!);
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
            await _loadLogiclogic.InitializeEpisodeAsync(SelectedItem!);
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
        return _hostService.SendProgressAsync(new TelevisionModel(SelectedItem!.ShowTable.ShowName, ProgressText, SelectedItem.Holiday!));
    }
}