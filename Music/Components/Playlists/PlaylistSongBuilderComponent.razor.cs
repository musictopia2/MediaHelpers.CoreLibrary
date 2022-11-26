namespace MediaHelpers.CoreLibrary.Music.Components.Playlists;
public partial class PlaylistSongBuilderComponent
{
    [Parameter]
    public EventCallback<EnumPlaylistUIStage> StageChanged { get; set; }
    [Inject]
    private PlaylistSongBuilderViewModel? DataContext { get; set; }
    private InputTabOrderNavigationContainer? _navs;
    protected override void OnInitialized()
    {
        _navs = null;
        DataContext!.FocusFirst = async () => await _navs!.FocusFirstAsync();
        DataContext.StartLoadingSongs = () => StageChanged.InvokeAsync(EnumPlaylistUIStage.Other);
        base.OnInitialized();
    }
}