using Asp.Versioning.ApiExplorer;
using WebAppDesafio.API.Extensions;
using WebAppDesafio.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao contêiner.
builder.Services.AddControllers();

// Saiba mais sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura as configurações de ambiente
builder.ConfigurarAmbiente();

// Adiciona o versionamento da API
// e a configuração do Swagger
builder
    .Services
    .ConfigurarVersionamentoApi()
    .ConfigurarSwagger();

// Você pode adicionar provedores de logging adicionais,
// como logging em arquivos ou serviços de logging externos, se desejar.
builder
    .Services
    .AddLogging(configure => configure.AddConsole());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var provider = app.Services
            .GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"WebAppDesafio API {description.GroupName.ToUpperInvariant()}");
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Register middleware for handling exceptions
app.UseMiddleware<MiddlewareTratamentoExcecoes>();

app.MapControllers();

app.Run();