namespace intitFunctions;

public class TransformFunction
{
    private readonly IXsltHandler _xsltHandler;
    private readonly IFunctionHandler _functionHandler;
    private readonly ILogger<TransformFunction> _logger;

    public TransformFunction(
                IXsltHandler xsltHandler,
                IFunctionHandler functionHandler,
                ILoggerFactory loggerFactory)
    {
        _xsltHandler = xsltHandler;
        _functionHandler = functionHandler;
        _logger = loggerFactory.CreateLogger<TransformFunction>();
    }

    [Function("Transform")]
    public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req
            )
    {
        try
        {
            _logger.LogInformation("Starting");
            var xmlStream = _functionHandler
                                .GetBodyAsStream(request: req);
            string? xsltName = _functionHandler
                                .GetRequiredHeader(request: req, key: "xslt");


            _logger.LogInformation($"xslt: {xsltName}");

            string? containerName = _functionHandler
                                    .GetHeader(request: req, key: "xsltContainer");

            containerName ??= "xslt";


            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/xml; charset=utf-8");
            await _xsltHandler
                    .TransformAsync
                        (containerName: containerName,
                        xsltName: xsltName,
                        xml: xmlStream,
                        result: response.Body);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = await _functionHandler
                            .GetErrorResponseAsync(request: req, ex: ex);
            return errorResponse;

        }
    }

}
