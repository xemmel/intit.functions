namespace intitFunctions;

public class FunctionHandler : IFunctionHandler
{
    public FunctionHandler()
    {

    }

    public string GetVersion()
    {
        return "0.4.1";
    }
    public string? GetHeader(HttpRequestData request, string key)
    {
        var result = request
                        .Headers
                        .GetValues(name: key)
                        ?.FirstOrDefault();
        return result;
    }

    public Stream GetBodyAsStream(HttpRequestData request)
    {
        return request.Body;
    }
}
