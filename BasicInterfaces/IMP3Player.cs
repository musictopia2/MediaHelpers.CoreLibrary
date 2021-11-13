namespace MediaHelpers.CoreLibrary.BasicInterfaces;
public interface IMP3Player : IBasicMediaPlayer
{
    string TimeElapsedLabel();
    string TotalInLabel();
}