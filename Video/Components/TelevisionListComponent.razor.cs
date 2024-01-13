namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class TelevisionListComponent<E>
    where E: class, IEpisodeTable
{
    [Inject]
    private TelevisionListViewModel<E>? DataContext { get; set; }
    [Inject]
    private ITelevisionVideoLoader<E>? Loader { get; set; }
    private async Task DoChooseShowAsync()
    {
        E? ee = await DataContext!.GetEpisodeChosenAsync() ?? throw new CustomBasicException("There was no episode chosen");
        Loader!.ChoseEpisode(ee);
    }
}