using System.Xml;

namespace intitFunctions;

public class XsltHandler : IXsltHandler
{
    private readonly IBlobHander _blobHander;

    public XsltHandler(IBlobHander blobHander)
    {
        _blobHander = blobHander;
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
        return Transform(
                    xslt: xsltStream,
                    xml: xml);
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
}