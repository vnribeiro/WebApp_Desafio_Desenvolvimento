using WebAppDesafio.API.Dominio.Models.Interfaces;

namespace WebAppDesafio.API.Dominio.Models;

public class Departamento : Entity, IAggregateRoot
{
    // EF Core
    protected Departamento() {}

    public Departamento(string descricao)
    {
        Descricao = descricao;
    }

    public Departamento(Guid id, string descricao)
    {
        base.Id = id;
        Descricao = descricao;
    }

    public string Descricao { get; private set; } = null!;
}
