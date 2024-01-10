namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListRerunLogic(ITelevisionContext data, IMessageBox message, IExit exit) : ITelevisionListLogic
{
    async Task<IEpisodeTable?> ITelevisionListLogic.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        var episode = data.GenerateNewRerunEpisode(selectedItem.ID);
        if (episode == null)
        {
            await message.ShowMessageAsync($"There are no more episodes that can be chosen for {selectedItem.ShowName}");
            exit.ExitApp();
            return null;
        }
        return episode;
    }
    Task<BasicList<IShowTable>> ITelevisionListLogic.GetShowListAsync()
    {
        return data.ListShowsAsync();
    }
}