namespace MediaHelpers.CoreLibrary.Music.RemoteControls;
public interface IMusicRemoteControlDataAccess
{
    Task IncreaseWeightAsync(IBaseSong song);
    Task DecreaseWeightAsync(IBaseSong song);
    int GetWeight(IBaseSong song);
    Task DeleteSongAsync(IBaseSong song);
}