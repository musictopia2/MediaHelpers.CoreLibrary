﻿namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class RerunLocalTelevisionLoaderViewModel : BaseLocalTelevisionLoaderViewModel
{
    private readonly IRerunTelevisionLoaderLogic _loadLogic;
    private readonly TelevisionHolidayViewModel _holidayViewModel;
    private readonly IDateOnlyPicker _picker;
    private readonly IRerunTelevisionRemoteControlHostService _hostService;
    private readonly INextEpisodeLogic _nextLogic;
    private readonly ISystemError _error;
    private readonly IExit _exit;
    private bool _wasHoliday;
    public RerunLocalTelevisionLoaderViewModel(IFullVideoPlayer player,
        IRerunTelevisionLoaderLogic loadLogic,
        TelevisionHolidayViewModel holidayViewModel, //this one needs it though.
        IDateOnlyPicker picker,
        TelevisionContainerClass containerClass,
        IRerunTelevisionRemoteControlHostService hostService,
        INextEpisodeLogic nextLogic,
        ISystemError error,
        IToast toast,
        IExit exit) : base(player, loadLogic, containerClass, hostService, error, toast, exit)
    {
        _loadLogic = loadLogic;
        _holidayViewModel = holidayViewModel;
        _picker = picker;
        _hostService = hostService;
        _nextLogic = nextLogic;
        _error = error;
        _exit = exit;
        _hostService.SkipEpisodeTemporarily = SkipEpisodeTemporarilyAsync;
    }

    public override bool CanPlay => true;

    protected override async Task FinishSkippingEpisodeForeverAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday)
    {
        ProcessHoliday(tempItem);
        bool manuallyChose = holiday != EnumTelevisionHoliday.None;
        if (_wasHoliday && manuallyChose == false)
        {
            _exit.ExitApp();
            return;
        }
        if (_wasHoliday)
        {
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
        SelectedItem = manuallyChose ? _holidayViewModel.GetHolidayEpisode(show.LengthType) : await _nextLogic.GetNextEpisodeAsync(show);
        if (SelectedItem is null)
        {
            _error.ShowSystemError("Selected Item Was Null");
            return;
        }
        await ReloadAppAsync();
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
    private async Task SkipEpisodeTemporarilyAsync()
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
        bool rets;
        rets = await _loadLogic.CanGoToNextEpisodeAsync();
        if (rets == false)
        {
            _exit.ExitApp();
            return; //close out period now.
        }
        //may have options to completely close out and not even choose another episode
        await StartNextEpisodeAsync(tempItem, tempItem.Holiday!.Value);
    }
}