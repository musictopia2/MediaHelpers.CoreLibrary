namespace MediaHelpers.CoreLibrary.BasicInterfaces;
public interface IFullVideoPlayer : IBasicMediaPlayer, ISimpleVideoPlayer
{
    int HowLongBeforeRemovingCursor { get; set; }
    void Init();
    bool IsCursorVisible();
    event Action MediaEnded;
    bool ProcessingBeginning();
    event Action<string, string> Progress;
    event Action<int> SaveResume;
    int TimeElapsed();
    bool PossibleSkips { get; set; }
    TimeSpan TimeLimit { get; set; }
    double SpeedRatio { get; set; } //this is for sure needed.
    void AddScenesToSkip(BasicList<SkipSceneClass> SkipList);
    bool FullScreen { get; set; } //i think this is still needed (?)
}