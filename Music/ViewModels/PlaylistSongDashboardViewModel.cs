namespace MediaHelpers.CoreLibrary.Music.ViewModels;
public class PlaylistSongDashboardViewModel(IPlaylistSongMainLogic logic)
{
    public async Task InitAsync()
    {
        Playlists = await logic.GetMainPlaylistsAsync();
        _recentPlaylist = await logic.GetMostRecentPlaylistAsync();
    }
    public EnumPlayListOption PlayListOption { get; set; } = EnumPlayListOption.PlaySongsDefault;
    private int? _recentPlaylist;
    public IPlayListMain? ChosenPlayList { get; set; }
    public BasicList<IPlayListMain> Playlists { get; private set; } = [];
    public bool DidChoosePlayList => ChosenPlayList is not null;
    public Action<EnumPlaylistUIStage>? StageChanged { get; set; }
    private async Task StartChoosingSongsAsync(int id)
    {
        await logic.SetMainPlaylistAsync(id);
        StageChanged?.Invoke(EnumPlaylistUIStage.ChooseSections);
    }
    public async Task SmartChooseOptionAsync()
    {
        if (ChosenPlayList == null)
        {
            throw new CustomBasicException("Needs to have a chosen playlist in order to use smart playlist options.  Rethink");
        }
        switch (PlayListOption)
        {
            case EnumPlayListOption.PlaySongsDefault:
                await PlayChosenPlaylistAsync(ChosenPlayList.ID);
                break;
            case EnumPlayListOption.ClearPlayLists:
                await logic.ClearSongsAsync(ChosenPlayList.ID);
                await StartChoosingSongsAsync(ChosenPlayList.ID);
                break;
            case EnumPlayListOption.DeletePlayLists:
                await DeletePlayListAsync(ChosenPlayList.ID);
                break;
            default:
                throw new CustomBasicException("Option not supported for smart playlist option");
        }
        PlayListOption = EnumPlayListOption.PlaySongsDefault; //back to this one.
    }
    private async Task PlayChosenPlaylistAsync(int id)
    {
        if (await logic.HasPlaylistCreatedAsync(id) == false)
        {
            await StartChoosingSongsAsync(id);
            return;
        }
        await logic.SetMainPlaylistAsync(id);
        StageChanged?.Invoke(EnumPlaylistUIStage.Other);
    }
    private async Task DeletePlayListAsync(int id)
    {
        await logic.DeleteCurrentPlayListAsync(id);
        ChosenPlayList = null;
        Playlists = await logic.GetMainPlaylistsAsync();
        FocusCombo?.Invoke();
    }
    public Action? FocusCombo { get; set; }
    public bool CanChooseRecentPlayList => _recentPlaylist != null;
    public async Task ChooseRecentPlayListAsync()
    {
        if (_recentPlaylist == null)
        {
            throw new CustomBasicException("Can't choose recent playlist because there was none");
        }
        await PlayChosenPlaylistAsync(_recentPlaylist.Value);
    }
}