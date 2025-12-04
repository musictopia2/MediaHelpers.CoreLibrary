namespace MediaHelpers.CoreLibrary.Video.TelevisionMiscClasses;
public static class HolidayExtensions
{
    public static EnumTelevisionHoliday WhichHoliday(this DateOnly whichDate)
    {
        if (whichDate.Month == 10 && whichDate.Day >= 29)
        {
            return EnumTelevisionHoliday.Halloween;
        }
        if (whichDate.Month == 11)
        {
            DateOnly tempDate = DateTime.Now.ToDateOnly.WhenIsThanksgivingThisYear;
            int Day = tempDate.Day - 7;
            if (whichDate.Day >= Day && whichDate <= tempDate)
            {
                return EnumTelevisionHoliday.Thanksgiving;
            }
            if (whichDate > tempDate)
            {
                return EnumTelevisionHoliday.Christmas;
            }
            return EnumTelevisionHoliday.None;
        }
        if (whichDate.Month == 12 && whichDate.Day <= 25)
        {
            return EnumTelevisionHoliday.Christmas;
        }
        if (whichDate.Month == 2 && whichDate.Day <= 14 && whichDate.Day >= 8)
        {
            return EnumTelevisionHoliday.ValentinesDay;
        }
        return EnumTelevisionHoliday.None;
    }
}