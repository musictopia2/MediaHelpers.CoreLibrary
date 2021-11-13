namespace MediaHelpers.CoreLibrary.BasicInterfaces;
public interface IVideoPlayer : IBasicMediaPlayer
{
    int HowLongBeforeRemovingCursor { get; set; }
    void Init();
    bool IsCursorVisible();
    void OtherMouseMove();
    void ShowCursor();
    void HideCursor();
    event Action MediaEnded;
    bool ProcessingBeginning();
    event Action<string, string> Progress;
    event Action<int> SaveResume;
    int TimeElapsed();
    bool PossibleSkips { get; set; }
    TimeSpan TimeLimit { get; set; }
    double SpeedRatio { get; set; }
    void AddScenesToSkip(BasicList<SkipSceneClass> SkipList);
    bool FullScreen { get; set; }
}