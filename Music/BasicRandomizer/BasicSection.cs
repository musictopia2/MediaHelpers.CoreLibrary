namespace MediaHelpers.CoreLibrary.Music.BasicRandomizer;
public class BasicSection
{
    public IEnumerable<IBaseSong>? SongList { get; set; }
    public int HowManySongs { get; set; }
    public int Percent { get; set; } = 90;
    public bool ChooseAll { get; set; }
}