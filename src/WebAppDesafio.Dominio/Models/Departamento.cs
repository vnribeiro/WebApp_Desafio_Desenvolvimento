using System.ComponentModel.DataAnnotations;
using WebAppDesafio.Dominio.Models.Interfaces;

namespace WebAppDesafio.Dominio.Models;

public class Departamento : Entity, IAggregateRoot
{
    // EF Core
    protected Departamento() {}

    public Departamento(string descricao)
    {
        Descricao = descricao;
    }

    [Required(ErrorMessage = "A Descricao é obrigatória")]
    public string Descricao { get; private set; }
}
