namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
public class PrepareSong : IPrepareSong
{
    private readonly IMP3Player _mP3;
    private readonly ISystemError _error;
    public static bool ShowErrors { get; set; }
    public PrepareSong(IMP3Player mP3, ISystemError error)
    {
        _mP3 = mP3;
        _error = error;
    }
    private static bool IsMp3(string path)
    {
        if (path.ToLower().EndsWith(".mp3") == true)
        {
            return true;
        }
        if (path.ToLower().EndsWith(".and") == true)
        {
            return false;
        }
        throw new CustomBasicException("Must End With .mp3 or .and");
    }
    private static string ConvertToMp3(string path)
    {
        return $"{path.Substring(0, path.Length - 4)}.mp3";
    }
    async Task<bool> IPrepareSong.PrepareSongAsync(IBaseSong currentSong, int resumeAt)
    {
        _mP3.StopPlay();
        string paths = currentSong.FullPath;
        if (IsMp3(paths) == false)
        {
            string OldPath = currentSong.FullPath;
            paths = ConvertToMp3(currentSong.FullPath);
            await RenameFileAsync(OldPath, paths);
        }
        if (FileExists(paths) == false)
        {
            if (ShowErrors == true)
            {
                _error.ShowSystemError($"Cannot find the path {paths}  when trying to play a song.  Maybe the hard drive is having problems or you have been disconnected from the network");
                return false;
            }
        }
        _mP3.Path = paths;
        if (resumeAt == 0)
        {
            await _mP3.PlayAsync();
        }
        else
        {
            await _mP3.PlayAsync(resumeAt);
        }
        return true;
    }
}