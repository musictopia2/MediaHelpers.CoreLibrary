namespace MediaHelpers.CoreLibrary.Music.PlaySongsClasses;
public class PlaylistSongsLogic<P> : IPlaylistSongMainLogic, IPlaylistSongProgressPlayer
    where P : BasicPlayListData, new()
{
    private readonly IPlaylistMusicDataAccess _data;
    private readonly IMP3Player _mp3;
    private readonly ChangeSongContainer _changeSong;
    private readonly ISongListCreater _query;
    private readonly IPlayListInfo _playListInfo;
    private IBaseSong? _currentSong;
    private readonly IMusicShuffleProcesses _ras;
    private readonly IExit _exit;
    private readonly ISystemError _error;
    public PlaylistSongsLogic(IPlaylistMusicDataAccess data,
        IMP3Player mp3,
        ChangeSongContainer changeSong,
        ISongListCreater query,
        IPlayListInfo playListInfo,
        IMusicShuffleProcesses ras,
        IExit exit,
        ISystemError error)
    {
        _error = error;
        _data = data;
        _mp3 = mp3;
        _ras = ras;
        _exit = exit;
        _changeSong = changeSong;
        _query = query;
        _playListInfo = playListInfo;
    }
    public int UpTo { get; set; }
    public int SongsLeft { get; set; }
    public Action UpdateProgress { get; set; } = () => { };
    private int? _playlistId;
    private BasicList<IBaseSong> _songs = new();
    Task IPlaylistSongMainLogic.ClearSongsAsync(int playlist)
    {
        return _data.ClearSongsAsync(playlist);
    }
    Task IPlaylistSongMainLogic.DeleteCurrentPlayListAsync(int playlist)
    {
        return _data.DeletePlayListAsync(playlist);
    }
    async Task<BasicList<IPlayListMain>> IPlaylistSongMainLogic.GetMainPlaylistsAsync()
    {
        await Task.CompletedTask;
        return _data.GetAllPlaylists();
    }
    async Task<int?> IPlaylistSongMainLogic.GetMostRecentPlaylistAsync()
    {
        await Task.CompletedTask;
        return _data.CurrentPlayList;
    }
    async Task<BasicList<IPlayListDetail>> IPlaylistSongMainLogic.GetPlaylistDetailsAsync()
    {
        if (_playlistId == null)
        {
            throw new CustomBasicException("Never saved the playlistid for the playlist used.  Rethink");
        }
        await Task.CompletedTask;
        _ras.Clear();
        return _data.GetPlayListDetails(_playlistId.Value).ToBasicList();
    }
    async Task<bool> IPlaylistSongMainLogic.HasPlaylistCreatedAsync(int playlist)
    {
        await Task.CompletedTask;
        return _data.HasPlayListCreated(playlist);
    }
    async Task IPlaylistSongMainLogic.SetMainPlaylistAsync(int id)
    {
        await _data.SetCurrentPlayListAsync(id);
        _playlistId = id;
        await StartUpAsync();
    }
    public Task SongInProgressAsync(int resumeAt)
    {
        if (_songs.Count == 0)
        {
            throw new CustomBasicException("There are no songs.  Rethink");
        }
        if (UpTo == 0)
        {
            throw new CustomBasicException("Upto must be at least one.  Otherwise, rethink");
        }
        if (_playlistId == null)
        {
            throw new CustomBasicException("Must have a playlist associated.  Otherwise, can't show song progress");
        }
        _currentSong = _songs[UpTo - 1];
        return _data.UpdatePlayListProgressAsync(resumeAt, _currentSong.SongNumber, _playlistId.Value);
    }
    async Task<int> IPlaylistSongMainLogic.ChooseSongsAsync(IPlayListDetail detail, int percentage, int howmanySongs)
    {
        BasicSection section = new()
        {
            Percent = percentage,
            HowManySongs = howmanySongs
        };
        P data = await fs.DeserializeObjectAsync<P>(detail.JsonData);
        BasicList<ICondition> firstList = _query.GetMusicList(data);
        section.SongList = _data.GetCompleteSongList(firstList);
        if (section.SongList.Any() == false)
        {
            throw new CustomBasicException("I know this play list has more than 0 songs");
        }
        await _ras.AddSectionAsync(section);
        int sofar = _ras.SongsSoFar();
        if (sofar == 0)
        {
            throw new CustomBasicException("Can't actually choose 0 songs");
        }
        return sofar;
    }
    async Task IPlaylistSongMainLogic.CreatePlaylistSongsAsync()
    {
        if (_playlistId == null)
        {
            throw new CustomBasicException("Never created playlist id.  Rethink");
        }
        var list = await _ras.GetRandomListAsync();
        var newList = new BasicList<IPlayListSong>();
        list.ForEach(song =>
        {
            IPlayListSong fins = _playListInfo.GetNewPlayListSong();
            if (fins.ID != 0)
            {
                throw new CustomBasicException("Did not use new object");
            }
            fins.SongNumber = list.IndexOf(song) + 1;
            fins.PlayList = _playlistId.Value;
            fins.SongID = song.ID;
            newList.Add(fins);
        });
        await _data.AddSeveralPlayListSongsAsync(newList);
        await StartUpAsync();
    }
    private async Task<bool> NextSongAsync()
    {
        if (_playlistId == null)
        {
            throw new CustomBasicException("Can't go to next song because we don't have a playlist id.  rethink");
        }
        _mp3.StopPlay();
        if (SongsLeft == 0)
        {
            await _data.ErasePlayListAsync(_playlistId.Value);
            _exit.ExitApp();
            return false;
        }
        UpTo++;
        SongsLeft--;
        await SongInProgressAsync(0);
        UpdateProgress.Invoke();
        if (_currentSong == null)
        {
            throw new CustomBasicException("No current song was sent.  Rethink");
        }
        if (_currentSong.DeleteThis)
        {
            return await NextSongAsync();
        }
        await _changeSong.UpdateSongAsync?.Invoke(_currentSong, 0)!;
        return true;
    }
    async Task<bool> IProgressMusicPlayer.NextSongAsync()
    {
        return await NextSongAsync();
        
    }
    private async Task StartUpAsync()
    {
        if (_playlistId == null)
        {
            throw new CustomBasicException("There was no playlist id.  Therefore, can't even start up.  Rethink");
        }
        var progress = _data.GetSinglePlayListProgress(_playlistId.Value);
        var tempsongs = _data.GetPlayListSongs(_playlistId.Value);
        if (tempsongs.Any() == false)
        {
            return;
        }
        int resumeat = 0;
        await _data.PerformAdvancedMusicProcessAsync(async (cons, trans) =>
        {
            if (progress == null)
            {
                progress = _playListInfo.GetNewPlayListProgress();
                progress.PlayList = _playlistId.Value;
                progress.ResumeAt = 0;
                progress.SongNumber = 1;
                UpTo = 1;
                resumeat = 0;
                await _data.AddNewPlayListProgressAsync(progress, cons, trans);
            }
            else
            {
                resumeat = progress.ResumeAt;
                UpTo = progress.SongNumber;
            }
            _songs = new();
            foreach (IPlayListSong thisTemp in tempsongs)
            {
                if (thisTemp.Song == null)
                {
                    _error.ShowSystemError("The Song From PlayList Song Was Nothing.  Rethink");
                    break;
                }
                thisTemp.Song.SongNumber = thisTemp.SongNumber;
                _songs.Add(thisTemp.Song);
            }
            trans.Commit();
        });
        SongsLeft = _songs.Count - UpTo; 
        try
        {
            _currentSong = _songs[UpTo - 1];
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
            return;
        }
        UpdateProgress.Invoke();
        if (_currentSong.DeleteThis)
        {
            await NextSongAsync();
            return;
        }
        await _changeSong.UpdateSongAsync?.Invoke(_currentSong, resumeat)!;
    }
}