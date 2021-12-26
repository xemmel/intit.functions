namespace intitFunctions.Tests;

public static class DITestFactory
{
    public static T GetService<T>() where T : class
    {
                Environment.SetEnvironmentVariable(
                    "AzureWebJobsStorage", "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=stintitfunctions;AccountKey=trbDOMbILx5hROXFtTFGOJTWwALw1w85qR3DI3AohUvDOCzkaHMCj1akNBgkTyi3pxSxmu9Mg4qMX+0oKq70cw==");

        var services = new ServiceCollection();
        services.AddIntitFunctions();
        services.AddLogging();
        return services.BuildServiceProvider().GetRequiredService<T>();
    }

}