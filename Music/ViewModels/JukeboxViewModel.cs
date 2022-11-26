namespace MediaHelpers.CoreLibrary.Music.ViewModels;
public class JukeboxViewModel
{
    private readonly IJukeboxLogic _jukebox;
    private readonly IDatePicker _datePicker;
    public JukeboxViewModel(IJukeboxLogic jukebox,
        IDatePicker datePicker
        )
    {
        _jukebox = jukebox;
        _datePicker = datePicker;
        SongsToPlay = _jukebox.SongsToPlay;
        IsChristmas = CalculateChristmas();
        LoadArtistList();
    }
    public EnumJukeboxSearchOption SearchOption { get; set; }
    public BasicList<SongResult> SongsToPlay { get; private set; }
    public string SearchTerm { get; set; } = "";
    public bool IsChristmas { get; set; }
    public ArtistResult? ArtistChosen { get; set; }
    public SongResult? SongChosen { get; set; }
    public SongResult? DeleteChosen { get; set; }
    public string ResultsText { get; set; } = "";
    public BasicList<SongResult> ResultList { get; set; } = new();
    public BasicList<ArtistResult> Artists { get; set; } = new();
    public bool CanChooseArtist => ArtistChosen != null;
    public Action<EnumFocusCategory>? ComboFocus { get; set; }
    public void ChooseArtist()
    {
        ResultList = _jukebox.GetSongList(EnumJukeboxSearchOption.Artist, ArtistChosen, IsChristmas, SearchTerm);
        ResultsText = ResultList.Count.ToString();
        ComboFocus?.Invoke(EnumFocusCategory.Results);
    }
    public bool CanAddSongToList
    {
        get
        {
            if (SongChosen == null)
            {
                return false;
            }
            if (SongsToPlay.Exists(x => x.ID == SongChosen.ID))
            {
                return false;
            }
            return true;
        }
    }
    public async Task AddSongToListAsync()
    {
        if (SongChosen == null)
        {
            throw new CustomBasicException("Cannot add to song list when its null");
        }
        await _jukebox.AddSongToListAsync(SongChosen);
        SongChosen = null;
    }
    public bool CanSearchSongs
    {
        get
        {
            if (string.IsNullOrWhiteSpace(SearchTerm) == true || SearchOption == EnumJukeboxSearchOption.Artist || SearchOption == EnumJukeboxSearchOption.None)
            {
                return false;
            }
            return true;
        }
    }
    public void SearchSongs()
    {
        ResultList = _jukebox.GetSongList(SearchOption, ArtistChosen, IsChristmas, SearchTerm);
        ResultsText = ResultList.Count.ToString();
        ComboFocus?.Invoke(EnumFocusCategory.Results);
    }
    public bool CanRemoveSongFromList => DeleteChosen != null;
    public async Task RemoveSongFromListAsync()
    {
        if (DeleteChosen == null)
        {
            throw new CustomBasicException("Cannot remove song from list when its null");
        }
        await _jukebox.RemoveSongFromListAsync(DeleteChosen);
        DeleteChosen = null;
    }
    private bool CalculateChristmas()
    {
        DateTime date = _datePicker.GetCurrentDate;
        if (date.Month == 12 && date.Day <= 25)
        {
            return true;
        }
        return false;
    }
    public void ChristmasToggle()
    {
        IsChristmas = !IsChristmas;
        LoadArtistList();
    }
    public void LoadArtistList()
    {
        Artists = _jukebox.GetArtistList(IsChristmas);
        ComboFocus?.Invoke(EnumFocusCategory.Artist);
    }
}