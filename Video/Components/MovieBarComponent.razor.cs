namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class MovieBarComponent
{
    [Inject]
    private MovieLoaderViewModel? DataContext { get; set; }
    [Inject]
    private IFullVideoPlayer? Player { get; set; }
    protected override void OnInitialized()
    {
        DataContext!.StateHasChanged = () => InvokeAsync(StateHasChanged);
    }
}