using GiftGenerator.Services.Interfaces;
using Newtonsoft.Json;

namespace GiftGenerator.Services;

public class PredictionService : IPredictionService
{
    public PredictionService()
    {

    }

    public async Task<Dictionary<string, List<string>>> GetFileData(string fileName)
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream);

        var contents = reader.ReadToEnd();

        return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(contents);
    }

    public async Task<List<string>> GetRecomandationBasedOfInterests(string fileName, List<string> interests)
    {
        Dictionary<string, List<string>> respons = await GetFileData(fileName);
        List<string> result = new();
        Random rng = new();

        if (interests.Count == 1)
        {
            return respons[interests[0]].OrderBy(a => rng.Next()).ToList();
        }

        if (interests.Count > 1)
        {
            int division = 1;
            if (interests.Count <= 10)
            {
                division = (int)(10 / interests.Count) + 1;
            }

            foreach (var interest in interests)
            {
                result.AddRange(respons[interest].OrderBy(a => rng.Next()).ToList().Take(division));
            }

            return result.OrderBy(a => rng.Next()).ToList();
        }
        else
        {
            return respons["SCNLFGHDHIAHIO"].OrderBy(a => rng.Next()).ToList();
        }
    }
}
