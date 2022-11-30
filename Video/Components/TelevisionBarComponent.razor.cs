namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class TelevisionBarComponent
{
    [Parameter]
    public bool ShowProgress { get; set; } = true;
    [Inject]
    private ITelevisionLoaderViewModel? DataContext { get; set; }
    protected override void OnInitialized()
    {
        DataContext!.StateHasChanged = () => InvokeAsync(StateHasChanged);
    }
}