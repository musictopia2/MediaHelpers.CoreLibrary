namespace MediaHelpers.CoreLibrary.Music.PlayListCreaterClasses;
public class BasicPlayListData
{
    public int Artist { get; set; }
    public int EarliestYear { get; set; }
    public int LatestYear { get; set; }
    public string SpecializedFormat { get; set; } = "";
    public string ShowType { get; set; } = "";
    public bool? Romantic { get; set; }
    public bool? Tropical { get; set; }
    public bool? Christmas { get; set; }
    public bool? WorkOut { get; set; }
    public bool UseLikeInSpecializedFormat { get; set; }
    public BasicList<int> SongList { get; set; } = new();
    protected bool CanResetChristmas = true;
    protected bool CanClearSongList = true;
    public virtual void SetDefaults()
    {
        if (CanResetChristmas == true)
        {
            Christmas = false;
        }
        Artist = 0;
        WorkOut = null;
        Tropical = null;
        if (CanClearSongList == true)
        {
            SongList.Clear();
        }
        Romantic = null;
        LatestYear = 0;
        EarliestYear = 0;
        ShowType = "";
        SpecializedFormat = "";
    }
}