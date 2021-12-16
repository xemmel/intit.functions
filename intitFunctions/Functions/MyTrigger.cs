using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace intitFunctions
{
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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var xslt = _functionHandler
                            .GetHeader(request: req,key: "xslt");
            _logger.LogInformation($"xslt: {xslt}");
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions!\tVersion: {_functionHandler.GetVersion()}");

            return response;
        }
    }
}
