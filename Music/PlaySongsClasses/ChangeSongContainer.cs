namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
public class ChangeSongContainer
{
    public Func<IPlaySong, int, Task>? UpdateSongAsync { get; set; }
    public Action? UpdatePlaylist { get; set; }
}