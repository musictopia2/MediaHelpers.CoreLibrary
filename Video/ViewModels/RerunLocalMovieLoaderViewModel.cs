﻿namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class RerunLocalMovieLoaderViewModel<M> : BaseLocalMovieLoaderViewModel<M, BasicMoviesModel>
    where M : class, IMainMovieTable
{
    private readonly IRerunMovieLoaderLogic<M> _loader;
    private readonly IExit _exit;

    public RerunLocalMovieLoaderViewModel(IFullVideoPlayer player,
        IRerunMovieLoaderLogic<M> loader,
        MovieContainerClass<M> movieContainer,
        IRerunMoviesRemoveControlHostService<BasicMoviesModel> hostService,
        IExit exit,
        ISystemError error) : base(player, loader, movieContainer, hostService, exit, error)
    {
        hostService.ExitEarly = ExitEarly;
        hostService.EditLater = EditLaterAsync;
        _exit = exit;
        _loader = loader;
    }
    private async Task EditLaterAsync()
    {
        if (SelectedItem is null)
        {
            return;
        }
        await Execute.OnUIThreadAsync(async () =>
        {
            var tempItem = StopEpisode();
            await _loader.EditMovieLaterAsync(tempItem);
            _exit.ExitApp();
        });
    }
    private async Task ExitEarly()
    {
        if (SelectedItem is null)
        {
            return;
        }
        await Execute.OnUIThreadAsync(async () =>
        {
            var tempItem = StopEpisode();
            await _loader.FinishMovieAsync(tempItem);
            _exit.ExitApp();
        });
    }
    protected override bool CanInitializeRemoteControlAfterPlayerInit => true;

}