using System.ComponentModel.DataAnnotations;
using WebAppDesafio.Dominio.Models.Interfaces;

namespace WebAppDesafio.Dominio.Models;

public class Chamado : Entity, IAggregateRoot
{
    // EF Core
    protected Chamado() {}

    public Chamado(string assunto, string solicitante, Departamento departamento, DateTime dataAbertura)
    {
        Assunto = assunto;
        Solicitante = solicitante;
        Departamento = departamento;
        DataAbertura = dataAbertura;
    }

    [Required(ErrorMessage = "O Assunto é obrigatório")]
    public string Assunto { get; private set; }

    [Required(ErrorMessage = "O Solicitante é obrigatório")]
    public string Solicitante { get; private set; }

    public Departamento Departamento { get; private set; }

    public DateTime DataAbertura { get; private set; }
}
