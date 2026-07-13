using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.SalesOrders;

namespace Eleraki.SalesEngine.Domain.Repositories;

public interface ISalesOrderRepository
{
    Task<SalesOrder?> GetByIdAsync(SalesOrderId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SalesOrder>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SalesOrder>> GetByCustomerIdAsync(CustomerId customerId, CancellationToken cancellationToken = default);
    Task AddAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
    Task UpdateAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
    Task DeleteAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
    Task<bool> ExistsByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
}
