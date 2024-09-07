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

    public Chamado(Guid id, string assunto, string solicitante, Departamento departamento, DateTime dataAbertura)
    {
        base.Id = id;
        Assunto = assunto;
        Solicitante = solicitante;
        Departamento = departamento;
        DataAbertura = dataAbertura;
    }

    public string Assunto { get; private set; }

    public string Solicitante { get; private set; }

    public Departamento Departamento { get; private set; }

    public DateTime DataAbertura { get; private set; }
}
