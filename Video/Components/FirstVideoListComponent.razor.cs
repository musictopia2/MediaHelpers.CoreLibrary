namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class FirstVideoListComponent //used to allow testing
{
    [Inject]
    private TelevisionListViewModel? DataContext { get; set; }
    [Inject]
    private IFirstVideoLoader? Loader { get; set; }
    private FullComboGenericLayout<IShowTable>? _page;
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
    private void DoChooseVideo()
    {
        Loader!.ChoseVideo(DataContext!.SelectedItem!);
    }
}