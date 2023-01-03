using System.Net;

namespace GiftGenerator.Services;

public class HttpService : IHttpService
{
    public string BaseURL = "https://api.openai.com/v1/completions";
    private HttpClient _client;

    public HttpService(HttpClient client)
    {
        _client = client;
    }

    public string SendMsg(string sQuestion)
    {
        //ServicePointManager.SecurityProtocol =
        //    SecurityProtocolType.Ssl3 |
        //    SecurityProtocolType.Tls12 |
        //    SecurityProtocolType.Tls11 |
        //    SecurityProtocolType.Tls;

        string apiEndpoint = "https://api.openai.com/v1/completions";
        var request = WebRequest.Create(apiEndpoint);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("Authorization", "Bearer " + Utils.Constants.OPENAI_API_KEY);

        int iMaxTokens = 2048;

        double dTemperature = 0.5;
        if (dTemperature < 0d | dTemperature > 1d)
        {
            //MessageBox.Show("Randomness has to be between 0 and 1 with higher values resulting in more random text");
            return "";
        }

        string sUserId = "1";
        string sModel = "text-davinci-003"; // text-davinci-002, text-davinci-003

        string data = "{";
        data += " \"model\":\"" + sModel + "\",";
        data += " \"prompt\": \"" + PadQuotes(sQuestion) + "\",";
        data += " \"max_tokens\": " + iMaxTokens + ",";
        data += " \"user\": \"" + sUserId + "\", ";
        data += " \"temperature\": " + dTemperature + ", ";
        data += " \"frequency_penalty\": 0.0" + ", "; // Number between -2.0 and 2.0  Positive value decrease the model's likelihood to repeat the same line verbatim.
        data += " \"presence_penalty\": 0.0" + ", "; // Number between -2.0 and 2.0. Positive values increase the model's likelihood to talk about new topics.
        data += " \"stop\": [\"#\", \";\"]"; // Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.
        data += "}";

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(data);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var response = request.GetResponse();
        var streamReader = new StreamReader(response.GetResponseStream());
        string sJson = streamReader.ReadToEnd();
        // Return sJson

        //var oJavaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, object> oJson = (Dictionary<string, object>)oJavaScriptSerializer.DeserializeObject(sJson);
        //Object[] oChoices = (Object[])oJson["choices"];
        //Dictionary<string, object> oChoice = (Dictionary<string, object>)oChoices[0];
        //string sResponse = (string)oChoice["text"];

        return sJson;
    }

    private string PadQuotes(string s)
    {
        if (s.IndexOf("\\") != -1)
            s = s.Replace("\\", @"\\");

        if (s.IndexOf("\n\r") != -1)
            s = s.Replace("\n\r", @"\n");

        if (s.IndexOf("\r") != -1)
            s = s.Replace("\r", @"\r");

        if (s.IndexOf("\n") != -1)
            s = s.Replace("\n", @"\n");

        if (s.IndexOf("\t") != -1)
            s = s.Replace("\t", @"\t");

        if (s.IndexOf("\"") != -1)
            return s.Replace("\"", @"""");
        else
            return s;
    }

}
