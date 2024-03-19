namespace MediaHelpers.CoreLibrary.Music.Components.Playlists;
public partial class PlaylistShellComponent
{
    public static Type? PlaylistCreaterRenderType { get; set; }
    public static bool PartOfCreater { get; set; }
    private static EnumPlaylistUIStage _stage;
    private static Action? PublicStateChanged { get; set; }
    [Inject]
    private BasicSongProgressViewModel? DataContext { get; set; }
    private bool _needsToClear;
    private void PrivateSetStage(EnumPlaylistUIStage stage)
    {
        if (DataContext!.IsSongPlaying && _stage == EnumPlaylistUIStage.Other)
        {
            DataContext.Stop();
            _needsToClear = true;
        }
        else
        {
            _needsToClear = false;
        }
        PartOfCreater = false;
        _stage = stage;
    }
    public static void PublicSetStage(EnumPlaylistUIStage stage)
    {
        _stage = stage;
        if (stage != EnumPlaylistUIStage.Creater)
        {
            PartOfCreater = true;
        }
        else
        {
            PartOfCreater = false;
        }
        PublicStateChanged?.Invoke();
    }
    protected override void OnInitialized()
    {
        PublicStateChanged = () => InvokeAsync(StateHasChanged);
        base.OnInitialized();
    }
}