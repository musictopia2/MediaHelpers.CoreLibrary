namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public interface IStartLoadingViewModel
{
    bool CanPlay { get; }
    Action? StartLoadingPlayer { get; set; } //this may be needed.  would mean that if you can suddenly play, then can do something.  was going to do something else.  but problems with naming.
}