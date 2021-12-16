namespace intitFunctions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntitFunctions(this IServiceCollection services)
    {

        services
            .AddScoped<IFunctionHandler, FunctionHandler>()
            .AddScoped<IXsltHandler, XsltHandler>()
            .AddScoped<IBlobHander, BlobHandler>()



            ;
        return services;
    }
}