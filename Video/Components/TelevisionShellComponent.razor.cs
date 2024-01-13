namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class TelevisionShellComponent<E>
    where E: class, IEpisodeTable
{
    [Inject]
    private ITelevisionShellViewModel<E>? DataContext { get; set; }
    [Inject]
    private ITelevisionVideoLoader<E>? Loader { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool ShowContentManually { get; set; }
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