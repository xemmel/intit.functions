namespace intitFunctions;

public interface IFunctionHandler
{
    Stream GetBodyAsStream(HttpRequestData request);
    string? GetHeader(HttpRequestData request, string key);
    string GetVersion();
}
