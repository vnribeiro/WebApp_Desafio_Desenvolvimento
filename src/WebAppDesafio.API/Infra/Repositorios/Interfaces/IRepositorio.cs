using WebAppDesafio.API.Domain.Models.Interfaces;

namespace WebAppDesafio.API.Infra.Repositorios.Interfaces;

public interface IRepositorio<T> where T : IAggregateRoot {}