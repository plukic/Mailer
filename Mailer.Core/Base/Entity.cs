

namespace Mailer.Core.Base
{
    /// <summary>
    /// Shortcut implementation for mostly used primary key type <see cref="int"/>
    /// </summary>
    public class Entity : Entity<int>
    {
    }

    public class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }

        public override string ToString() => $"[{GetType().Name}] {Id}";
    }
}
