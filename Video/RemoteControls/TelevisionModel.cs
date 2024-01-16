namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public record TelevisionModel(string ShowName, string Progress, EnumTelevisionHoliday? Holiday, bool NeedsStart, int StartAt, bool CanEdit);