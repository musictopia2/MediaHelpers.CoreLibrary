namespace MediaHelpers.CoreLibrary.Music.PlayListCreaterClasses;
public class PlayListBehaviors<S> where S : IBaseSong
{
    public void FillInYears(BasicPlayListData currentObj, ref BasicList<ICondition> tempList, ref bool hadOne,
        Action<BasicList<ICondition>>? action = null)
    {
        if (currentObj.EarliestYear > 0)
        {
            if (currentObj.EarliestYear > currentObj.LatestYear && currentObj.LatestYear > 0)
            {
                throw new CustomBasicException("The latest year must be greater or equal to the earliest year");
            }
            hadOne = true;
            if (currentObj.LatestYear > 0)
            {
                tempList.AppendRangeCondition(nameof(IBaseSong.YearSong), currentObj.EarliestYear, currentObj.LatestYear);
            }
            else
            {
                tempList.AppendCondition(nameof(IBaseSong.YearSong), cs.Equals, currentObj.EarliestYear);
            }
            action?.Invoke(tempList);
        }
    }
    public BasicList<ICondition> GetStartingPoint(BasicPlayListData currentObj, IAppendTropicalAccess dats, bool songListCounts, bool anyChristmasCounts, out bool hadOne)
    {
        BasicList<ICondition> tempList = new();
        hadOne = false;
        if (SongsChosen.Count > 0)
        {
            tempList.AppendsNot(SongsChosen);
        }
        if (currentObj.SongList.Count > 0)
        {
            tempList.AppendContains(currentObj.SongList);
            if (songListCounts == true)
            {
                hadOne = true;
            }
        }
        if (currentObj.Christmas.HasValue == true)
        {
            if (anyChristmasCounts == true || currentObj.Christmas!.Value == true)
            {
                hadOne = true;
            }
            tempList.AppendCondition(nameof(IBaseSong.Christmas), currentObj.Christmas!.Value);
        }
        if (currentObj.Artist > 0)
        {
            hadOne = true;
            tempList.AppendCondition(nameof(IBaseSong.ArtistID), currentObj.Artist);
        }
        if (currentObj.Romantic == false)
        {
            currentObj.Romantic = null;
        }
        if (currentObj.Romantic == true)
        {
            hadOne = true;
            tempList.AppendCondition(nameof(IBaseSong.Romantic), true);
        }
        if (currentObj.Tropical == true)
        {
            hadOne = true;
            dats.AppendTropical(tempList);
        }
        if (currentObj.WorkOut == true)
        {
            hadOne = true;
            tempList.AppendCondition(nameof(IBaseSong.WorkOut), true);
        }
        if (currentObj.SpecializedFormat != "")
        {
            hadOne = true;
            if (currentObj.UseLikeInSpecializedFormat == true)
            {
                tempList.AppendCondition(nameof(IBaseSong.SpecialFormat), cs.Like, currentObj.SpecializedFormat);
            }
            else
            {
                tempList.AppendCondition(nameof(IBaseSong.SpecialFormat), currentObj.SpecializedFormat);
            }
        }
        if (currentObj.ShowType != "")
        {
            hadOne = true;
            tempList.AppendCondition(nameof(IBaseSong.ShowType), currentObj.ShowType);
        }
        return tempList;
    }
}