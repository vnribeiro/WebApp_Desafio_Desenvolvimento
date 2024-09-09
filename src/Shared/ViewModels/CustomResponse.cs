namespace Shared.ViewModels;

public class CustomResponse<T>
{
    public bool Sucesso { get; init; }
    public T Dados { get; init; } = default!;
    public string Mensagem { get; init; } = null!;
}

public class CustomResponse
{
    public bool Sucesso { get; init; }
    public string Mensagem { get; init; } = null!;
}
