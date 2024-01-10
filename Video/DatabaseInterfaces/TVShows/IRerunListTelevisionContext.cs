namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IRerunListTelevisionContext : IStartListTelevisionContext
{
    Task<BasicList<IEpisodeTable>> GetHolidayListAsync(EnumTelevisionHoliday currentHoliday);
}