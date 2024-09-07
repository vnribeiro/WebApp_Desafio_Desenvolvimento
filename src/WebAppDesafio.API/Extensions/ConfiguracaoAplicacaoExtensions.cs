using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAppDesafio.API.Infra.Dados;

namespace WebAppDesafio.API.Extensions;

public static class ConfiguracaoAplicacaoExtensions
{
    private const string DefaultConnection = "DefaultConnection";

    /// <summary>
    /// Configura o versionamento da API para a aplicação.
    /// </summary>
    /// <param name="services">A coleção de serviços para adicionar os serviços de versionamento da API.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection ConfigurarVersionamentoApi(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        }).AddMvc();

        return services;
    }

    /// <summary>
    /// Configura o Swagger para a aplicação, incluindo a geração dinâmica de documentação para todas as versões da API.
    /// </summary>
    /// <param name="service">A coleção de serviços para adicionar os serviços do Swagger.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection ConfigurarSwagger(this IServiceCollection service)
    {

        service.AddSwaggerGen(c =>
        {
            // Retrieve all API versions using reflection
            var apiVersionDescriptionProvider = service
                .BuildServiceProvider()
                .GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                c.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"WebApp_Desafio_API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "API de comunicação WebApp_Desafio_Desenvolvimento.",
                    Contact = new OpenApiContact() { Name = "Netspeed", Email = "contato@netspeed.com.br" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org.licenses/MIT") },
                });
            }

            // Você pode adicionar autenticação JWT aqui
            //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //{
            //    Description = "Enter the JWT token like this: Bearer {your token}\n\n" +
            //                  "Example: Bearer eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1",
            //    Name = "Authorization",
            //    Scheme = "Bearer",
            //    BearerFormat = "JWT",
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.ApiKey
            //});

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
        });

        return service;
    }

    /// <summary>
    /// Configura as configurações de ambiente para a aplicação.
    /// </summary>
    /// <param name="builder">O construtor da aplicação web a ser configurado.</param>
    /// <returns>O construtor da aplicação web atualizado.</returns>
    public static WebApplicationBuilder ConfigurarAmbiente(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (builder.Environment.IsDevelopment())
            builder.Configuration.AddUserSecrets<Program>();

        return builder;
    }

    /// <summary>
    /// Configura a aplicação para usar SQLite como o provedor de banco de dados.
    /// </summary>
    /// <param name="builder">O WebApplicationBuilder a ser configurado.</param>
    public static WebApplicationBuilder ConfigurarSqLite(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(DefaultConnection);

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        return builder;
    }
}