namespace MediaHelpers.CoreLibrary.Video.Logic;
//this may be list or may be loader.
public class TelevisionBasicRerunNextEpisodeLogic(IStartBasicTelevisionContext data, IMessageBox message, IExit exit) : INextEpisodeLogic
{
    async Task<IEpisodeTable?> INextEpisodeLogic.GetNextEpisodeAsync(IShowTable selectedItem)
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
}