namespace MediaHelpers.CoreLibrary.Music.DB.Helpers;
public static class SongTableExtension
{
    public static EnumSecondaryFormat GetSecondaryFormat(this string secondaryString)
    {
        return secondaryString switch
        {
            "todo" or "ToDo" => EnumSecondaryFormat.ToDo,
            "" or "None" => EnumSecondaryFormat.None,
            "Acapella" => EnumSecondaryFormat.Acapella,
            "Country" => EnumSecondaryFormat.Country,
            "Jazz" => EnumSecondaryFormat.Jazz,
            "Rap" => EnumSecondaryFormat.Rap,
            "Retro" => EnumSecondaryFormat.Retro,
            "Rock" => EnumSecondaryFormat.Rock,
            "Rockabilly" => EnumSecondaryFormat.Rockabilly,
            "Surf" => EnumSecondaryFormat.Surf,
            "Swing" => EnumSecondaryFormat.Swing,
            "Talk" => EnumSecondaryFormat.Talk,
            "Techo" => EnumSecondaryFormat.Techo,
            "Tropical" => EnumSecondaryFormat.Tropical,
            "Unique" => EnumSecondaryFormat.Unique,
            _ => EnumSecondaryFormat.None,
        };
    }
    public static EnumShowTypeCategory GetShowEnumCategory(this string stringShow)
    {
        return stringShow switch
        {
            "" => EnumShowTypeCategory.None,
            "Broadway" => EnumShowTypeCategory.Broadway,
            "Disney" => EnumShowTypeCategory.Disney,
            "Games" => EnumShowTypeCategory.Games,
            "Movie" => EnumShowTypeCategory.Movie,
            "TV" => EnumShowTypeCategory.TV,
            "New Wave" or "None" => EnumShowTypeCategory.None,
            _ => EnumShowTypeCategory.None,
        };
    }
    public static EnumSpecialFormatCategory GetSpecialEnumCategory(this string stringSpecial)
    {
        return stringSpecial switch
        {
            "new wave" or "New Wave" => EnumSpecialFormatCategory.NewWave,
            "Bluegrass" => EnumSpecialFormatCategory.Bluegrass,
            "Classical" => EnumSpecialFormatCategory.Classical,
            "Country/Contemporary" => EnumSpecialFormatCategory.CountryContemporary,
            "Country/Pop" => EnumSpecialFormatCategory.CountryPop,
            "Country/Traditional" => EnumSpecialFormatCategory.CountryTraditional,
            "Dance" => EnumSpecialFormatCategory.Dance,
            "Dance Pop" => EnumSpecialFormatCategory.DancePop,
            "Disco" => EnumSpecialFormatCategory.Disco,
            "Folk" => EnumSpecialFormatCategory.Folk,
            "Gospel" => EnumSpecialFormatCategory.Gospel,
            "Jazz" => EnumSpecialFormatCategory.Jazz,
            "Mexican Pop" => EnumSpecialFormatCategory.MexicanPop,
            "" or "None" => EnumSpecialFormatCategory.None,
            "Oldies" => EnumSpecialFormatCategory.Oldies,
            "Rap" => EnumSpecialFormatCategory.Rap,
            "Reggae" => EnumSpecialFormatCategory.Reggae,
            "Remix" => EnumSpecialFormatCategory.Remix,
            "Salsa" => EnumSpecialFormatCategory.Salsa,
            "Spanish" => EnumSpecialFormatCategory.Spanish,
            "Standards" => EnumSpecialFormatCategory.Standards,
            "Swing/Big Band" => EnumSpecialFormatCategory.SwingBigBand,
            "Swing/Revival" => EnumSpecialFormatCategory.SwingRevival,
            "Swing/Western" => EnumSpecialFormatCategory.SwingWestern,
            "Urban" => EnumSpecialFormatCategory.Urban,
            _ => EnumSpecialFormatCategory.None,
        };
    }
    public static string GetSpecialString(this EnumSpecialFormatCategory category)
    {
        return category switch
        {
            EnumSpecialFormatCategory.None => "None",
            EnumSpecialFormatCategory.CountryContemporary => "Country/Contemporary",
            EnumSpecialFormatCategory.SwingWestern => "Swing/Western",
            EnumSpecialFormatCategory.SwingBigBand => "Swing/Big Band",
            EnumSpecialFormatCategory.Spanish => "Spanish",
            EnumSpecialFormatCategory.Salsa => "Salsa",
            EnumSpecialFormatCategory.Standards => "Standards",
            EnumSpecialFormatCategory.Oldies => "Oldies",
            EnumSpecialFormatCategory.Classical => "Classical",
            EnumSpecialFormatCategory.Disco => "Disco",
            EnumSpecialFormatCategory.Dance => "Dance",
            EnumSpecialFormatCategory.Rap => "Rap",
            EnumSpecialFormatCategory.Gospel => "Gospel",
            EnumSpecialFormatCategory.Jazz => "Jazz",
            EnumSpecialFormatCategory.NewWave => "new wave",
            EnumSpecialFormatCategory.Reggae => "Reggae",
            EnumSpecialFormatCategory.DancePop => "Dance Pop",
            EnumSpecialFormatCategory.Urban => "Urban",
            EnumSpecialFormatCategory.SwingRevival => "Swing/Revival",
            EnumSpecialFormatCategory.Bluegrass => "Bluegrass",
            EnumSpecialFormatCategory.CountryPop => "Country/Pop",
            EnumSpecialFormatCategory.CountryTraditional => "Country/Traditional",
            EnumSpecialFormatCategory.Folk => "Folk",
            EnumSpecialFormatCategory.MexicanPop => "Mexican Pop",
            EnumSpecialFormatCategory.Remix => "Remix",
            _ => throw new CustomBasicException("Category Not Supported"),
        };
    }
}