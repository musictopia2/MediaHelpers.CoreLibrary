namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class VideoListComponent<V>
    where V : class
{
    [Parameter]
    public MainVideoListViewModel<V>? DataContext { get; set; }
    [Parameter]
    public EventCallback VideoSelected { get; set; }
    [Parameter]
    public bool IsChild { get; set; }
    [Parameter]
    public string Title { get; set; } = "";
    [Parameter]
    public Func<V, string>? RetrieveValue { get; set; }
    [Parameter]
    public EventCallback VideoChanged { get; set; }
    private void PrivateChange(V value)
    {
        DataContext!.SelectedItem = value;
        VideoChanged.InvokeAsync();
    }
    private FullComboGenericLayout<V>? _page;
    private readonly AutoCompleteStyleModel _style = new();
    protected override void OnInitialized()
    {
        _page = null;
        _style.FontSize = "40px";
        base.OnInitialized();
    }
    protected override async Task OnInitializedAsync()
    {
        await DataContext!.InitAsync();
    }
    protected override void OnAfterRender(bool firstRender)
    {
        if (_page is null)
        {
            throw new CustomBasicException("No page was found.  Rethink");
        }
        DataContext!.FocusCombo = async () => await _page.FocusAsync();
    }
}