using Azure.Storage.Blobs;

namespace intitFunctions;

public class BlobHandler : IBlobHander
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobHandler()
    {
        string? connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public Task<Stream> GetBlobAsSteamAsync(string containerName, string blobName,CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        return blobClient.OpenReadAsync();
        //return stream;
    }
}