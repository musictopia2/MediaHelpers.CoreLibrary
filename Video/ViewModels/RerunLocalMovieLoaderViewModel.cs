namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class RerunLocalMovieLoaderViewModel<M> : BaseLocalMovieLoaderViewModel<M, BasicMoviesModel>
    where M : class, IMainMovieTable
{
    public RerunLocalMovieLoaderViewModel(IFullVideoPlayer player,
        IRerunMovieLoaderLogic<M> loader,
        MovieContainerClass<M> movieContainer,
        IRerunMoviesRemoveControlHostService<BasicMoviesModel> hostService,
        IExit exit,
        ISystemError error) : base(player, loader, movieContainer, hostService, exit, error)
    {
        hostService.ExitEarly = async () =>
        {
            await loader.FinishMovieAsync(SelectedItem!);
        };
        hostService.EditLater = async () =>
        {
            await loader.EditMovieLaterAsync(SelectedItem!);
        };
    }
    protected override bool CanInitializeRemoteControlAfterPlayerInit => true;
}