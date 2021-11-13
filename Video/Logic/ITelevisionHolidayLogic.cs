namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface ITelevisionHolidayLogic
{
    Task<BasicList<IEpisodeTable>> GetHolidayEpisodeListAsync(EnumTelevisionHoliday holiday);
}