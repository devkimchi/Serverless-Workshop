#r "Newtonsoft.Json"

using System.Net;
using System.Text;

using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    dynamic body = await req.Content.ReadAsAsync<object>();

    var serialised = JsonConvert.SerializeObject(body);
    // log.Info(serialised);

    var message = $"Push made by {body.head_commit.author.name} with commitId, {body.head_commit.id}";
    var baseUri = new Uri("https://hooks.slack.com");
    var endpoint = "services/[API_KEY_GOES_HERE]";

    using (var client = new HttpClient() { BaseAddress = baseUri })
    {
        var rqst = new { text = message };
        var content = new StringContent(JsonConvert.SerializeObject(rqst));
        var messgaeResponse = await client.PostAsync(endpoint, content).ConfigureAwait(false);

        serialised = await messgaeResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    var msg = new StringContent(serialised, Encoding.UTF8, "application/json");
    return new HttpResponseMessage(HttpStatusCode.OK) { Content = msg };
}