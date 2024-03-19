namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
public class JukeboxLogic(IMP3Player mp3,
    ISimpleMusicDataAccess dats,
    ChangeSongContainer changeSong
        ) : IJukeboxLogic, IProgressMusicPlayer
{
    public BasicList<SongResult> SongsToPlay { get; set; } = [];
    private IBaseSong? _currentSong;
    async Task IJukeboxLogic.AddSongToListAsync(SongResult song)
    {
        SongsToPlay.Add(song);
        if (SongsToPlay.Count == 1)
        {
            _currentSong = dats.GetSong(song.ID);
            await changeSong.UpdateSongAsync?.Invoke(_currentSong!, 0)!;
        }
    }
    BasicList<ArtistResult> IJukeboxLogic.GetArtistList(bool isChristmas)
    {
        if (isChristmas == false)
        {
            var tempList = dats.GetSortedArtistList();
            return tempList.Select
                (xx => new ArtistResult { ArtistName = xx.ArtistName, ID = xx.ID }).ToBasicList();
        }
        else
        {
            var starts = StartWithOneCondition(nameof(IBaseSong.Christmas), true);
            var firstList = dats.GetCompleteSongList(starts);
            var nextList = firstList.Where(xx => xx.Christmas == true).ToBasicList();
            var groups = nextList.GroupBy(xx => new { xx.ArtistName, xx.ArtistID })
                .Select(Items => new ArtistResult { ArtistName = Items.Key.ArtistName, ID = Items.Key.ArtistID }).ToBasicList();
            groups.Sort();
            return groups;
        }
    }
    BasicList<SongResult> IJukeboxLogic.GetSongList(EnumJukeboxSearchOption searchOption, ArtistResult? artistChosen, bool isChristmas, string searchTerm)
    {
        if (searchOption == EnumJukeboxSearchOption.None || searchOption == EnumJukeboxSearchOption.Artist)
        {
            if (artistChosen == null)
            {
                return [];
            }
        }
        if (searchOption == EnumJukeboxSearchOption.Artist)
        {
            if (artistChosen == null)
            {
                throw new CustomBasicException("Must have an artist if choosing by artist.  Otherwise, rethink");
            }
            BasicList<ICondition> cList = StartWithOneCondition(nameof(IBaseSong.ArtistID), artistChosen.ID)
                .AppendCondition(nameof(IBaseSong.Christmas), isChristmas);
            var firstList = dats.GetCompleteSongList(cList, true);
            return firstList.Select(items =>
            new SongResult { ID = items.ID, PlayListDisplay = items.GetSongArtistDisplay(), ResultDisplay = items.GetSongArtistDisplay() }).ToBasicList();
        }
        BasicList<IBaseSong> nextList;
        if (searchOption == EnumJukeboxSearchOption.SpecificWords)
        {
            BasicList<ICondition> cList = StartWithOneCondition(nameof(IBaseSong.SongName), searchTerm)
                 .AppendCondition(nameof(IBaseSong.Christmas), isChristmas);
            var firstList = dats.GetCompleteSongList(cList, true);
            nextList = firstList.ToBasicList();
        }
        else
        {
            BasicList<ICondition> conList = [];
            if (isChristmas == true)
            {
                conList.AppendCondition(nameof(IBaseSong.Christmas), true);
            }
            conList.AppendCondition(nameof(IBaseSong.SongName), cs1.Like, searchTerm);
            var tempList = dats.GetCompleteSongList(conList, true);
            nextList = tempList.ToBasicList();
        }
        return nextList.OrderBy(items => items.SongName).Select(items =>
        new SongResult { ID = items.ID, PlayListDisplay = items.GetSongArtistDisplay(), ResultDisplay = items.GetSongArtistDisplay() }).ToBasicList();
    }
    public async Task<bool> NextSongAsync()
    {
        mp3.StopPlay();
        if (SongsToPlay.Count == 0)
        {
            throw new CustomBasicException("Cannot have 0 songs left to play");
        }
        SongsToPlay.RemoveFirstItem();
        changeSong.UpdatePlaylist?.Invoke();
        if (SongsToPlay.Count == 0)
        {
            return false;
        }
        _currentSong = dats.GetSong(SongsToPlay.First().ID);
        await changeSong.UpdateSongAsync?.Invoke(_currentSong, 0)!;
        return true;
    }
    async Task IJukeboxLogic.RemoveSongFromListAsync(SongResult song)
    {
        if (_currentSong == null)
        {
            throw new CustomBasicException("I think there should be a current song if removing song.  If I am wrong, rethink");
        }
        int id = song.ID;
        if (id == _currentSong.ID)
        {
            await NextSongAsync();
            return;
        }
        SongsToPlay.RemoveSpecificItem(song);
    }
    Task IProgressMusicPlayer.SongInProgressAsync(int resumeAt)
    {
        return Task.CompletedTask;
    }
}