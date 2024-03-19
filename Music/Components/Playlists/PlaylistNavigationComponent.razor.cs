namespace MediaHelpers.CoreLibrary.Music.Components.Playlists;
public partial class PlaylistNavigationComponent
{
    [Parameter]
    public EnumPlaylistUIStage Stage { get; set; }
    [Parameter]
    public EventCallback<EnumPlaylistUIStage> StageChanged { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    public static Action? OnBackToCreater { get; set; }
    private bool SongsEnabled => Stage != EnumPlaylistUIStage.Songs;
    private bool CreaterEnabled => Stage != EnumPlaylistUIStage.Creater;
    private void PrivateClicked(EnumPlaylistUIStage stage)
    {
        if (stage != EnumPlaylistUIStage.Creater && stage != EnumPlaylistUIStage.Songs)
        {
            throw new CustomBasicException("Only songs and creater sections are supported in the navigation component");
        }
        if (stage == EnumPlaylistUIStage.Creater)
        {
            OnBackToCreater?.Invoke();
        }
        StageChanged.InvokeAsync(stage);
    }
}