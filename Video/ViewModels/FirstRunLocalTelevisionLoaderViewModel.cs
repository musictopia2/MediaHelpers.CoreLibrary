namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class FirstRunLocalTelevisionLoaderViewModel<E> : BaseLocalTelevisionLoaderViewModel<E>
    where E : class, IEpisodeTable
{
    private readonly IFullVideoPlayer _player;
    private readonly IFirstRunTelevisionLoaderLogic<E> _loadLogic;
    private readonly IFirstRunTelevisionRemoteControlHostService _hostService;
    private readonly INextEpisodeLogic<E> _nextLogic;
    private bool _canStart;
    public FirstRunLocalTelevisionLoaderViewModel(IFullVideoPlayer player,
        IFirstRunTelevisionLoaderLogic<E> loadLogic,
        TelevisionContainerClass<E> containerClass,
        IFirstRunTelevisionRemoteControlHostService hostService,
        INextEpisodeLogic<E> nextLogic,
        ISystemError error,
        IToast toast,
        IExit exit) : base(player, loadLogic, containerClass, hostService, error, toast, exit)
    {
        _player = player;
        _loadLogic = loadLogic;
        _hostService = hostService;
        _nextLogic = nextLogic;
        _hostService.Start = StartAsync;
        _hostService.IntroBegins = ShowIntroBeginsAsync;
        _hostService.EndEpisode = EndEpisodeAsync;
        _hostService.ThemeSongOver = ThemeSongOverAsync;
        _hostService.Rewind = RewindAsync;
    }
    public override bool CanPlay => _canStart;
    private Task StartAsync()
    {
        _canStart = true;
        return Task.CompletedTask;
    }
    private async Task ShowIntroBeginsAsync()
    {
        await _loadLogic.IntroBeginsAsync(SelectedItem!); //i think
    }
    private async Task EndEpisodeAsync()
    {
        //may decide later to not always stop it (depending on conditions).
        //this can't be youtube either.
        E episode = StopEpisode();
        await _loadLogic.EndTVEpisodeEarlyAsync(episode);
        //for now, no questions asked.  eventually figure out a better way to figure out whether to really end it or ignore (because done by mistake)
        //await _loadLogic.EndTVEpisodeEarlyAsync(SelectedItem!);
    }
    private async Task ThemeSongOverAsync()
    {
        await _loadLogic.ThemeSongOverAsync(SelectedItem!);
    }
    private async Task RewindAsync()
    {
        if (SelectedItem is null)
        {
            throw new CustomBasicException("No selected item");
        }
        int position = _player.TimeElapsedSeconds();
        if (position < 5)
        {
            return;
        }
        position -= 5;
        //you can do as many times as you need.

        await _player.PlayAsync(position);
        //hopefully this simple.
        //if so, then hopefully everything will save like normal.
        //await _loadLogic.RewindAsync(SelectedItem!);
        //not sure about what to do about the video player though.

    }

    protected override async Task StartNextEpisodeAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday)
    {
        IShowTable show = tempItem.ShowTable;
        SelectedItem = await _nextLogic.GetNextEpisodeAsync(show);
        await ReloadAppAsync();
    }

    protected override async Task FinishSkippingEpisodeForeverAsync(IEpisodeTable tempItem, EnumTelevisionHoliday holiday)
    {
        await StartNextEpisodeAsync(tempItem, holiday); //this simple.
    }
}