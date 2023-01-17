var builder = WebApplication.CreateBuilder(args);

var certPath = Environment.GetEnvironmentVariable("SIMPLEAPI_CERT_PATH");
var certPassword = Environment.GetEnvironmentVariable("SIMPLEAPI_CERT_PASSWORD");

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.ListenAnyIP(5003, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2AndHttp3;
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

app.MapGet("/", () => "Hello World");

app.Run();
