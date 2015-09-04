namespace BikeMates.Domain.Entities
{
    public interface IEntity<TKey> //TODO: Move to Contracts project
    {
        TKey Id { get; set; }
    }
}
