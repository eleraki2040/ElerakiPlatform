# Performance Considerations

Version: 1.0
Status: Approved
Last Updated: 2026-07-12

---

## PURPOSE

This document outlines performance considerations and guidelines for developing with the Eleraki Framework.

---

## GENERAL GUIDELINES

1. **Measure before optimizing** - Use benchmarks to identify bottlenecks
2. **Prefer async operations** - Use `async/await` for all I/O
3. **Minimize allocations** - Reuse objects where safe
4. **Cache expensive computations** - Use framework caching utilities
5. **Avoid N+1 queries** - Use eager loading or batch operations

---

## DATABASE PERFORMANCE

### Query Optimization

```csharp
// Avoid N+1
var orders = await _repository.ListAsync(
    new ActiveOrdersSpecification(),
    include: o => o.Include(x => x.Items),
    ct);

// Use projection for read-only scenarios
var orderDtos = await _dbContext.Orders
    .Where(o => o.CustomerId == customerId)
    .Select(o => new OrderDto(o.Id, o.Total))
    .ToListAsync(ct);
```

### Indexing Strategy

- Add indexes on frequently queried fields (foreign keys, tenant IDs)
- Use composite indexes for common filter combinations
- Avoid over-indexing - each index slows writes

---

## CACHING

Use caching for read-heavy, rarely-changing data.

```csharp
// In-memory caching
public async Task<Customer?> GetAsync(Guid id, CancellationToken ct = default)
{
    return await _cache.GetOrCreateAsync($"customer:{id}", async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        return await _repository.GetByIdAsync(id, ct);
    });
}

// Invalidation on change
public async Task<Result> UpdateAsync(Customer customer, CancellationToken ct = default)
{
    await _cache.RemoveAsync($"customer:{customer.Id}");
    await _repository.UpdateAsync(customer, ct);
}
```

**Guidelines:**
- Cache read-only or rarely-changing data
- Invalidate cache on writes
- Set appropriate expiration times
- Use distributed cache for multi-instance deployments

---

## MESSAGING

### In-Process Events

Fast but synchronous. Use for same-transaction events.

```csharp
await _unitOfWork.SaveChangesAsync(ct);
// Events dispatched within the same transaction
```

### Out-of-Process Events

Use for long-running or cross-service operations.

```csharp
// Fire-and-forget for non-critical paths
await _eventBus.PublishAsync(event, PublishOptions.FireAndForget);

// Ensure delivery for critical paths
await _eventBus.PublishAsync(event, PublishOptions.EnsureDelivery);
```

---

## MEMORY MANAGEMENT

### Reduce Allocations

```csharp
// Use structs for small, immutable data
public readonly struct Money : IEquatable<Money>
{
    public decimal Amount { get; }
    public string Currency { get; }
}

// Use ArrayPool for large arrays
var array = ArrayPool<byte>.Shared.Rent(bufferSize);
try
{
    // Use array
}
finally
{
    ArrayPool<byte>.Shared.Return(array);
}
```

---

## CONCURRENCY

Use optimistic concurrency for data integrity.

```csharp
public class Order : AggregateRoot<Guid>
{
    [Timestamp]
    public byte[] RowVersion { get; private set; } = Array.Empty<byte>();
}

// Repository handles concurrency conflicts
```

---

## BENCHMARKING

Use BenchmarkDotNet for performance-critical code.

```csharp
[MemoryDiagnoser]
public class CustomerQueryBenchmarks
{
    private readonly AppDbContext _context;

    [Benchmark]
    public async Task<List<CustomerDto>> GetAllCustomers()
        => await _context.Customers
            .Select(c => new CustomerDto(c.Id, c.Name))
            .ToListAsync();
}
```

---

## RELATED DOCUMENTS

- framework/architecture.md
- framework/patterns.md

---

END OF DOCUMENT
