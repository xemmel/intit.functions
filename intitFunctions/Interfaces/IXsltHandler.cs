namespace intitFunctions;

public interface IXsltHandler
{
    Stream Transform(Stream xslt, Stream xml);
    Task<Stream> TransformAsync(string containerName, string xsltName, Stream xml, CancellationToken cancellationToken = default);
}
