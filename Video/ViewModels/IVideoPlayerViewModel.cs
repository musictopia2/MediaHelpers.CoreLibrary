namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public interface IVideoPlayerViewModel
{
    Action? StateHasChanged { get; set; }
    bool PlayButtonVisible { get; set; }
    bool CloseButtonVisible { get; set; }
    bool FullScreen { get; set; }
}