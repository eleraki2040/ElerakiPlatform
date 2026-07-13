using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.SalesEngine.Infrastructure.Repositories;

public class SalesOrderRepository : ISalesOrderRepository
{
    private readonly SalesDbContext _context;

    public SalesOrderRepository(SalesDbContext context)
    {
        _context = context;
    }

    public async Task<SalesOrder?> GetByIdAsync(SalesOrderId id, CancellationToken cancellationToken = default)
    {
        return await _context.SalesOrders
            .Include(so => so.Lines)
            .FirstOrDefaultAsync(so => so.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SalesOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SalesOrders
            .Include(so => so.Lines)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SalesOrder>> GetByCustomerIdAsync(CustomerId customerId, CancellationToken cancellationToken = default)
    {
        return await _context.SalesOrders
            .Include(so => so.Lines)
            .Where(so => so.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
    {
        await _context.SalesOrders.AddAsync(salesOrder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
    {
        _context.SalesOrders.Update(salesOrder);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
    {
        _context.SalesOrders.Remove(salesOrder);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
    {
        return await _context.SalesOrders
            .AnyAsync(so => so.OrderNumber == orderNumber, cancellationToken);
    }
}
