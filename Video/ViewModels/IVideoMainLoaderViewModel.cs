namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public interface IVideoMainLoaderViewModel<V> where V : class
{
    bool CloseButtonVisible { get; set; }
    bool FullScreen { get; set; }
    bool PlayButtonVisible { get; set; }
    string ProgressText { get; set; } //i think this is still needed (?)
    //int ResumeSecs { get; set; }
    //V? SelectedItem { get; }
    Action? StateHasChanged { get; set; }
    //int VideoLength { get; set; }
    //string VideoPath { get; set; }
    //int VideoPosition { get; set; }

    Task CloseScreenAsync();
    Task InitAsync();
    void PlayPause();
    bool CanPlay { get; }
    //hopefully not needed (?)
    //Task SaveProgressAsync();
    //Task VideoFinishedAsync();
}