namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IRerunStartTelevisionContext
{
    Task<BasicList<IEpisodeTable>> GetHolidayListAsync(EnumTelevisionHoliday currentHoliday);
}