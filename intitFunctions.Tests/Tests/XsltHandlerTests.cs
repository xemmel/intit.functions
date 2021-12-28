namespace intitFunctions.Tests;

public class XsltHandlerTests
{
    private readonly IXsltHandler _xsltHandler;

    public XsltHandlerTests()
    {
        _xsltHandler = DITestFactory.GetService<IXsltHandler>();
    }

    [Theory]
    [InlineData("xslt", "morten.xslt", "<Input />")]
    public async Task TransformAsync(string container, string xsltName, string xml)
    {
        var xmlBytes = Encoding.UTF8.GetBytes(xml);
        MemoryStream xmlStream = new MemoryStream(xmlBytes);
        MemoryStream result = new MemoryStream();

        await _xsltHandler
                            .TransformAsync(
                                    containerName: container,
                                    xsltName: xsltName,
                                    xml: xmlStream,
                                    result: result);
        Assert.NotNull(result);
        result.Position = 0;
        StreamReader streamReader = new StreamReader(result);
        var resultXml = await streamReader.ReadToEndAsync();
        Assert.StartsWith("<", resultXml);
    }

}