namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IShowTable
{
    int ID { get; set; }
    string ShowName { get; set; }
    bool FinishedFirstRun { get; set; }
    EnumTelevisionLengthType LengthType { get; set; }
}