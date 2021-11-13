namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
/// <summary>
/// this is all the logic parts for playlist songs that is not quite related to actually playing the song.
/// however, one class can implement both.
/// </summary>
public interface IPlaylistSongMainLogic
{
    Task<int?> GetMostRecentPlaylistAsync();
    Task<BasicList<IPlayListMain>> GetMainPlaylistsAsync();
    Task ClearSongsAsync(int playlist);
    Task DeleteCurrentPlayListAsync(int playlist);
    Task<bool> HasPlaylistCreatedAsync(int playlist);
    Task SetMainPlaylistAsync(int id);
    Task<BasicList<IPlayListDetail>> GetPlaylistDetailsAsync();
    Task<int> ChooseSongsAsync(IPlayListDetail detail, int percentage, int howmanySongs);
    Task CreatePlaylistSongsAsync();
}