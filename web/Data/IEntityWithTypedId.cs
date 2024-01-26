namespace web.Data;

public interface IEntityWithTypedId<T>
{
    T ID { get; set; }
}