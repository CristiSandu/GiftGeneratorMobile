namespace GiftGenerator.Services.Interfaces
{
    public interface IPredictionService
    {
        Task<Dictionary<string, List<string>>> GetFileData(string fileName);
        Task<List<string>> GetRecomandationBasedOfInterests(string fileName, List<string> interests);
    }
}