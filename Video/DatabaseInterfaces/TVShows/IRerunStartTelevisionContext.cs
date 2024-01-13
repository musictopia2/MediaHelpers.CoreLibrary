namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IRerunStartTelevisionContext<E>
    where E : class, IEpisodeTable
{
    Task<BasicList<E>> GetHolidayListAsync(EnumTelevisionHoliday currentHoliday);
}