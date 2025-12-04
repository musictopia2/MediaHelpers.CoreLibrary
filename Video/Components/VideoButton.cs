namespace MediaHelpers.CoreLibrary.Video.Components;
public class VideoButton : ButtonComponentBase
{
    public override string BackColor => cc1.Blue.ToWebColor;
    public override string TextColor => cc1.LavenderBlush.ToWebColor;
    public override string DisabledColor => cc1.Gray.ToWebColor;
    public override string FontSize => "40px";
}