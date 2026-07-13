# Testing Strategy

Version: 1.0
Status: Approved
Last Updated: 2026-07-12

---

## PURPOSE

This document describes the testing strategy and utilities provided by the Eleraki Framework.

---

## TESTING PYRAMID

The framework supports a balanced testing approach:

```
        /\
       /  \      E2E (Few)
      /----\
     /      \    Integration (Some)
    /--------\
   /          \  Unit (Many)
  /------------\
```

**Guidelines:**
- Write unit tests for domain logic and command/query handlers
- Write integration tests for repository and event bus implementations
- Write E2E tests for critical user flows only

---

## TEST UTILITIES

### Test Base Classes

```csharp
public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected readonly IServiceProvider ServiceProvider;
    protected readonly AppDbContext DbContext;

    protected IntegrationTestBase()
    {
        ServiceProvider = TestHelper.BuildServiceProvider();
        DbContext = ServiceProvider.GetRequiredService<AppDbContext>();
    }

    public virtual async Task InitializeAsync()
        => await DbContext.Database.EnsureCreatedAsync();

    public virtual async Task DisposeAsync()
        => await DbContext.Database.EnsureDeletedAsync();

    protected async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default)
    {
        var mediator = ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(command, ct);
    }
}
```

### In-Memory Infrastructure

```csharp
// In-memory event bus for testing
services.AddSingleton<IEventBus, InMemoryEventBus>();

// In-memory database
services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase($"TestDb-{Guid.NewGuid()}"));
```

### Test Data Builders

```csharp
public class CustomerBuilder
{
    private string _name = "Test Customer";
    private string _email = "test@example.com";

    public CustomerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CustomerBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public Customer Build()
    {
        return Customer.Create(_name, _email);
    }
}

// Usage
var customer = new CustomerBuilder()
    .WithName("Mohamed Eleraki")
    .WithEmail("mohamed@example.com")
    .Build();
```

---

## MOCKING

Use standard mocking libraries (Moq, NSubstitute) for interfaces. Avoid mocking concrete types.

```csharp
[Fact]
public async Task CreateCustomer_ValidCommand_ReturnsSuccess()
{
    var customerRepository = new Mock<ICustomerRepository>();
    var handler = new CreateCustomerHandler(customerRepository.Object);

    var result = await handler.Handle(new CreateCustomerCommand("Test", "test@example.com"), CancellationToken.None);

    Assert.True(result.IsSuccess);
    customerRepository.Verify(r => r.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
}
```

---

## ASSERTIONS

Prefer framework assertion utilities over raw assertions.

```csharp
// Result assertions
result.Should().BeSuccess();
result.Should().BeFailure();
result.Should().HaveError("Customer already exists.");

// Domain event assertions
aggregate.DomainEvents.Should().ContainSingle()
    .Which.Should().BeOfType<CustomerCreatedEvent>();
```

---

## TEST ORGANIZATION

```
tests/
├── Framework.UnitTests/
│   ├── Domain/
│   ├── Application/
│   └── Common/
├── Framework.IntegrationTests/
│   ├── Infrastructure/
│   └── Persistence/
└── Application.IntegrationTests/
    └── <Business Application>/Modules/
```

---

## RELATED DOCUMENTS

- framework/architecture.md
- framework/patterns.md

---

END OF DOCUMENT
