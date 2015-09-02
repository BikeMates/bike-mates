namespace BikeMates.Domain.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
