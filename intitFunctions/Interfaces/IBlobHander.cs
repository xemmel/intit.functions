namespace intitFunctions;

public interface IBlobHander
{
    Task<Stream> GetBlobAsSteamAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
}
