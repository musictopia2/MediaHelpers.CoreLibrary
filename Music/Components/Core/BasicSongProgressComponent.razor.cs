namespace MediaHelpers.CoreLibrary.Music.Components.Core;
public partial class BasicSongProgressComponent
{
    [Inject]
    private BasicSongProgressViewModel? DataContext { get; set; }
    [Parameter]
    public bool AlsoStart { get; set; } = true;
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    protected override void OnInitialized()
    {
        if (AlsoStart)
        {
            DataContext!.Start(); //somehow its doing duplicates.  trying this way.
        }
        DataContext!.StateChanged = () => InvokeAsync(StateHasChanged);
    }
}