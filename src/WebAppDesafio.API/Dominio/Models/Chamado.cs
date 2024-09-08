using WebAppDesafio.API.Dominio.Models.Interfaces;

namespace WebAppDesafio.API.Dominio.Models;

public class Chamado : Entity, IAggregateRoot
{
    // EF Core
    protected Chamado() {}

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Chamado"/> com os parâmetros especificados.
    /// </summary>
    /// <param name="assunto">O assunto do chamado.</param>
    /// <param name="solicitante">O solicitante do chamado.</param>
    /// <param name="departamento">O departamento responsável pelo chamado.</param>
    /// <param name="dataAbertura">A data de abertura do chamado.</param>
    public Chamado(string assunto, string solicitante, Departamento departamento, DateTime dataAbertura)
    {
        Assunto = assunto;
        Solicitante = solicitante;
        Departamento = departamento;
        DataAbertura = dataAbertura;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Chamado"/> com os parâmetros especificados, incluindo o ID.
    /// </summary>
    /// <param name="id">O ID do chamado.</param>
    /// <param name="assunto">O assunto do chamado.</param>
    /// <param name="solicitante">O solicitante do chamado.</param>
    /// <param name="departamento">O departamento responsável pelo chamado.</param>
    /// <param name="dataAbertura">A data de abertura do chamado.</param>
    public Chamado(Guid id, string assunto, string solicitante, Departamento departamento, DateTime dataAbertura) : base(id)
    {
        Assunto = assunto;
        Solicitante = solicitante;
        Departamento = departamento;
        DataAbertura = dataAbertura;
    }

    /// <summary>
    /// Obtém o assunto do chamado.
    /// </summary>
    public string Assunto { get; private set; } = null!;

    /// <summary>
    /// Obtém o solicitante do chamado.
    /// </summary>
    public string Solicitante { get; private set; } = null!;

    /// <summary>
    /// Obtém o departamento responsável pelo chamado.
    /// </summary>
    public Departamento Departamento { get; private set; } = null!;

    /// <summary>
    /// Obtém a data de abertura do chamado.
    /// </summary>
    public DateTime DataAbertura { get; private set; }

    /// <summary>
    /// Atualiza os dados do chamado com os valores especificados.
    /// </summary>
    /// <param name="assunto">O novo assunto do chamado.</param>
    /// <param name="solicitante">O novo solicitante do chamado.</param>
    /// <param name="departamento">O novo departamento responsável pelo chamado.</param>
    /// <param name="dataAbertura">A nova data de abertura do chamado.</param>
    public void Atualizar(string assunto, string solicitante, Departamento departamento, DateTime dataAbertura)
    {
        Assunto = assunto;
        Solicitante = solicitante;
        Departamento = departamento;
        DataAbertura = dataAbertura;
    }
}
