namespace MediaHelpers.CoreLibrary.Music.DB.DataAccess;
public interface ISimpleMusicDataAccess : IAppendTropicalAccess
{
    IBaseSong GetSong(int id);
    IEnumerable<IBaseSong> GetCompleteSongList(BasicList<ICondition> extraConditions, bool sortByAristSong = false);
    IEnumerable<IArtist> GetSortedArtistList();
}