namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class VideoBarComponent<V>
    where V : class
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public IVideoMainLoaderViewModel<V>? DataContext { get; set; }
    [Inject]
    private ISimpleVideoPlayer? Player { get; set; }
    private bool _loaded = false;
    protected override async Task OnInitializedAsync()
    {
        await DataContext!.InitAsync();
        _loaded = true;
    }
}