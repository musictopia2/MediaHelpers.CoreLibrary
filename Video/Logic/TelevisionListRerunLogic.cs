namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListRerunLogic : ITelevisionListLogic
{
    private readonly ITelevisionContext _data;
    private readonly IMessageBox _message;
    private readonly IExit _exit;

    public TelevisionListRerunLogic(ITelevisionContext data, IMessageBox message, IExit exit)
    {
        _data = data;
        _message = message;
        _exit = exit;
    }
    async Task<IEpisodeTable?> ITelevisionListLogic.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        var episode = _data.GenerateNewRerunEpisode(selectedItem.ID);
        if (episode == null)
        {
            await _message.ShowMessageAsync($"There are no more episodes that can be chosen for {selectedItem.ShowName}");
            _exit.ExitApp();
            return null;
        }
        return episode;
    }
    async Task<BasicList<IShowTable>> ITelevisionListLogic.GetShowListAsync()
    {
        await Task.CompletedTask;
        return _data.ListShows(EnumTelevisionCategory.Reruns);
    }
}