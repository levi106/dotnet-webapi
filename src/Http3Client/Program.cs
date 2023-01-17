using System.Net;
using System.Net.Quic;

using var client = new HttpClient()
{
    DefaultRequestVersion = HttpVersion.Version30,
    // DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower
    // DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher
    DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact
};

string requestUrl = args.Length > 0 ? args[0] : "https://localhost:5003";

if (!QuicConnection.IsSupported)
{
    Console.WriteLine($"QUIC is not supported.");
}
else
{
    Console.WriteLine($"QUIC is supported.");
}
Console.WriteLine($"Send request to {requestUrl}");
HttpResponseMessage res = await client.GetAsync(requestUrl);
string body = await res.Content.ReadAsStringAsync();

Console.WriteLine($"status: {res.StatusCode}, version: {res.Version}, " +
    $"body: {body.Substring(0, Math.Min(100, body.Length))}");
