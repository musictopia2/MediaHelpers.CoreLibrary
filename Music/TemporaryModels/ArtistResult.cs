namespace MediaHelpers.CoreLibrary.Music.TemporaryModels;
public class ArtistResult : IComparable<ArtistResult>
{
    public int ID { get; set; }
    public string ArtistName { get; set; } = "";
    int IComparable<ArtistResult>.CompareTo(ArtistResult? other)
    {
        int temps = ArtistName.CompareTo(other!.ArtistName);
        if (temps != 0)
        {
            return temps;
        }
        return ID.CompareTo(other.ID);
    }
}