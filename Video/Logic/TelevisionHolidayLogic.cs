namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionHolidayLogic<E>(IRerunStartTelevisionContext<E> dats) : ITelevisionHolidayLogic<E>
    where E : class, IEpisodeTable
{
    async Task<BasicList<E>> ITelevisionHolidayLogic<E>.GetHolidayEpisodeListAsync(EnumTelevisionHoliday currentHoliday)
    {
        return await dats.GetHolidayListAsync(currentHoliday);
    }
}