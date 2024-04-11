namespace MediaHelpers.CoreLibrary.Music.DB.DataAccess;
/// <summary>
/// this is intended to be used for playlist processes.
/// since this require extra functions.
/// </summary>
public interface IPlaylistMusicDataAccess : ISimpleMusicDataAccess
{
    BasicList<IPlayListMain> GetAllPlaylists();
    Task ErasePlayListAsync(int id);
    Task DeletePlayListAsync(int id);
    Task ClearSongsAsync(int id);
    IEnumerable<IPlayListSong> GetPlayListSongs(int playList); //has to be ienumerable unfortunatley.
    IEnumerable<IPlayListDetail> GetPlayListDetails(int playList); //we don't have iqueryable.
    bool HasPlayListCreated(int playList);
    int? CurrentPlayList { get; } //will be read only.
    Task SetCurrentPlayListAsync(int? playList);
    Task UpdatePlayListProgressAsync(int secs, int songNumber, int playList);
    IPlayListProgress GetSinglePlayListProgress(int playlistid);
    Task PerformAdvancedMusicProcessAsync(Func<ICaptureCommandParameter, IDbTransaction, Task> action);
    Task AddNewPlayListProgressAsync(IPlayListProgress progress, ICaptureCommandParameter capture, IDbTransaction trans);
    Task AddSeveralPlayListSongsAsync(IEnumerable<IPlayListSong> songlist);
}