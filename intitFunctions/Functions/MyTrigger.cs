using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace intitFunctions;

public class MyTrigger
{
    private readonly ILogger _logger;
    private readonly IFunctionHandler _functionHandler;

    public MyTrigger(
            ILoggerFactory loggerFactory,
            IFunctionHandler functionHandler)
    {
        _logger = loggerFactory.CreateLogger<MyTrigger>();
        _functionHandler = functionHandler;
    }

    [Function("MyTrigger")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var inputStream = _functionHandler.GetBodyAsStream(request: req);
        var processStream = new DummyStream(inputStream);

        var response = req.CreateResponse();
        response.Body = processStream;
        return response;
    }
}

