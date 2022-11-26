namespace MediaHelpers.CoreLibrary.Music.Components.Core;
public partial class PauseButtonComponent
{
    [Inject]
    private IPlayPauseClass? DataContext { get; set; }
    [Parameter]
    public bool CanPause { get; set; } 
}