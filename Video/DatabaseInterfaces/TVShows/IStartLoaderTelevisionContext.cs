﻿namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IStartLoaderTelevisionContext<E> : IStartBasicTelevisionContext<E>
    where E : class, IEpisodeTable
{
    //anything that is needed on both is here.
    int Seconds { get; set; }
    //Task ReloadAppAsync();
    Task UpdateEpisodeAsync();
    Task InitializeEpisodeAsync();
    Task ModifyHolidayCategoryForEpisodeAsync(EnumTelevisionHoliday holiday); //i think
    Task ForeverSkipEpisodeAsync();
    Task EditEpisodeLaterAsync();
    void PopulateChosenEpisode(int episodeID);
}