namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public interface IBasicTelevisionModel
{
    bool CanEdit { get; set; }
    EnumTelevisionHoliday? Holiday { get; set; }
    bool NeedsStart { get; set; }
    string Progress { get; set; }
    string ShowName { get; set; }
}