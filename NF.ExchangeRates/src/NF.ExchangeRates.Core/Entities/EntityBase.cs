namespace NF.ExchangeRates.Core.Entities
{
    public interface IEntity
    {
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }

    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public abstract class EntityBase : EntityBase<int>
    {
    }

}
