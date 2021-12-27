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

    public async Task<HttpResponseData> GetErrorResponseAsync(
        HttpRequestData request, Exception ex,
        CancellationToken cancellationToken = default)
    {
        var response = request.CreateResponse(HttpStatusCode.InternalServerError);
        var errorResponse = new ErrorResponseModel
        {
            ErrorMessage = ex.Message,
            InnerErrorMessage = ex.InnerException?.Message
        };
        await response.WriteAsJsonAsync<ErrorResponseModel>(
                    instance: errorResponse,
                    cancellationToken: cancellationToken);
        return response;
    }
    public string GetRequiredHeader(HttpRequestData request, string key)
    {
        string? result = GetHeader(request: request,key:key);
        if (result == null)
        {
            throw new ApplicationException($"Header: {key} required");
        }
        return result;
    }
    public string? GetHeader(HttpRequestData request, string key)
    {
        try
        {
                     var result = request
                        .Headers
                        .GetValues(name: key)
                        ?.FirstOrDefault();
        return result;
        }
        catch (System.Exception)
        {
            
            return null;
        }

    }

    public Stream GetBodyAsStream(HttpRequestData request)
    {
        return request.Body;
    }
    public Task<string> GetBodyAsStringAsync(HttpRequestData request, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return new StreamReader(stream: request.Body,encoding: encoding)
                            .ReadToEndAsync();
    }
}
