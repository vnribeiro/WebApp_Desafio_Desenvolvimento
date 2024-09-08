using WebAppDesafio.API.Domain.Models.Interfaces;

namespace WebAppDesafio.API.Domain.Models;

public class Departamento : Entity, IAggregateRoot
{
    // EF Core
    protected Departamento() {}

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Departamento"/> com a descrição especificada.
    /// </summary>
    /// <param name="descricao">A descrição do departamento.</param>
    public Departamento(string descricao)
    {
        Descricao = descricao;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Departamento"/> com o ID e a descrição especificados.
    /// </summary>
    /// <param name="id">O ID do departamento.</param>
    /// <param name="descricao">A descrição do departamento.</param>
    public Departamento(Guid id, string descricao) : base(id)
    {
        Descricao = descricao;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Departamento"/> com o ID especificado.
    /// </summary>
    /// <param name="id">O ID do departamento.</param>
    public Departamento(Guid id) : base(id) {}

    /// <summary>
    /// Obtém a descrição do departamento.
    /// </summary>
    public string Descricao { get; private set; } = null!;

    /// <summary>
    /// Atualiza a descrição do departamento com o valor especificado.
    /// </summary>
    /// <param name="descricao">A nova descrição do departamento.</param>
    public void Atualizar(string descricao)
    {
        Descricao = descricao;
    }
}
