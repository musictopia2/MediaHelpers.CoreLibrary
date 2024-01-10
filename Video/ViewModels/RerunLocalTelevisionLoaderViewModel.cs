namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class RerunLocalTelevisionLoaderViewModel : BaseLocalTelevisionLoaderViewModel
{
    private readonly IRerunTelevisionLoaderLogic _loadLogic;
    private readonly TelevisionHolidayViewModel _holidayViewModel;
    private readonly IRerunTelevisionRemoteControlHostService _hostService;
    private readonly IExit _exit;

    public RerunLocalTelevisionLoaderViewModel(IFullVideoPlayer player,
        IRerunTelevisionLoaderLogic loadLogic,
        TelevisionHolidayViewModel holidayViewModel,
        IDateOnlyPicker picker,
        TelevisionContainerClass containerClass,
        IRerunTelevisionRemoteControlHostService hostService,
        ITelevisionListLogic listLogic,
        ITelevisionShellViewModel shellViewModel,
        ISystemError error,
        IToast toast,
        IExit exit) : base(player, loadLogic, holidayViewModel, picker, containerClass, hostService, listLogic, shellViewModel, error, toast, exit)
    {
        _loadLogic = loadLogic;
        _holidayViewModel = holidayViewModel;
        _hostService = hostService;
        _exit = exit;
        _hostService.SkipEpisodeTemporarily = SkipEpisodeTemporarilyAsync;
    }

    public override bool CanPlay => true;

    private async Task SkipEpisodeTemporarilyAsync()
    {
        var tempItem = StopEpisode();
        await _loadLogic.TemporarilySKipEpisodeAsync(tempItem);
        if (WasHoliday && _holidayViewModel.ManuallyChoseHoliday == false)
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
        await StartNextEpisodeAsync(tempItem);
    }
}