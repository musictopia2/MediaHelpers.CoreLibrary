namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionListRerunLogic(IRerunTelevisionContext data, IMessageBox message, IExit exit) : ITelevisionListLogic
{
    async Task<IEpisodeTable?> ITelevisionListLogic.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        IEpisodeTable? episode = await data.GetNextEpisodeAsync(selectedItem.ID);
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