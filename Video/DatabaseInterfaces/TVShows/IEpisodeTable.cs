namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IEpisodeTable
{
    int ID { get; set; }
    int Show { get; set; }
    int? StartAt { get; set; }
    int? OpeningLength { get; set; }
    int? ClosingLength { get; set; }
    bool AlreadySkippedOpening { get; set; }
    bool Finished { get; set; }
    bool AlwaysSkipBeginning { get; set; }
    int BeginAt { get; set; }
    int? ResumeAt { get; set; }
    bool WatchedAtLeastOnce { get; set; }
    int? EpisodeNumber { get; set; }
    int UpTo { get; set; }
    EnumTelevisionHoliday? Holiday { get; set; }
    string FullPath();
    IShowTable ShowTable { get; }
}
