namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface ITelevisionHolidayLogic<E>
    where E : class, IEpisodeTable
{
    Task<BasicList<E>> GetHolidayEpisodeListAsync(EnumTelevisionHoliday holiday);
}