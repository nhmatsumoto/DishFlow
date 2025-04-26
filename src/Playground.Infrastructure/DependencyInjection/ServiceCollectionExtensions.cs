using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Domain.Repository;
using Playground.Infrastructure.Context;
using Playground.Infrastructure.Repositories;

namespace Playground.Infrastructure.DependencyInjection;

/// <summary>
/// Contém métodos de extensão para registrar os serviços da camada de infraestrutura
/// na coleção de dependências da aplicação.
/// 
/// Essa classe isola a configuração de acesso a dados e infraestrutura, mantendo
/// a camada de apresentação desacoplada dos detalhes de implementação.
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Registra o <see cref="PlaygroundDbContext"/> com SQL Server, utilizando a string de conexão
    /// "DefaultConnection" definida no <see cref="IConfiguration"/>, e adiciona os repositórios
    /// da camada de infraestrutura no container de injeção de dependência.
    /// 
    /// Lança uma exceção se a string de conexão não for encontrada.
    /// </summary>
    /// <param name="services">A coleção de serviços do ASP.NET Core.</param>
    /// <param name="configuration">A configuração da aplicação, usada para recuperar a string de conexão.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
        }

        services.AddDbContext<PlaygroundDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IIndirectCostRepository, IndirectCostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}