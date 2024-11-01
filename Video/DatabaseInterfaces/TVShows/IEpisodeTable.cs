﻿namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IEpisodeTable
{
    int ID { get; set; }
    int Show { get; set; }
    int? StartAt { get; set; }
    int? OpeningLength { get; set; }
    int? ClosingLength { get; set; }
    //maybe does not even need whether the opening was skipped or not now
    //bool AlreadySkippedOpening { get; set; }
    //bool Finished { get; set; }
    bool AlwaysSkipBeginning { get; set; }
    //needs this for backward compatibility.
    int BeginAt { get; set; }
    //if you use new version, will need to enable the skiplist.
    //more stuff may be needed but this is a first step.
    BasicList<SkipSceneClass> Skips { get; set; }
    int? ResumeAt { get; set; }
    bool WatchedAtLeastOnce { get; set; }
    int? EpisodeNumber { get; set; }
    int UpTo { get; set; }
    EnumTelevisionHoliday? Holiday { get; set; }
    //this is now questionable.  bad news is even though bad design, don't know how to do without figuring out even more names (?)
    string FullPath();
    IShowTable ShowTable { get; }
    bool CanEdit { get; } // determine whether the episode can even be edited.
}
