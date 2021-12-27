namespace intitFunctions;

public interface IFunctionHandler
{
    Stream GetBodyAsStream(HttpRequestData request);
    Task<string> GetBodyAsStringAsync(HttpRequestData request, Encoding? encoding = null);
    Task<HttpResponseData> GetErrorResponseAsync(HttpRequestData request, Exception ex, CancellationToken cancellationToken = default);
    string? GetHeader(HttpRequestData request, string key);
    string GetRequiredHeader(HttpRequestData request, string key);
    string GetVersion();
}
