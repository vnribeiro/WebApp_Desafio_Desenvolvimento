using WebAppDesafio.API.Dominio.Models.Interfaces;

namespace WebAppDesafio.API.Infra.Repositorios.Interfaces;

public interface IRepositorio<T> where T : IAggregateRoot {}