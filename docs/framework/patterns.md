# Design Patterns

Version: 1.0
Status: Approved
Last Updated: 2026-07-12

---

## PURPOSE

This document describes the design patterns used throughout the Eleraki Framework.

---

## CQRS (Command Query Responsibility Segregation)

Separates read operations from write operations.

**Commands** change state. They return `Result` or `Result<T>`.

```csharp
public record CreateOrderCommand(Guid CustomerId, List<OrderItem> Items)
    : ICommand<Result<Guid>>;

public class CreateOrderHandler : CommandHandler<CreateOrderCommand, Result<Guid>>
{
    public override async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken ct)
    {
        // Validate, enforce invariants, create aggregate
    }
}
```

**Queries** return data without side effects.

```csharp
public record GetOrderQuery(Guid OrderId) : IQuery<Result<OrderDto>>;

public class GetOrderHandler : QueryHandler<GetOrderQuery, Result<OrderDto>>
{
    public override async Task<Result<OrderDto>> Handle(GetOrderQuery query, CancellationToken ct)
    {
        // Return read model
    }
}
```

**When to Use:** All write operations. Read operations may use simpler repository patterns.

---

## REPOSITORY PATTERN

Abstracts data access behind interfaces.

```csharp
public interface IRepository<T> where T : Entity, IAggregateRoot
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task DeleteAsync(T entity, CancellationToken ct = default);
}
```

**When to Use:** Aggregates. Do not use for simple lookups - use specialized queries.

---

## DOMAIN EVENTS

Captures significant occurrences within a domain.

```csharp
public record OrderCreatedEvent(Guid OrderId, Guid CustomerId, DateTime OccurredOn)
    : IDomainEvent;

// Publishing an event
var order = Order.Create(customerId, items);
await _eventBus.PublishAsync(new OrderCreatedEvent(order.Id, customerId, DateTime.UtcNow));

// Subscribing to an event
public class OrderCreatedHandler : IDomainEventHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent @event, CancellationToken ct)
    {
        // React to event
    }
}
```

**When to Use:** Decoupling side effects from aggregate state changes. Never use for inter-aggregate consistency (use eventual consistency instead).

---

## SPECIFICATION PATTERN

Encapsulates business rules that can be combined and reused.

```csharp
public class ActiveCustomerSpecification : Specification<Customer>
{
    public ActiveCustomerSpecification() : base(c => c.IsActive)
    {
    }
}

// Usage
var activeCustomers = await _repository.ListAsync(new ActiveCustomerSpecification(), ct);
```

**When to Use:** Complex filtering logic that needs to be reused across queries and commands.

---

## UNIT OF WORK

Coordinates a set of operations as a single transaction.

```csharp
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task ExecuteInTransactionAsync(Func<Task> operation, CancellationToken ct = default);
}
```

**When to Use:** All write operations that touch multiple aggregates or repositories.

---

## GUARD CLAUSES

Protects against invalid arguments at the boundary.

```csharp
public static Result<T> EnsureNotNull<T>(T? value, string paramName)
    where T : class
{
    if (value is null)
        return Result.Failure<T>($"{paramName} cannot be null.");

    return Result.Success(value);
}

// Usage
var result = Guard.EnsureNotNull(order, nameof(order));
if (result.IsFailure) return result;
```

**When to Use:** Entry points of public APIs, constructor parameters, command handlers.

---

## RESULT TYPE

Encapsulates operation outcomes without exceptions for business errors.

```csharp
public class Result<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public string? Error { get; }
    public List<string> Errors { get; }

    public static Result<T> Success(T value) => new(true, value, null, null);
    public static Result<T> Failure(string error) => new(false, default, error, null);
    public static Result<T> Failure(List<string> errors) => new(false, default, null, errors);
}
```

**When to Use:** All public API return types. Never throw for expected business failures.

---

## MEDIATOR / PIPELINE BEHAVIOR

Cross-cutting concerns through decorators.

```csharp
public class ValidationBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TCommand command, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var validator = _serviceProvider.GetService<IValidator<TCommand>>();
        if (validator is not null)
        {
            var validation = await validator.ValidateAsync(command, ct);
            if (!validation.IsValid)
                return Result.Failure<TResponse>(validation.Errors);
        }

        return await next();
    }
}
```

**When to Use:** Logging, validation, authorization, metrics, caching.

---

## RELATED DOCUMENTS

- framework/architecture.md
- framework/abstractions.md

---

END OF DOCUMENT
