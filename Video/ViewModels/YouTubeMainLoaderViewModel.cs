namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public abstract class YouTubeMainLoaderViewModel<V> : IVideoPlayerViewModel, ITelevisionLoaderViewModel where V : class
{
    private readonly IExit _exit;
    public YouTubeMainLoaderViewModel(IExit exit)
    {
        _exit = exit;
    }
    public V? SelectedItem { get; protected set; }
    public Action? StateHasChanged { get; set; }
    public bool PlayButtonVisible { get; set; }
    public bool CloseButtonVisible { get; set; }
    public bool FullScreen { get; set; } //iffy.
    public string ProgressText { get; set; } = "00:00:00/00:00:00";
    public abstract Task SaveProgressAsync();
    public abstract Task VideoFinishedAsync();
    public Action? ComponentPlay { get; set; } //somebody needs to set this to allow the youtube player to play.
    public int VideoPosition { get; set; } //this will need to be set somehow (?)
    public string VideoID { get; set; } = "";
    public int ResumeSecs { get; set; }
    public async Task CloseScreenAsync()
    {
        await SaveProgressAsync();
        _exit.ExitApp();
    }
    //this is going to be iffy
    public void PlayPause()
    {
        ComponentPlay?.Invoke();
    }
    public abstract Task InitAsync();
}