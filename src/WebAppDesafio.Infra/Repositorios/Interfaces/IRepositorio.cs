using WebAppDesafio.Dominio.Models.Interfaces;

namespace WebAppDesafio.Infra.Repositorios.Interfaces;

public interface IRepositorio<T> where T : IAggregateRoot {}