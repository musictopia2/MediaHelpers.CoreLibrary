namespace MediaHelpers.CoreLibrary.Music.DB.Models;
public interface IPlayListDetail
{
    int ID { get; set; }
    string Description { get; set; } //we still needed those 2 because of bindings for views.
    string JsonData { get; set; }
}