#r "ServerlessWorkshop.EntityModels.dll"
#r "Newtonsoft.Json"

using System;
using Newtonsoft.Json;
using ServerlessWorkshop.EntityModels;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    using (var context = new SampleDbContext())
    {
        var now = DateTimeOffset.Now;

        foreach (var user in context.Users)
        {
            user.DateUpdated = now;
        }

        context.SaveChanges();
        
        log.Info(JsonConvert.SerializeObject(context.Users));
    }
}