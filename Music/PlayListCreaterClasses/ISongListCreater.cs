namespace MediaHelpers.CoreLibrary.Music.PlayListCreaterClasses;
public interface ISongListCreater
{
    BasicList<ICondition> GetMusicList(BasicPlayListData data); //thanks to dependency injection, for this one, i think the database should be newed up.
}