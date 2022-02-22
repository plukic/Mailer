

namespace Mailer.Core.Base
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
