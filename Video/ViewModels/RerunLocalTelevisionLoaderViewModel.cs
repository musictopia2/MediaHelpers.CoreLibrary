using CommonBasicLibraries.BasicUIProcesses;

namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class RerunLocalTelevisionLoaderViewModel<E> : BaseLocalTelevisionLoaderViewModel<E, BasicTelevisionModel>
    where E : class, IEpisodeTable
{
    private readonly IRerunTelevisionLoaderLogic<E> _loadLogic;
    private readonly TelevisionHolidayViewModel<E> _holidayViewModel;
    private readonly IDateOnlyPicker _picker;
    private readonly IRerunTelevisionRemoteControlHostService _hostService;
    private readonly INextEpisodeLogic<E> _nextLogic;
    private readonly ISystemError _error;
    private readonly IExit _exit;
    private readonly IMessageBox _message;
    private bool _wasHoliday;
    public RerunLocalTelevisionLoaderViewModel(IFullVideoPlayer player,
        IRerunTelevisionLoaderLogic<E> loadLogic,
        ITelevisionVideoLoader<E> reload,
        TelevisionHolidayViewModel<E> holidayViewModel, //this one needs it though.
        IDateOnlyPicker picker,
        TelevisionContainerClass<E> containerClass,
        IRerunTelevisionRemoteControlHostService hostService,
        INextEpisodeLogic<E> nextLogic,
        ISystemError error,
        IToast toast,
        IExit exit,
        IMessageBox message
        ) : base(player, loadLogic, reload, containerClass, hostService, error, toast, exit)
    {
        _loadLogic = loadLogic;
        _holidayViewModel = holidayViewModel;
        _picker = picker;
        _hostService = hostService;
        _nextLogic = nextLogic;
        _error = error;
        _exit = exit;
        _message = message;
        _hostService.SkipEpisodeTemporarily = SkipEpisodeTemporarilyAsync;
    }
    public override bool CanPlay => true;
    protected override bool CanInitializeRemoteControlAfterPlayerInit => true;
    protected override async Task FinishEditEpisodeLaterAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday, EnumNextMode nextMode)
    {
        await FinishEditingEpisodeAsync(tempItem, holiday, nextMode);
    }
    private async Task FinishEditingEpisodeAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday, EnumNextMode nextMode)
    {
        ProcessHoliday(tempItem);
        bool manuallyChose = holiday != EnumTelevisionHoliday.None;
        if (_wasHoliday && manuallyChose == false)
        {
            Execute.OnUIThread(_exit.ExitApp);
            return;
        }
        if (nextMode == EnumNextMode.CloseOut)
        {
            Execute.OnUIThread(_exit.ExitApp);
            return;
            //if you chose holiday and chose to close out, hopefully when you go back in again, you have to choose whether you want holiday or not again.
        }
        if (manuallyChose)
        {
            //for now, if it was manually chosen, then must do this.  later may change.
            if (_holidayViewModel.IsLoaded == false)
            {
                await _holidayViewModel.InitAsync(tempItem.Holiday!.Value);
            }
            else
            {
                _holidayViewModel.RemoveHolidayEpisode(tempItem);
            }
        }
        await StartNextEpisodeAsync(tempItem, holiday);
    }
    protected override async Task FinishSkippingEpisodeForeverAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday, EnumNextMode nextMode)
    {
        await FinishEditingEpisodeAsync(tempItem, holiday, nextMode);
    }
    protected override async Task StartNextEpisodeAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday)
    {
        IShowTable show = tempItem.ShowTable;
        bool manuallyChose = holiday != EnumTelevisionHoliday.None;
        if (_wasHoliday && manuallyChose == false)
        {
            _error.ShowSystemError("Holidays for starting next episode should have exited the app because too many possibilities");
            _exit.ExitApp();
            return;
        }
        bool rets;
        rets = await CheckNextAsync();
        if (rets == false)
        {
            return;
        }
        SelectedItem = manuallyChose ? _holidayViewModel.GetHolidayEpisode(show.LengthType) : await _nextLogic.GetNextEpisodeAsync(show);
        if (SelectedItem is null)
        {
            await _message.ShowMessageAsync("No more episodes was found");
            Execute.OnUIThread(_exit.ExitApp);
            //_error.ShowSystemError("Selected Item Was Null");
            return;
        }
        LoadNewEpisode(); //not even same app
    }
    private void ProcessHoliday(IEpisodeTable tempItem)
    {
        if (tempItem.Holiday == EnumTelevisionHoliday.None)
        {
            _wasHoliday = false;
        }
        else
        {
            var temps = _picker.GetCurrentDate.WhichHoliday();
            _wasHoliday = temps is not EnumTelevisionHoliday.None; //its depend
        }
    }
    private async Task SkipEpisodeTemporarilyAsync(EnumNextMode mode)
    {
        var tempItem = StopEpisode();
        await _loadLogic.TemporarilySKipEpisodeAsync(tempItem);
        ProcessHoliday(tempItem);
        bool manuallyChose = tempItem.Holiday != EnumTelevisionHoliday.None;
        if (_wasHoliday && manuallyChose == false)
        {
            _exit.ExitApp();
            return;
        }
        bool rets = await CheckNextAsync();
        if (rets == false)
        {
            return;
        }
        await FinishEditingEpisodeAsync(tempItem, tempItem.Holiday!.Value, mode);
        //may have options to completely close out and not even choose another episode
        //await StartNextEpisodeAsync(tempItem, tempItem.Holiday!.Value);
    }
    private async Task<bool> CheckNextAsync()
    {
        bool rets;
        rets = await _loadLogic.CanGoToNextEpisodeAsync();
        if (rets == false)
        {
            Execute.OnUIThread(_exit.ExitApp);
            //_exit.ExitApp();
            return false;
        }
        return true;
    }

    protected override async Task FinishModifyingHoliday(IEpisodeTable tempItem, EnumTelevisionHoliday holiday, EnumNextMode nextMode)
    {
        await FinishEditingEpisodeAsync(tempItem, holiday, nextMode);
    }

    protected override BasicTelevisionModel GetTelevisionDataToSend()
    {
        BasicTelevisionModel output = new();
        output.ShowName = SelectedItem!.ShowTable.ShowName;
        output.Progress = ProgressText;
        output.Holiday = SelectedItem.Holiday;
        output.CanEdit = SelectedItem.CanEdit; //for now.
        output.NeedsStart = false; //never needs start for this since its reruns.  firstrun can vary.  plus send other information as well.
        return output;
    }
}