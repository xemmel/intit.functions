using System.Threading.Tasks;

namespace intitFunctions.Tests;

public class BlobHandlerTests
{
    private readonly IBlobHander _blobHandler;

    public BlobHandlerTests()
    {
        _blobHandler = DITestFactory.GetService<IBlobHander>();
    }

    [Theory]
    [InlineData("xslt", "morten.xslt", "<")]

    public async Task GetBlobAsSteamAsync(
          string container,
          string blobName,
          string? startsWith = null)
    {
        
        var blobStream = await _blobHandler
                                .GetBlobAsSteamAsync(
                                        containerName: container, 
                                        blobName: blobName);

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

}