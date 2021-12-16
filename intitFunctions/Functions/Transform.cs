using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace intitFunctions
{
    public class Transform
    {
        private readonly IXsltHandler _xsltHandler;
        private readonly IFunctionHandler _functionHandler;
        private readonly ILogger<Transform> _logger;

        public Transform(
                    IXsltHandler xsltHandler, 
                    IFunctionHandler functionHandler,
                    ILoggerFactory loggerFactory)
        {
            _xsltHandler = xsltHandler;
            _functionHandler = functionHandler;
            _logger = loggerFactory.CreateLogger<Transform>();
        }

        [Function("Transform")]
        public async Task<HttpResponseData> Run(
                [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req
                )
        {
            _logger.LogInformation("Starting");
            var xmlStream =  _functionHandler
                                .GetBodyAsStream(request: req);
            string? xsltName = _functionHandler
                                .GetHeader(request: req,key: "xslt");
            
            _logger.LogInformation($"xslt: {xsltName}");

            string? containerName = _functionHandler
                                    .GetHeader(request: req, key: "xsltContainer");
            containerName ??= "xslt";
            
            var resultStream = await _xsltHandler
                                        .TransformAsync(
                                            containerName: containerName,
                                            xsltName: xsltName,
                                            xml: xmlStream);
            //StreamReader streamReader = new StreamReader(resultStream);
           // var result = streamReader.ReadToEnd();                               
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/xml; charset=utf-8");
            response.Body = resultStream;
            return response;
        }

    }
}