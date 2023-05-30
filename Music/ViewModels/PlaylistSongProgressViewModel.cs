namespace MediaHelpers.CoreLibrary.Music.ViewModels;
public class PlaylistSongProgressViewModel : BasicSongProgressViewModel
{
    private readonly IPlaylistSongProgressPlayer _player;
    private readonly IMusicRemoteControlDataAccess _remoteData;
    private readonly IMusicRemoteControlHostService _hostService;
    public PlaylistSongProgressViewModel(IMP3Player mp3,
        IPlaylistSongProgressPlayer player,
        IPrepareSong prepare,
        ChangeSongContainer container,
        IMusicRemoteControlHostService hostService,
        IMusicRemoteControlDataAccess remoteData,
        IToast toast
        ) : base(mp3, player, prepare, container,toast)
    {
        _player = player;
        _hostService = hostService;
        _remoteData = remoteData;
        _player.UpdateProgress = UpdateProgress;
        _hostService.DeleteSong = async () =>
        {
            if (CanMusicPlay() == false)
            {
                return; //because if you can't play, then should not try to delete the song.
            }
            try
            {
                await remoteData.DeleteSongAsync(CurrentSong!);
                await _player.NextSongAsync(); //go to next song automatically as well.
            }
            catch (Exception)
            {

                throw;
            }
        };
        _hostService.IncreaseWeight = async () =>
        {
            if (CanMusicPlay() == false)
            {
                return; //because if you can't play, then should not try to delete the song.
            }
            await remoteData.IncreaseWeightAsync(CurrentSong!);
        };
        _hostService.DecreaseWeight = async () =>
        {
            if (CanMusicPlay() == false)
            {
                return; //because if you can't play, then should not try to delete the song.
            }
            await remoteData.DecreaseWeightAsync(CurrentSong!);
        };
        _hostService.PlayPause = () =>
        {
            Execute.OnUIThread(PlayPause);
        };
    }
    protected override Task InitPossibleRemoteControl()
    {
        TryToInitRemoteControl(); //just keep trying to initialize.
        return Task.CompletedTask;
        //await _hostService.InitializeAsync();
    }
    async void TryToInitRemoteControl()
    {
        await Task.Run(async () =>
        {
            do
            {
                try
                {
                    await _hostService!.InitializeAsync();
                    return;
                }
                catch (Exception)
                {
                    await Task.Delay(2000); //try again in 2 seconds.
                }

            } while (true);
        });
    }
    private void UpdateProgress()
    {
        SongsLeft = _player.SongsLeft;
        UpTo = _player.UpTo;
        StateChanged?.Invoke();
    }
    protected override async Task SendRemoteControlProcessAsync()
    {
        if (CurrentSong is not null)
        {
            int weight = _remoteData.GetWeight(CurrentSong);
            SongModel model = new(CurrentSong.SongName, CurrentSong.ArtistName, weight, ProgressText);
            await _hostService.SendProgressAsync(model);
        }
    }
    //looks like something else is needed.

    public int UpTo { get; set; }
    public int SongsLeft { get; set; }
}