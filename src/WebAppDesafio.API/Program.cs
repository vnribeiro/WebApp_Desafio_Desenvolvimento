using Asp.Versioning.ApiExplorer;
using WebAppDesafio.API.Extensions;
using WebAppDesafio.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os ao cont�iner.
builder.Services.AddControllers();

// Saiba mais sobre a configura��o do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o ambiente
// e o banco de dados
builder
    .ConfigurarAmbiente()
    .ConfigurarSqLite();

// Adiciona o versionamento da API
// e a configura��o do Swagger
builder
    .Services
    .ConfigurarVersionamentoApi()
    .ConfigurarSwagger();

// Adiciona os servi�os de
// reposit�rio e de aplica��o
builder.Services
    .AddRepositorios()
    .AddServicos();

// Voc� pode adicionar provedores de logging adicionais,
// como logging em arquivos ou servi�os de logging externos, se desejar.
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
app.UseMiddleware<TratamentoExcecoesMiddleware>();

app.MapControllers();

app.Run();