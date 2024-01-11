namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IStartBasicTelevisionContext
{
    IEpisodeTable? CurrentEpisode { get; set; } //this is needed so if there is a previous show, can set it to use later.
    Task<IEpisodeTable?> GetNextEpisodeAsync(int showID);
    //has to be async so if the newing up has to call it, can work.
}