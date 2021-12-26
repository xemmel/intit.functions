using System.Xml;
using Microsoft.Extensions.Logging;

namespace intitFunctions;

public class XsltHandler : IXsltHandler
{
    private readonly IBlobHander _blobHander;
    private readonly ILogger<XsltHandler> _logger;

    public XsltHandler(IBlobHander blobHander, ILoggerFactory loggerFactory)
    {
        _blobHander = blobHander;
        _logger = loggerFactory.CreateLogger<XsltHandler>();
    }
    public async Task<Stream> TransformAsync(
                        string containerName, 
                        string xsltName,
                        Stream xml,
                        CancellationToken cancellationToken = default)
    {
        var xsltStream = await _blobHander.GetBlobAsSteamAsync(
                                                containerName: containerName,
                                                blobName: xsltName,
                                                cancellationToken: cancellationToken);
        _logger.LogInformation($"Start transform. Length: {xml.Length}");
        var result = Transform(
                    xslt: xsltStream,
                    xml: xml);
        _logger.LogInformation($"End transform. Length: {result.Length}");
        
        return result;
    }

        public async Task TransformAsync(
                        string containerName, 
                        string xsltName,
                        Stream xml,
                        Stream result,
                        CancellationToken cancellationToken = default)
    {
        var xsltStream = await _blobHander.GetBlobAsSteamAsync(
                                                containerName: containerName,
                                                blobName: xsltName,
                                                cancellationToken: cancellationToken);
        _logger.LogInformation($"Start transform. Length: {xml.Length}");
        Transform(
                    xslt: xsltStream,
                    xml: xml,
                    result: result);
        _logger.LogInformation($"End transform. Length: {result.Length}");
        
        
    }
    public Stream Transform(Stream xslt, Stream xml)
    {
        XslCompiledTransform trans = new XslCompiledTransform();
        var xsltXmlReader = XmlReader.Create(xslt);
        var xmlXmlReader = XmlReader.Create(xml);

        trans.Load(xsltXmlReader);
        MemoryStream stream = new MemoryStream();
        trans.Transform(input: xmlXmlReader,arguments: null,results: stream);
        stream.Position = 0;
        return stream;
    }

      public void Transform(Stream xslt, Stream xml, Stream result)
    {
        XslCompiledTransform trans = new XslCompiledTransform();
        var xsltXmlReader = XmlReader.Create(xslt);
        var xmlXmlReader = XmlReader.Create(xml);

        trans.Load(xsltXmlReader);
        trans.Transform(input: xmlXmlReader,arguments: null,results: result);
        //result.Position = 0;
    }
}