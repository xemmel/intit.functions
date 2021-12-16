namespace intitFunctions;

public interface IFunctionHandler
{
    string? GetHeader(HttpRequestData request, string key);
    string GetVersion();
}
