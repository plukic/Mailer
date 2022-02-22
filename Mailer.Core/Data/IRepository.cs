using Ardalis.Specification;

namespace Mailer.Core.Data
{
    /// <inheritdoc/>
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }

    /// <inheritdoc/>
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
    }
}
