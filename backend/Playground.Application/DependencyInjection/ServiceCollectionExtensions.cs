using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Interfaces;
using Playground.Application.UseCases;

namespace Playground.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateRestaurantUseCase, CreateRestaurantUseCase>();
        services.AddScoped<IUpdateRestaurantUseCase, UpdateRestaurantUseCase>();
        services.AddScoped<IGetRestaurantUseCase, GetRestaurantUseCase>();
        services.AddScoped<ICreateRecipeUseCase, CreateRecipeUseCase>();
        services.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
        services.AddScoped<IGetRecipeUseCase, GetRecipeUseCase>();
        services.AddScoped<ICalculateRecipeCostUseCase, CalculateRecipeCostUseCase>();
        services.AddScoped<ICreateIngredientUseCase, CreateIngredientUseCase>();
        services.AddScoped<IUpdateIngredientUseCase, UpdateIngredientUseCase>();
        services.AddScoped<IGetIngredientUseCase, GetIngredientUseCase>();
        services.AddScoped<ICreateIndirectCostUseCase, CreateIndirectCostUseCase>();
        services.AddScoped<IUpdateIndirectCostUseCase, UpdateIndirectCostUseCase>();
        services.AddScoped<IGetIndirectCostUseCase, GetIndirectCostUseCase>();
        services.AddScoped<IRegisterRestaurantOwnerUseCase, RegisterRestaurantOwnerUseCase>();  

        return services;
    }
}
