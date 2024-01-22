namespace MediaHelpers.CoreLibrary.Video.RemoteControls;
public class BasicTelevisionModel : IBasicTelevisionModel
{
    public string ShowName { get; set; } = "";
    public string Progress { get; set; } = "";
    public EnumTelevisionHoliday? Holiday { get; set; }
    public bool NeedsStart { get; set; }
    public bool CanEdit { get; set; }
}