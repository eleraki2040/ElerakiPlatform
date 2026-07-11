using Eleraki.IdentityEngine.Domain;
using Eleraki.IdentityEngine.Domain.Repositories;

namespace Eleraki.IdentityEngine.Application.Tests.Fakes;

public class FakeUserRepository : IUserRepository
{
    private readonly Dictionary<UserId, User> _users = new();

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        _users[user.Id] = user;
        return Task.CompletedTask;
    }

    public Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_users.GetValueOrDefault(id));
    }

    public Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<User>>(_users.Values.ToList().AsReadOnly());
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _users[user.Id] = user;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _users.Remove(user.Id);
        return Task.CompletedTask;
    }
}
