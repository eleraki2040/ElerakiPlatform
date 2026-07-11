using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.AuthorizationEngine.Domain;

public class PermissionType : ValueObject
{
    private PermissionType(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static PermissionType Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));
        return new PermissionType(value);
    }

    public static PermissionType Read => new("read");
    public static PermissionType Write => new("write");
    public static PermissionType Delete => new("delete");
    public static PermissionType Admin => new("admin");

    public static implicit operator string(PermissionType type) => type.Value;
    public static explicit operator PermissionType(string value) => Create(value);

    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not PermissionType otherPermission) return false;
        return string.Equals(Value, otherPermission.Value, StringComparison.OrdinalIgnoreCase);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
