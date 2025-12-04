namespace MediaHelpers.CoreLibrary.Video.Helpers;
public static class ServiceExtensions
{
    extension (IServiceCollection services)
    {
        public IServiceCollection RegisterMockTelevisionRerunRemoteControls()
        {
            services.AddSingleton<IRerunTelevisionRemoteControlHostService, MockRerunTelevisionRemoteControlHostService>();
            return services;
        }
        public IServiceCollection RegisterMockMovieRerunRemoteControls()
        {
            services.AddSingleton<IRerunMoviesRemoveControlHostService<BasicMoviesModel>, MockRerunMoviesRemoteControlHostService>();
            return services;
        }
        public IServiceCollection RegisterMovieContainer<M>()
            where M : class, IMainMovieTable
        {
            services.AddSingleton<MovieContainerClass<M>>();
            return services;
        }
        public IServiceCollection RegisterTelevisionContainer<E>()
            where E : class, IEpisodeTable
        {
            //this is all the stuff that does not require the video players or displays.
            services.AddSingleton<TelevisionContainerClass<E>>();
            return services;
        }
        public IServiceCollection RegisterCoreRerunListMovieServices<L, M>()
            where M : class, IMainMovieTable
            where L : class, IMovieVideoLoader<M>
        {
            services.RegisterMovieContainer<M>()
                .AddSingleton<MovieListViewModel<M>>()
                .AddSingleton<IMovieListLogic<M>, MovieRerunsListLogic<M>>()
                .AddSingleton<IMovieVideoLoader<M>, L>();
            return services;
        }
        public IServiceCollection RegisterCoreRerunListTelevisionServices<L, E>()
            where E : class, IEpisodeTable
            where L : class, ITelevisionVideoLoader<E>
        {
            services.RegisterTelevisionContainer<E>()
                .AddSingleton<ITelevisionShellViewModel<E>, TelevisionRerunsShellViewModel<E>>()
                .AddSingleton<ITelevisionShellLogic<E>, TelevisionCompleteShellLogic<E>>()
                .RegisterCoreHolidayTelevisionServices<E>()
                .AddSingleton<TelevisionListViewModel<E>>()
                .RegisterNextReRunTelevisionLogic<E>()
                .AddSingleton<ITelevisionListLogic, TelevisionListRerunLogic<E>>()
                .AddSingleton<ITelevisionVideoLoader<E>, L>();
            return services;
        }
        public IServiceCollection RegisterCoreLocalRerunLoaderTelevisionServices<E>()
            where E : class, IEpisodeTable
        {
            services.AddSingleton<RerunLocalTelevisionLoaderViewModel<E>>()
                .RegisterTelevisionContainer<E>()
                .AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<RerunLocalTelevisionLoaderViewModel<E>>())
                .AddSingleton<ITelevisionLoaderViewModel>(pp => pp.GetRequiredService<RerunLocalTelevisionLoaderViewModel<E>>())
                .AddSingleton<IStartLoadingViewModel>(pp => pp.GetRequiredService<RerunLocalTelevisionLoaderViewModel<E>>())
                .RegisterNextReRunTelevisionLogic<E>()
                .RegisterRerunTelevisionLoaderLogic<E>()
                .RegisterCoreHolidayTelevisionServices<E>();
            return services;
        }
        public IServiceCollection RegisterCoreLocalRerunLoaderMovieServices<M>()
            where M : class, IMainMovieTable
        {
            services.RegisterMovieContainer<M>()
                .AddSingleton<RerunLocalMovieLoaderViewModel<M>>()
                .AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<RerunLocalMovieLoaderViewModel<M>>())
                .AddSingleton<IMovieLoaderViewModel>(pp => pp.GetRequiredService<RerunLocalMovieLoaderViewModel<M>>())
                .AddSingleton<IStartLoadingViewModel>(pp => pp.GetRequiredService<RerunLocalMovieLoaderViewModel<M>>())
                .AddSingleton<MovieRerunsLoaderLogic<M>>()
                .AddSingleton<IRerunMovieLoaderLogic<M>>(pp => pp.GetRequiredService<MovieRerunsLoaderLogic<M>>());
            return services;
        }
        public IServiceCollection RegisterRerunTelevisionLoaderLogic<E>()
            where E : class, IEpisodeTable
        {
            services.AddSingleton<TelevisionRerunsLoaderLogic<E>>()
                .AddSingleton<IRerunTelevisionLoaderLogic<E>>(pp => pp.GetRequiredService<TelevisionRerunsLoaderLogic<E>>());
            return services;
        }
        //even these needs to be public so youtube can access them.


        public IServiceCollection RegisterNextReRunTelevisionLogic<E>()
            where E : class, IEpisodeTable
        {
            services.AddSingleton<INextEpisodeLogic<E>, TelevisionBasicRerunNextEpisodeLogic<E>>();
            return services;
        }
        public IServiceCollection RegisterCoreHolidayTelevisionServices<E>()
            where E : class, IEpisodeTable
        {
            services.AddSingleton<TelevisionHolidayLogic<E>>()
                .AddSingleton<ITelevisionHolidayLogic<E>>(pp => pp.GetRequiredService<TelevisionHolidayLogic<E>>())
                .AddSingleton<TelevisionHolidayViewModel<E>>();
            return services;
        }
    }   
}