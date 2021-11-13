namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
public interface IJukeboxLogic
{
    BasicList<ArtistResult> GetArtistList(bool isChristmas);
    BasicList<SongResult> GetSongList(EnumJukeboxSearchOption searchOption, ArtistResult? artistChosen, bool isChristmas, string searchTerm);
    BasicList<SongResult> SongsToPlay { get; set; }
    Task AddSongToListAsync(SongResult song);
    Task RemoveSongFromListAsync(SongResult song);
}