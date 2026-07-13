# Security Guidelines

Version: 1.0
Status: Approved
Last Updated: 2026-07-12

---

## PURPOSE

This document describes security patterns and guidelines for the Eleraki Framework.

---

## AUTHENTICATION

The framework delegates authentication to the Identity Engine.

```csharp
// Access current user from context
public class CurrentUser
{
    public Guid UserId { get; init; }
    public string? Email { get; init; }
    public List<string> Roles { get; init; } = new();
}

// Injected via scoped service
public class OrderHandler : CommandHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly CurrentUser _currentUser;

    public OrderHandler(CurrentUser currentUser, ...)
    {
        _currentUser = currentUser;
    }
}
```

---

## AUTHORIZATION

Use policy-based authorization at the command/query level.

```csharp
// Command authorization
[Authorize(Policy = "CustomerManagement")]
public record CreateCustomerCommand(string Name, string Email) : ICommand<Result<Guid>>;

// Resource-based authorization
public async Task<Result<OrderDto>> Handle(GetOrderQuery query, CancellationToken ct)
{
    var order = await _repository.GetByIdAsync(query.OrderId, ct);
    if (order is null) return Result.Failure<OrderDto>("Order not found.");

    if (!_authorizationService.CanAccessOrder(_currentUser.UserId, order))
        return Result.Failure<OrderDto>("Access denied.");

    return Result.Success(order.ToDto());
}
```

**Policy Registration:**

```csharp
services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerManagement",
        policy => policy.RequireRole("Admin", "Sales"));
});
```

---

## TENANT ISOLATION

All data access must filter by tenant.

```csharp
public abstract class TenantAwareRepository<T> : EfRepository<T>
    where T : Entity<Guid>, ITenantEntity
{
    protected override IQueryable<T> ApplyTenantFilter(IQueryable<T> query)
    {
        var tenantId = _tenantContext.TenantId;
        return query.Where(e => e.TenantId == tenantId);
    }
}
```

**Implementation:**
- Tenant ID resolved from authenticated user context
- All queries automatically filter by tenant
- Cross-tenant access is denied at repository level

---

## INPUT VALIDATION

Validate all inputs at system boundaries.

```csharp
// Commands are automatically validated
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Items).NotEmpty();
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).NotEmpty();
            item.RuleFor(i => i.Quantity).GreaterThan(0);
        });
    }
}
```

**Guidelines:**
- Never trust client input
- Validate in command validators
- Use FluentValidation for complex rules
- Sanitize strings before storage

---

## SENSITIVE DATA

Protect sensitive data at rest and in transit.

```csharp
// Encryption for sensitive fields
public class Customer : Entity<Guid>
{
    public string EncryptedSsn { get; private set; }
    public string Email { get; private set; }
}

// Use framework encryption utilities
public string Encrypt(string plainText)
    => _encryptionService.Encrypt(plainText, _currentUser.TenantId);
```

**Guidelines:**
- Encrypt PII at rest
- Use HTTPS for all communications
- Never log sensitive data
- Rotate encryption keys periodically

---

## AUDIT LOGGING

All significant operations must be logged.

```csharp
// Automatic audit via pipeline behavior
public class AuditBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TCommand command, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var audit = new AuditEntry
        {
            UserId = _currentUser.UserId,
            Action = typeof(TCommand).Name,
            Timestamp = DateTime.UtcNow,
            Data = JsonSerializer.Serialize(command)
        };

        var response = await next();
        await _auditLog.WriteAsync(audit, ct);
        return response;
    }
}
```

---

## SECURITY HEADERS

```csharp
// Configure security headers at application level
app.UseHsts();
app.UseHttpsRedirection();
app.UseXContentTypeOptions();
app.UseXFrameOptions();
app.UseXXssProtection();
app.UseReferrerPolicy(opts => opts.NoReferrer());
```

---

## RELATED DOCUMENTS

- framework/patterns.md
- framework/abstractions.md

---

END OF DOCUMENT
