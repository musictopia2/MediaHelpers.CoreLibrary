namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
public interface IProgressMusicPlayer
{
    /// <summary>
    /// this is everything that needs to happen for next song.
    /// if it returns false, then song is no longer playing.
    /// so jukebox can return false
    /// </summary>
    /// <returns></returns>
    Task<bool> NextSongAsync();
    /// <summary>
    /// something will send in the resumeat which this part needs to decide how it will handle it.
    /// if the interface does not need it, they can just ignore it.
    /// </summary>
    /// <param name="resumeAt"></param>
    /// <returns></returns>
    Task SongInProgressAsync(int resumeAt);
}