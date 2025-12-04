namespace MediaHelpers.CoreLibrary.Music.Components.Jukebox;
public partial class JukeboxTopComponent
{
    [Inject]
    private JukeboxViewModel? DataContext { get; set; }
    [Inject]
    private ChangeSongContainer? Container { get; set; }
    [Inject]
    private BasicSongProgressViewModel? ProgressContext { get; set; }
    [Parameter]
    public EventCallback OnJukeboxUpdating { get; set; }
    private FullComboGenericLayout<ArtistResult>? _artistCombo;
    private FullComboGenericLayout<SongResult>? _resultCombo;
    private static string StandardFontSize => "20px";
    private string ResultsText => DataContext!.ResultsText == "" ? "Results: " : $"Results: {DataContext.ResultsText}";
    private static string ComboHeight => "400px"; //keep at 400 pixels.  that seems to be okay.
    private AutoCompleteStyleModel _lastModel = new();
    protected override void OnInitialized()
    {
        _artistCombo = null;
        _resultCombo = null;
        _lastModel.ComboTextColor = cc1.Green.ToWebColor;
        Container!.UpdatePlaylist = () => InvokeAsync(StateHasChanged);
        DataContext!.ComboFocus = async (item) =>
        {
            if (item == EnumFocusCategory.Artist && _artistCombo is not null)
            {
                await _artistCombo.FocusAsync();
            }
            if (item == EnumFocusCategory.Results && _resultCombo is not null)
            {
                await _resultCombo.FocusAsync();
            }
        };
        base.OnInitialized();
    }
    private async Task RemoveFromPlayListAsync()
    {
        if (DataContext!.CanRemoveSongFromList == false)
        {
            return;
        }
        await DataContext.RemoveSongFromListAsync();
    }
    private string ChristmasText => DataContext!.IsChristmas ? "Non Christmas Only" : "Christmas Only";
    private void SearchSongs()
    {
        if (DataContext!.SearchTerm == "")
        {
            return;
        }
        DataContext.SearchSongs();
    }
}