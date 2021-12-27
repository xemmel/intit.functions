namespace intitFunctions;

public class RegexFunction
{
    private readonly IRegexHandler _regexHandler;
    private readonly IFunctionHandler _functionHandler;
    private readonly ILogger<Transform> _logger;

    public RegexFunction(
                IRegexHandler regexHandler,
                IFunctionHandler functionHandler,
                ILoggerFactory loggerFactory)
    {
        _regexHandler = regexHandler;
        _functionHandler = functionHandler;
        _logger = loggerFactory.CreateLogger<Transform>();
    }
    [Function("Regex")]
    public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req
            )
    {
        try
        {
            var body = await _functionHandler.GetBodyAsStringAsync(request: req);
            var pattern = _functionHandler.GetRequiredHeader(request: req, key: "pattern");
            var result = _regexHandler
                            .GetMatches(input: body, pattern: pattern);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync<IEnumerable<RegexMatchModel>>(instance: result);
            return response;
        }
        catch (Exception ex)
        {

            var errorResponse = await _functionHandler
                                        .GetErrorResponseAsync(request: req,ex: ex);
            return errorResponse;
        }

    }
}