namespace MediaHelpers.CoreLibrary.Music.RemoteControls;
public interface IMusicRemoteControlDataAccess
{
    Task IncreaseWeightAsync(IPlaySong song);
    Task DecreaseWeightAsync(IPlaySong song);
    int GetWeight(IPlaySong song);
    Task DeleteSongAsync(IPlaySong song);
}