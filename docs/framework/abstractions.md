# Core Abstractions

Version: 1.0
Status: Approved
Last Updated: 2026-07-12

---

## PURPOSE

This document describes the core abstractions provided by the Eleraki Framework.

---

## ENTITY

Represents an object with a unique identity.

```csharp
public abstract class Entity<T> where T : struct
{
    public T Id { get; protected set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<T> other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(Entity<T>? left, Entity<T>? right)
        => Equals(left, right);
    public static bool operator !=(Entity<T>? left, Entity<T>? right)
        => !Equals(left, right);
}
```

**Key Characteristics:**
- Identity persists across lifetimes
- Equality based on identity, not attributes
- Mutable state allowed

---

## VALUE OBJECT

Represents an object defined by its attributes, not identity.

```csharp
public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;
        return EqualsInternal((ValueObject)obj);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked { return HashCode.Combine(current, obj); }
            });
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
        => Equals(left, right);
    public static bool operator !=(ValueObject? left, ValueObject? right)
        => !Equals(left, right);

    private bool EqualsInternal(ValueObject other)
        => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
}
```

**Example Usage:**

```csharp
public record Address(string Street, string City, string Country) : ValueObject;
```

**Key Characteristics:**
- Immutable
- Equality based on structural attributes
- No identity

---

## AGGREGATE ROOT

Entity that serves as consistency boundary for a cluster of related objects.

```csharp
public abstract class AggregateRoot<T> : Entity<T> where T : struct
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent @event)
        => _domainEvents.Add(@event);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
```

**Responsibilities:**
- Enforce invariants
- Control access to children
- Publish domain events
- Serve as repository entry point

---

## RESULT

Represents the outcome of an operation, avoiding exceptions for expected failures.

```csharp
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }
    public List<string> Errors { get; }

    protected Result(bool isSuccess, string? error, List<string>? errors)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors ?? new();
    }

    public static Result Success() => new(true, null, null);
    public static Result Failure(string error) => new(false, error, null);
    public static Result Failure(List<string> errors) => new(false, errors.FirstOrDefault(), errors);
}

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(bool isSuccess, T value, string? error, List<string>? errors)
        : base(isSuccess, error, errors)
        => Value = value;

    public static Result<T> Success(T value) => new(true, value, null, null);
    public static new Result<T> Failure(string error) => new(false, default, error, null);
    public static new Result<T> Failure(List<string> errors) => new(false, default, errors.FirstOrDefault(), errors);
}
```

---

## GUARD

Provides argument validation at system boundaries.

```csharp
public static class Guard
{
    public static void AgainstNull(object? value, string paramName)
    {
        if (value is null)
            throw new ArgumentNullException(paramName);
    }

    public static void AgainstNullOrEmpty(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or empty.", paramName);
    }

    public static void AgainstOutOfRange(int value, int min, int max, string paramName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName);
    }
}
```

**Usage:**

```csharp
public Customer(string name, string email)
{
    Guard.AgainstNullOrEmpty(name, nameof(name));
    Guard.AgainstNullOrEmpty(email, nameof(email));
    Name = name;
    Email = email;
}
```

---

## RELATED DOCUMENTS

- framework/architecture.md
- framework/patterns.md

---

END OF DOCUMENT
