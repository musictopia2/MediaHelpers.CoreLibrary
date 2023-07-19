//using System.Timers; //not common enough.
namespace MediaHelpers.CoreLibrary.Music.ViewModels;
public class BasicSongProgressViewModel : IPlayPauseClass, INextSongClass
{
    private readonly IMP3Player _mp3;
    private readonly IProgressMusicPlayer _player;
    private readonly IPrepareSong _prepare;
    private readonly IToast _toast;
    public static Func<bool> CanMusicPlay { get; set; } = () => true; //default to music can always play.
    public BasicSongProgressViewModel(IMP3Player mp3,
        IProgressMusicPlayer player,
        IPrepareSong prepare,
        ChangeSongContainer container,
        IToast toast
        )
    {
        _mp3 = mp3;
        _player = player;
        _prepare = prepare;
        _toast = toast;
        _mp3.ErrorRaised += MP3_ErrorRaised;
        container.UpdateSongAsync = UpdateSongAsync;
    }
    private bool _processing;
    private async void RunTask()
    {
        await Task.Delay(900);
        if (_processing == false)
        {
            return;
        }
        await FirstShowProgressAsync();
    }
    public Action? StateChanged { get; set; }
    private IPlaySong? _currentSong;
    public IPlaySong? CurrentSong
    {
        get => _currentSong;
        set
        {
            if (_currentSong is null && value is null)
            {
                return;
            }
            if (_currentSong is null)
            {
                _currentSong = value;
                IsSongPlaying = true;
                return;
            }
            if (value is null)
            {
                IsSongPlaying = false;
                _currentSong = null;
                return;
            }
            IsSongPlaying = true;
            _currentSong = value;
        }
    }
    private bool _isSongPlaying;
    public bool IsSongPlaying
    {
        get => _isSongPlaying;
        set
        {
            _isSongPlaying = value;
            if (value != _isSongPlaying)
            {
                if (value == false)
                {
                    CurrentSong = null;
                }
                StateChanged?.Invoke();
            }
        }
    }
    public string SongName => CurrentSong is null ? "" : CurrentSong.SongName;
    public string ArtistName => CurrentSong is null ? "" : CurrentSong.ArtistName;
    public int SongLength => CurrentSong is null ? 0 : CurrentSong.Length;
    public int ResumeAt { get; set; }
    public string ProgressText { get; set; } = "";
    public bool CanPause => CurrentSong is not null;
    public async Task NextSongAsync()
    {
        if (CanMusicPlay() == false)
        {
            return; //if you can't play the music, then don't even go to the next song.
        }
        IsSongPlaying = await _player.NextSongAsync();
        StateChanged?.Invoke();
    }
    public void PlayPause()
    {
        if (CanMusicPlay() == false)
        {
            return; //forgot that if you can't even play the music, then don't even play/pause music.
        }
        _mp3.Pause();
    }
    //private async void TimerElapsed(object sender, ElapsedEventArgs e)
    //{
    //    await FirstShowProgressAsync();
    //}
    private async Task FirstShowProgressAsync()
    {
        if (IsSongPlaying == false)
        {
            return;
        }
        bool isAllowed = CanMusicPlay();
        if (isAllowed == false)
        {
            //specialized stuff needs to happen.
            if (_mp3.GetState() == EnumMusicState.Playing)
            {
                _mp3.Pause(); //try this way.
            }
            //if (_mp3.IsPaused() == false)
            //{
            //    _mp3.Pause(); //do pause because its not allowed.
            //}
            //can't even send to remote control because not allowed.
            //Start(); //has to start again just in case you later can do something.
            LaterCheck(); //has to later check
            return;
        }
        if (_mp3.IsFinished())
        {
            IsSongPlaying = await _player.NextSongAsync();
            StateChanged?.Invoke();
            return;
        }
        ResumeAt = _mp3.TimeElapsedSeconds();
        ProgressText = ResumeAt.MusicProgressStringFromSeconds(SongLength);
        await SendRemoteControlProcessAsync();
        await _player.SongInProgressAsync(ResumeAt);
        Start();
        StateChanged?.Invoke();
    }
    protected virtual Task SendRemoteControlProcessAsync() { return Task.CompletedTask; }
    private void MP3_ErrorRaised(string message)
    {
        _toast.ShowUserErrorToast(message);
    }
    public async Task UpdateSongAsync(IBaseSong song, int resumeAt)
    {
        CurrentSong = song;
        ResumeAt = resumeAt;
        IsSongPlaying = await _prepare.PrepareSongAsync(CurrentSong, ResumeAt);
        Start();
        StateChanged?.Invoke();
    }
    private async void LaterCheck()
    {
        do
        {
            await Task.Delay(400);
            if (CanMusicPlay())
            {
                Start();
                return;
            }
        } while (true);
    }
    public async void Start()
    {
        _processing = true;
        await InitPossibleRemoteControl();
        RunTask();
    }
    protected virtual Task InitPossibleRemoteControl() { return Task.CompletedTask; }
    private async void LastUpdateAsync()
    {
        await _player.SongInProgressAsync(ResumeAt);
    }
    public void Stop()
    {
        LastUpdateAsync();
        IsSongPlaying = false;
        _mp3.StopPlay();
        _processing = false;
        StateChanged?.Invoke();
    }
}