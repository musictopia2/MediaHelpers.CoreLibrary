namespace MediaHelpers.CoreLibrary.Music.ViewModels;
public class PlaylistSongBuilderViewModel
{
    private readonly IPlaylistSongMainLogic _logic;
    public PlaylistSongBuilderViewModel(IPlaylistSongMainLogic logic)
    {
        _logic = logic;
        InitAsync();
    }
    private async void InitAsync()
    {
        SubLists = await _logic.GetPlaylistDetailsAsync();
    }
    public BasicList<IPlayListDetail> SubLists { get; set; } = new();
    public Action? FocusFirst { get; set; }
    public int SongsActuallyChosen { get; set; }
    public int Percentage { get; set; } = 90;
    public int HowManySongsWanted { get; set; }
    private void ClearSelections()
    {
        HowManySongsWanted = 0;
        Percentage = 90;
    }
    public async Task ChooseSongsAsync()
    {
        if (SubLists.Count == 0)
        {
            throw new CustomBasicException("Can't have 0 songs.  Otherwise, rethinking is required");
        }
        int actuallyChosen = await _logic.ChooseSongsAsync(SubLists.First(), Percentage, HowManySongsWanted);
        SongsActuallyChosen += actuallyChosen;
        SubLists.RemoveFirstItem();
        if (SubLists.Count == 0)
        {
            await _logic.CreatePlaylistSongsAsync();

            StartLoadingSongs?.Invoke();
            return;
        }
        ClearSelections();
        FocusFirst?.Invoke();
    }
    public Action? StartLoadingSongs { get; set; }
}