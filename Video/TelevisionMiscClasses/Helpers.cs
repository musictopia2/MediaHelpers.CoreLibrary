namespace MediaHelpers.CoreLibrary.Video.TelevisionMiscClasses;
public static class Helpers
{
    public static int GetSeconds(this IEpisodeTable episode, IStartLoaderTelevisionContext data)
    {
        data.CurrentEpisode = episode;
        int secs = data.Seconds;
        if (secs == 0 && data.CurrentEpisode.AlwaysSkipBeginning == true && data.CurrentEpisode.OpeningLength > 0)
        {
            secs = data.CurrentEpisode.OpeningLength.Value;
            if (data.CurrentEpisode.StartAt.HasValue == true)
            {
                secs += data.CurrentEpisode.StartAt!.Value;
            }
            data.Seconds = secs;
        }
        else if (secs == 0 && data.CurrentEpisode.StartAt.HasValue == true)
        {
            secs = data.CurrentEpisode.StartAt!.Value;
            data.Seconds = secs;
        }
        return secs;
    }
    //i think it needs to be global.
    public static int? EpisodeChosen { get; set; }
}