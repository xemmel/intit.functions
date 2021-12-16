namespace intitFunctions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntitFunctions(this IServiceCollection services)
    {

        services
            .AddScoped<IFunctionHandler, FunctionHandler>()

            ;
        return services;
    }
}