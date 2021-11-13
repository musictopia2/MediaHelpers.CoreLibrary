namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IShowTable
{
    int ID { get; set; }
    string ShowName { get; set; }
    int? OpeningLength { get; set; }
    int? ClosingLength { get; set; }
    bool FinishedFirstRun { get; set; }
    bool AlwaysSkipOpening { get; set; }
    EnumTelevisionLengthType LengthType { get; set; }
}