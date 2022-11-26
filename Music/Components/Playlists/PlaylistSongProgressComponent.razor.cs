namespace MediaHelpers.CoreLibrary.Music.Components.Playlists;
public partial class PlaylistSongProgressComponent
{
    [Inject]
    private PlaylistSongProgressViewModel? DataContext { get; set; }
    [Parameter]
    public bool AlsoStart { get; set; } = true;
}