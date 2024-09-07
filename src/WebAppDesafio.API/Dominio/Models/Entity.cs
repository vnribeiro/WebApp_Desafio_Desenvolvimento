namespace WebAppDesafio.API.Dominio.Models;

public abstract class Entity
{
    private Guid _id;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Entity"/>.
    /// Gera um novo Guid se o Id estiver vazio.
    /// </summary>
    protected Entity()
    {
        _id = Guid.NewGuid();
    }

    /// <summary>
    /// Obtém ou define o identificador exclusivo para a entidade.
    /// </summary>
    public Guid Id
    {
        get => _id;
        set
        {
            if (_id == Guid.Empty)
            {
                _id = value == Guid.Empty ? Guid.NewGuid() : value;
            }
        }
    }

    /// <summary>
    /// Determina se duas entidades são iguais.
    /// </summary>
    public static bool operator ==(Entity? a, Entity? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    /// <summary>
    /// Determina se duas entidades são diferentes.
    /// </summary>
    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Determina se o objeto especificado é igual à entidade atual.
    /// </summary>
    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    /// <summary>
    /// Serve como a função de hash padrão.
    /// </summary>
    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    /// <summary>
    /// Retorna uma string que representa a entidade atual.
    /// </summary>
    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
}
