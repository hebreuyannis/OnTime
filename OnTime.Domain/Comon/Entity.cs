namespace OnTime.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; set; }

    protected Entity() { }
    protected Entity(Guid id) 
    { 
        Id = id;
    }
}
