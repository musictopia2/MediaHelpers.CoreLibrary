namespace MediaHelpers.CoreLibrary.Video.Logic;
//this may be list or may be loader.
public class TelevisionBasicRerunNextEpisodeLogic<E>(IStartBasicTelevisionContext<E> data, IMessageBox message, IExit exit) : INextEpisodeLogic<E>
    where E: class, IEpisodeTable
{
    async Task<E?> INextEpisodeLogic<E>.GetNextEpisodeAsync(IShowTable selectedItem)
    {
        E? episode = await data.GetNextEpisodeAsync(selectedItem.ID);
        if (episode == null)
        {
            await message.ShowMessageAsync($"There are no more episodes that can be chosen for {selectedItem.ShowName}");
            //exit.ExitApp();
            Execute.OnUIThread(exit.ExitApp);
            return null;
        }
        return episode;
    }
}