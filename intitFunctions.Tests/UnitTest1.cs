using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace intitFunctions.Tests;

public class UnitTest1
{
    private readonly ServiceProvider _provider;

    public UnitTest1()
    {
        Environment.SetEnvironmentVariable(
                    "AzureWebJobsStorage", "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=stintitfunctions;AccountKey=trbDOMbILx5hROXFtTFGOJTWwALw1w85qR3DI3AohUvDOCzkaHMCj1akNBgkTyi3pxSxmu9Mg4qMX+0oKq70cw==");
        var services = new ServiceCollection();
        services.AddIntitFunctions();
        _provider = services.BuildServiceProvider();

    }
    [Fact]
    public void Test1()
    {
        int i = 10;
    }

    [Theory]
    [InlineData("xslt", "morten.xslt", "<")]
    //[InlineData("xslt", "morten2.xslt", null)]

    public async Task BlobHandler_GetBlobAsSteamAsync(
            string container,
            string blobName,
            string? startsWith = null)
    {
        var blobHandler = _provider.GetRequiredService<IBlobHander>();
        var blobStream = await blobHandler
                                .GetBlobAsSteamAsync(containerName: container, blobName: blobName);

        if (startsWith == null)
        {
            Assert.Null(blobStream);
        }
        else
        {
            Assert.NotNull(blobName);
            StreamReader streamReader = new StreamReader(blobStream);
            string result = await streamReader.ReadToEndAsync();
            Assert.StartsWith(startsWith, result);
        }
    }

    [Theory]
    [InlineData("xslt", "morten.xslt","<Input />")]
    public async Task XsltHandler_TransformAsync(string container, string xsltName, string xml)
    {
        var xmlBytes = Encoding.UTF8.GetBytes(xml);
        MemoryStream xmlStream = new MemoryStream(xmlBytes);

        var xsltHandler = _provider.GetRequiredService<IXsltHandler>();
        var stream = await xsltHandler
                            .TransformAsync(
                                    containerName: container,
                                    xsltName: xsltName,
                                    xml: xmlStream);
        Assert.NotNull(stream);
        StreamReader streamReader = new StreamReader(stream);
        var result = await streamReader.ReadToEndAsync();
        Assert.StartsWith("<",result);
    }
}