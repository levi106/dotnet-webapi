var builder = WebApplication.CreateBuilder(args);

var certPath = Environment.GetEnvironmentVariable("SIMPLEAPI_CERT_PATH");
var certPassword = Environment.GetEnvironmentVariable("SIMPLEAPI_CERT_PASSWORD");

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.ListenAnyIP(5003, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2AndHttp3;
        // listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http3;
        if (string.IsNullOrEmpty(certPath))
        {
            listenOptions.UseHttps();
        }
        else
        {
            listenOptions.UseHttps(certPath, certPassword);
        }
    });
});
var app = builder.Build();

app.MapGet("/", () => {
    app.Logger.LogInformation("GET");
    return "Hello World";
});

app.Run();
