namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.Movies;
public interface IMainMovieTable
{
    int ID { get; set; }
    string Title { get; set; }
    DateOnly? LastWatched { get; set; } //now i can support dateonly.  the time does not matter when i last watched.
    int? ResumeAt { get; set; }
    string FullPath(); //now must be full path
    int? Opening { get; set; }
    int? Closing { get; set; }
    //now allow the ability to skip scenes (not just the traditional stuff)
    BasicList<SkipSceneClass> Skips { get; set; }
}