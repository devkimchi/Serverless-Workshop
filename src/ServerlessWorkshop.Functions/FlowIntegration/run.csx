#r "Newtonsoft.Json"
using System.Net;
using System.Text;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();
    var serialised = JsonConvert.SerializeObject(data);

    log.Info(serialised);

    var msg = new StringContent(serialised, Encoding.UTF8, "application/json");
    return new HttpResponseMessage(HttpStatusCode.OK) { Content = msg };
}