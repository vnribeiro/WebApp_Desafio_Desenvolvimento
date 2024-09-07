namespace WebAppDesafio.API.Infra.Exceptions;

/// <summary>
/// Exceção lançada quando uma entidade não é encontrada no banco de dados.
/// </summary>
public class EntidadeNaoEncontradaException : Exception
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EntidadeNaoEncontradaException"/> com uma mensagem de erro especificada.
    /// </summary>
    /// <param name="message">A mensagem que descreve o erro.</param>
    public EntidadeNaoEncontradaException(string message) : base(message) { }
}