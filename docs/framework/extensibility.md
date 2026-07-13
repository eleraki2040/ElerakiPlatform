# Extensibility

Version: 1.0
Status: Approved
Last Updated: 2026-07-12

---

## PURPOSE

This document describes how to extend the Eleraki Framework for application-specific needs.

---

## EXTENSION PRINCIPLES

1. **Do not modify framework code** - Extend through interfaces and composition
2. **Prefer composition over inheritance** - Use dependency injection
3. **Convention over configuration** - Framework provides sensible defaults
4. **Open for extension, closed for modification** - Apply Open/Closed Principle

---

## PLUGIN MODEL

The framework supports module-based extensibility through assembly scanning and DI registration.

```csharp
// Module registration
public interface IModule
{
    void RegisterServices(IServiceCollection services);
}

// Framework module
public class CoreModule : IModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}

// Application module
public class OrganizationModule : IModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```

**Loading modules at startup:**

```csharp
public static void AddFramework(this IServiceCollection services, params IModule[] modules)
{
    var moduleTypes = modules ?? new IModule[] { new CoreModule() };
    foreach (var module in moduleTypes)
        module.RegisterServices(services);
}
```

---

## CUSTOM AGGREGATES

Extend the framework by inheriting from base abstractions:

```csharp
public class Customer : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }

    public static Customer Create(string name, string email)
    {
        var customer = new Customer { Id = Guid.NewGuid() };
        customer.RaiseDomainEvent(new CustomerCreatedEvent(customer.Id));
        return customer;
    }
}
```

---

## CUSTOM REPOSITORIES

Extend repository behavior through interfaces and decorators:

```csharp
public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByEmailAsync(string email, CancellationToken ct = default);
}

public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context) { }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Email == email, ct);
    }
}
```

---

## CUSTOM PIPELINE BEHAVIORS

Add cross-cutting concerns through mediator pipeline behaviors:

```csharp
public class LoggingBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    private readonly ILogger<LoggingBehavior<TCommand, TResponse>> _logger;

    public async Task<TResponse> Handle(TCommand command, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        _logger.LogInformation("Handling {CommandType}", typeof(TCommand).Name);
        var response = await next();
        _logger.LogInformation("Handled {CommandType}", typeof(TCommand).Name);
        return response;
    }
}

// Register in module
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
```

---

## EVENT BUS EXTENSION

Publish custom events and subscribe to framework events:

```csharp
// Define event
public record CustomerSubscribedEvent(Guid CustomerId, DateTime OccurredOn) : IDomainEvent;

// Subscribe
public class CustomerSubscribedHandler : IDomainEventHandler<CustomerSubscribedEvent>
{
    public async Task Handle(CustomerSubscribedEvent @event, CancellationToken ct)
    {
        await _emailSender.SendWelcomeAsync(@event.CustomerId, ct);
    }
}
```

---

## CUSTOM VALIDATORS

Add validation using FluentValidation integration:

```csharp
public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
```

---

## RELATED DOCUMENTS

- framework/architecture.md
- framework/patterns.md
- framework/abstractions.md

---

END OF DOCUMENT
