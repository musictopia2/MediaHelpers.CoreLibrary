namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class TelevisionShellComponent
{
    [Inject]
    private ITelevisionShellViewModel? DataContext { get; set; }
    [Inject]
    private ITelevisionVideoLoader? Loader { get; set; }
    private bool _hadPrevious;
    protected override async Task OnInitializedAsync()
    {
        await DataContext!.InitAsync();
        if (DataContext.PreviousEpisode is not null)
        {
            _hadPrevious = true;
            Loader!.ChoseEpisode(DataContext.PreviousEpisode);
        }
    }
}