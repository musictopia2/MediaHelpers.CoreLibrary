namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.Movies;
public interface IMainMovieTable
{
    int ID { get; set; }
    string Title { get; set; }
    DateTime? LastWatched { get; set; }
    int? ResumeAt { get; set; }
    string Path { get; set; }
    int? Opening { get; set; }
    int? Closing { get; set; }
}