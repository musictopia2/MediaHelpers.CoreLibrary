namespace MediaHelpers.CoreLibrary.Music.Components.Playlists;
public partial class PlaylistSongDashboardComponent
{
    [Parameter]
    public EventCallback<EnumPlaylistUIStage> StageChanged { get; set; }
    [Parameter]
    public bool NeedsToClear { get; set; }
    [Inject]
    private PlaylistSongDashboardViewModel? DataContext { get; set; }
    private FullComboGenericLayout<IPlayListMain>? _combo;
    protected override void OnInitialized()
    {
        _combo = null;
        DataContext!.FocusCombo = async () => await _combo!.FocusAsync();
        DataContext.StageChanged = (item) => StageChanged.InvokeAsync(item);
        if (NeedsToClear)
        {
            DataContext.ChosenPlayList = null;
        }
        base.OnInitialized();
    }
    protected override async Task OnInitializedAsync()
    {
        await DataContext!.InitAsync();
    }
}